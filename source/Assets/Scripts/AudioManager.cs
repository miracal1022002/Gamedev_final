using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;
    public string thisLevelMusicName;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSound, x => x.soundName == name);
        if(s == null){
            Debug.Log("Sound not found");
        }else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSound, x => x.soundName == name);
        if(s == null){
            Debug.Log("Sound not found");
        }else{
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    } 

    public void StopMusic(string name){
        Sound s = Array.Find(musicSound, x => x.soundName == name);
        if(s == null){
            Debug.Log("Sound not found");
        }else{
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }

    public void PauseMusic(string name){
        Sound s = Array.Find(musicSound, x => x.soundName == name);
        if(s == null){
            Debug.Log("Sound not found");
        }else{
            musicSource.clip = s.clip;
            musicSource.Pause();
        }
    }

    public void UnpauseMusic(string name){
        Sound s = Array.Find(musicSound, x => x.soundName == name);
        if(s == null){
            Debug.Log("Sound not found");
        }else{
            musicSource.clip = s.clip;
            musicSource.UnPause();
        }
    }

    public void ToggleMusic(){
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX(){
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume){
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume){
        sfxSource.volume = volume;
    }
}
