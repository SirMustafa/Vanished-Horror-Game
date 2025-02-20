using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public List<string> lines = new List<string>();
    public Sprite speakerSprite;
    public AudioClip speakerAudio;
}

[CreateAssetMenu(fileName = "NewDialogue")]
public class SubtitlesSO : ScriptableObject
{
    public List<DialogueLine> dialogueLines;
}