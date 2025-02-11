using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource subtitleSource;
    [SerializeField] AudioSource ambianceSource;

    public enum SoundTypes
    {
        Ambiance,
        Music,
        Sfx,
        Dialouge
    }
    public SoundTypes soundType;

    private void Awake()
    {
        if (AudioInstance is null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySfx(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
    public void PlayMusic()
    {

    }
    public void PlayAmbiance(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;

        musicSource.Play();
    }

    public void PlaySubtitle(AudioClip subtitleSpeech)
    {
        subtitleSource.PlayOneShot(subtitleSpeech);
    }

    public void StopSound(SoundTypes whichSound)
    {

    }
}