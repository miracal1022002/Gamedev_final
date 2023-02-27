using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemyController : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public float speed = 1f;
    private GameObject player;
    public PlayerCombat playerCombat;
    private bool movingRight = true;
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(player == null){
            return;
        }

        if(movingRight){
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }else{
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }

        Flip();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Turn")){
			if (movingRight){
				movingRight = false;
			}
			else{
				movingRight = true;
			}	
		}

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

    void Flip(){
            if(transform.position.x < player.transform.position.x){
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }else{
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

    void Die(){
        animator.SetTrigger("Death");
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 0.7f);
    }

}
