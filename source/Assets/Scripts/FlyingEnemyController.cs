using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public float speed = 3f;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    public PlayerCombat playerCombat;
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(player == null){
            return;
        }
        if(chase==true){
            Chase();
        }else{
            ReturnToStartPoint();
        }
        Flip();

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            animator.SetTrigger("Attack");
            Invoke("Attack", 0.5f);
        }
    }

    void Attack(){
        playerCombat.TakeDamage();
    }

    public void TakeDamage(int damage){
        animator.SetTrigger("Hit");
        currentHealth -= damage;
        if(currentHealth <= 0){
            Invoke("Die", 0.3f);
        }
    }

    void Die(){
        animator.SetTrigger("Death");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.7f);
    }

    void Chase(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    }

    void Flip(){
        if(transform.position.x < player.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void ReturnToStartPoint(){

        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
}
