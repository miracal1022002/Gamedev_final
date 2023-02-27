using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void DeSetup(){
        gameObject.SetActive(false);
    }
}
