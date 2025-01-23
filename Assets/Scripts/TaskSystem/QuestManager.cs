using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestInfoSO> tasks = new List<QuestInfoSO>();   
    private int _currentTaskIndex = 0;

    public void Initialize()
    {
        StartTask(_currentTaskIndex);
    }

    private void StartTask(int index)
    {
        QuestInfoSO currentTask = tasks[index];

        if (currentTask.subtitles != null)
        {
            
        }
        currentTask.OnTaskCompleted += () =>
        {
            Debug.Log("TaskFinished");
            StartNextTask();
        };
    }

    private void StartNextTask()
    {
        if (_currentTaskIndex + 1 < tasks.Count)
        {
            _currentTaskIndex++;
            StartTask(_currentTaskIndex);
        }
        else
        {
            Debug.Log("All tasks completed!");
        }
    }

    public void CompleteCurrentTask()
    {
        if (_currentTaskIndex < tasks.Count)
        {
            tasks[_currentTaskIndex].CompleteTask();
        }
    }
}