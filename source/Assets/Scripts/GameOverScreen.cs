using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    Scene thisScene;
    private string sceneName;

    private void Start() {
        thisScene = SceneManager.GetActiveScene();
        sceneName = thisScene.name;
    }

    private void Update() {
        AudioManager.Instance.StopMusic("MenuTheme");
        if(Input.anyKeyDown){
            Invoke("Restart", 1f);
            AudioManager.Instance.PlayMusic(AudioManager.Instance.thisLevelMusicName);
        }
    }

    public void Setup(){
        gameObject.SetActive(true);
        AudioManager.Instance.StopMusic("Level1");
        Invoke("PlayGameOverMusic", 0.7f);
    }

    public void PlayGameOverMusic(){
        AudioManager.Instance.PlaySFX("GameOver");
    }
    public void Restart(){
        SceneManager.LoadScene(sceneName);
    }
}
