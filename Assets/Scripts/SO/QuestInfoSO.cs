using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTask")]
public class QuestInfoSO : ScriptableObject
{
    public string TaskName;
    public string TaskDescription;
    public bool isCompleted = false;
    public bool isJustSubtitle = false;
    public Action OnTaskCompleted;
    public SubtitlesSO subtitles;
    public void CompleteTask()
    {
        if (isCompleted) return;

        isCompleted = true;
        OnTaskCompleted?.Invoke();
    }
}