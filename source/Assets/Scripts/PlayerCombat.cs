using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 100;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public LayerMask flyingEnemyLayer;
    public LayerMask groundedEnemyLayer;
    public HealthAndGem healthAndGem;
    public GameOverScreen gameOverScreen;
    public FinishedMenu finishedMenu;
    private void Start() {
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J)){
                animator.SetTrigger("isAtk");
                Invoke("AttackFlyingEnemy", 0.1f);
                Invoke("AttackGroundedEnemy", 0.1f);
                Invoke("PlayAttackAudio", 0.3f);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void PlayAttackAudio(){
        AudioManager.Instance.PlaySFX("Attack");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Spike"){
            Die();
        }

        
        if(other.gameObject.tag == "Finish"){
            finishedMenu.Setup();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Cherry")){
            healthAndGem.health++;
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySFX("Cherry");
        }

        if(other.CompareTag("Gem")){
            healthAndGem.gemCount++;
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySFX("Gem");
        }
    }

    private void AttackFlyingEnemy(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, flyingEnemyLayer);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<FlyingEnemyController>().TakeDamage(attackDamage);           
        }
    }

    private void AttackGroundedEnemy(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, groundedEnemyLayer);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<GroundedEnemyController>().TakeDamage(attackDamage);           
        }
    }

    private void OnDrawGizmosSelected() {

        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    void PlayHitSFX(){
        AudioManager.Instance.PlaySFX("Hit");
    }
    
    public void TakeDamage(){
        Debug.Log("Attacked");
        healthAndGem.health--;
        animator.SetTrigger("TakeDamage");
        Invoke("PlayHitSFX", 0.5f);
        if(healthAndGem.health <= 0){
            Die();
        }
    }
    
    void Die(){
        healthAndGem.health = 0;
        animator.SetTrigger("Die");
        this.enabled = false;
        Destroy(this.gameObject, 1f);
        gameOverScreen.Setup();
    }

}
