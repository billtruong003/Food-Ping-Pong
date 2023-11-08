using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
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

    public List<AudioSource> SoundAudioSources = new List<AudioSource>();
    public List<AudioSource> MusicAudioSources = new List<AudioSource>();
    public List<AudioClip> SoundAudioClips = new List<AudioClip>();
    public List<AudioClip> MusicAudioClips = new List<AudioClip>();

    public void PlaySFX(int index)
    {
        SoundAudioSources[0].PlayOneShot(SoundAudioClips[index]);
    }
    public void PlayMusic(int index)
    {
        MusicAudioSources[0].PlayOneShot(MusicAudioClips[index]);
    }
    public void PlayWallCollide(AudioClip wallCollide)
    {
        SoundAudioSources[0].PlayOneShot(wallCollide);
    }
    public void StopSFX()
    {
        SoundAudioSources[0].Stop();
    }
    public void SetMusicVol(float volume)
    {
        MusicAudioSources[0].volume = volume / 100;
    }
    public void SetSoundVol(float volume)
    {
        SoundAudioSources[0].volume = volume / 100;
    }
    private void Start()
    {
        MusicAudioSources[0].Play();
    }
}
