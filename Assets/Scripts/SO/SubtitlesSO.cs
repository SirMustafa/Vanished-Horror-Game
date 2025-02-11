using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Line")]
public class SubtitlesSO : ScriptableObject
{
    public Sprite narratorsSprite;
    public AudioClip narratorsClip;
    public string[] lines;
}
