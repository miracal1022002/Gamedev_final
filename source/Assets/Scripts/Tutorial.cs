using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public TutorialController controller;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            controller.Setup();
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            controller.DeSetup();
        }
    }
}
