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
    public void SetVolume(float volume)
    {
        MusicAudioSources[0].volume = volume/100;
    }

    private void Start()
    {
        MusicAudioSources[0].Play();
    }

    // private void PlaySound (int index)
    // {
    //     int count;
    //     if (!int.TryParse(SoundCountTextBox.text, out count))
    //     {
    //         count = 1;
    //     }
    //     while (count-- > 0)
    //     {
    //         SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
    //     }
    // }

    // private void PlayMusic (int index)
    // {
    //     MusicAudioSources[index].PlayLoopingMusicManaged();
    // }

    // private void CheckPlayKey()
    // {
    //     if (SoundCount)
    // }
    
}
