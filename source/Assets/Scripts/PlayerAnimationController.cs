using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    private void Update() {
        if(playerMovement.isJumping){
            animator.SetBool("isJumping", true);
        }

        if(playerMovement.isFalling){
            animator.SetBool("isJumping", false);
        }
        if(playerMovement.isGrounded()){
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            if(playerMovement.isRunning){
                animator.SetFloat("Speed", 1f);
            }else{
                animator.SetFloat("Speed", 0f);
            }
        }

        if(playerMovement.isFalling){
            animator.SetBool("isFalling", true);
        }
    }
}
