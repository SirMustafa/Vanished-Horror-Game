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

    private void Awake()
    {
        if (AudioInstance is null)
        {
            AudioInstance = this;
        }
    }

    public void PlaySfx()
    {

    }
    public void PlayMusic()
    {

    }
    public void PlayAmbiance()
    {

    }
    public void PlaySubtitle(AudioClip subtitleSpeech)
    {
        subtitleSource.PlayOneShot(subtitleSpeech);
    }
}