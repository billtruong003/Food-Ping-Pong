using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy() {
        Instance = null;
    }

    public AudioSource[] SoundAudioSources;
    public AudioSource[] MusicAudioSources;
    public AudioClip[] SoundAudioClips;
    public AudioClip[] MusicAudioClips;

    public void PlaySoundEffect(int index)
    {
        SoundAudioSources[0].PlayOneShot(SoundAudioClips[index]);
    }
    public void PlayMusic(int index)
    {
        MusicAudioSources[0].PlayOneShot(MusicAudioClips[index]);
    }
    public void SetMusicVol(float volume)
    {
        MusicAudioSources[0].volume = volume/100;
    }
    public void SetSoundVol(float volume)
    {
        SoundAudioSources[0].volume = volume/100;
    }
    private void Start()
    {
        MusicAudioSources[0].Play();
    }
}
