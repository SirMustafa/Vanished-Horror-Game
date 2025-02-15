using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource subtitleSource;
    [SerializeField] AudioSource ambianceSource;
    [SerializeField] AudioMixer audioMixer;

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
        ambianceSource.clip = musicClip;
        ambianceSource.loop = true;

        ambianceSource.Play();
    }

    public void PlaySubtitle(AudioClip subtitleSpeech)
    {
        subtitleSource.PlayOneShot(subtitleSpeech);
    }

    public void StopSound(SoundTypes whichSound)
    {

    }
    public void SetMusicVolume(float volumeAmount)
    {
        audioMixer.SetFloat("MusicVolume", volumeAmount);
    }
    public void SetSfxVolume(float volumeAmount)
    {
        audioMixer.SetFloat("SFXVolume", volumeAmount);
    }
}