using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public string nextLevelName;
    private void Start() {
        AudioManager.Instance.StopMusic("MenuTheme");
        AudioManager.Instance.PlayMusic("Level1");
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
