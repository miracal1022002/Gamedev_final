using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public string thisLevelMusicName;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused && !optionMenu.activeSelf){
                Resume();
            }else if(!isPaused && !optionMenu.activeSelf){
                Pause();
            }else{
                
            }
        }

        Debug.Log(optionMenu.activeSelf);
    }

    public void Resume(){
        AudioManager.Instance.UnpauseMusic(thisLevelMusicName);
        AudioManager.Instance.PlaySFX("gameUnpause");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause(){
        AudioManager.Instance.PauseMusic(thisLevelMusicName);
        AudioManager.Instance.PlaySFX("gamePause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Menu(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        Debug.Log("Menu");
    }

    public void QuitGame(){
        Application.Quit(); 
        Debug.Log("Quit");
    }
}
