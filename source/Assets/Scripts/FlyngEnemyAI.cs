using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyngEnemyAI : MonoBehaviour
{
    public FlyingEnemyController[] enemyArray;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            foreach(FlyingEnemyController enemy in enemyArray){
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            foreach(FlyingEnemyController enemy in enemyArray){
                enemy.chase = false;
            }
        }
    }
}
