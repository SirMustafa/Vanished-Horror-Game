using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBase : MonoBehaviour
{
    protected string TaskName;
    protected string TaskDescription;
    protected bool IsCompleted;

    public abstract void StartTask();
    public abstract void StopTask();

    protected void CompleteTask()
    {
        IsCompleted = true;
        Debug.Log($"{TaskName} tamamlandý!");
        StopTask();
    }
}