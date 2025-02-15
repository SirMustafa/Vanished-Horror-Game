using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllAudios")]
public class AudiosSO : ScriptableObject
{
    public AudioClip AmbianceSound;
    [SerializeField] List<AudioClip> FootStepSounds = new List<AudioClip>();

    public AudioClip FootStepSound()
    {
        return FootStepSounds[Random.Range(0,4)];
    }
}