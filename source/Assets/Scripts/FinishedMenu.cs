using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedMenu : MonoBehaviour
{
    public GameObject finishedMenu;
    private void Start() {
        AudioManager.Instance.PlayMusic("Level2");
    }
    public void Setup(){
        finishedMenu.SetActive(true);
        AudioManager.Instance.PlayMusic("Finish");
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
        Debug.Log("Menu");
    }

    public void QuitGame(){
        Application.Quit(); 
        Debug.Log("QuitGame");
    }
}
