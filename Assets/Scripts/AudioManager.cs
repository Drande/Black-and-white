using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic("GameLoop");
    }

    private IEnumerator MusicPlaylist(Sound[] sounds) {
        musicSource.loop = false;
        while(true) {
            var nextSound = sounds[UnityEngine.Random.Range(0, sounds.Length)];
            musicSource.clip = nextSound.audioClip;
            musicSource.Play();
            yield return new WaitForSeconds(nextSound.audioClip.length);
        }
    }
    
    public void PlayPlaylist(Sound[] sounds) {
        StopCoroutine(nameof(MusicPlaylist));
        StartCoroutine(MusicPlaylist(sounds));
    }

    public void PlayMusic(string name) {
        StopCoroutine(nameof(MusicPlaylist));
        var sound = Array.Find(musicSounds, s => s.name == name);
        if(sound != null) {
            musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = sound.audioClip;
            musicSource.Play();
        } else {
            Debug.Log($"Couldn't find music sound by name: '{name}'");
        }
    }

    public void PlaySFX(string name) {
        var sound = Array.Find(sfxSounds, s => s.name == name);
        if(sound != null) {
            sfxSource.clip = sound.audioClip;
            sfxSource.Play();
        } else {
            Debug.Log($"Couldn't find sound effect by name: {name}");
        }
    }
}