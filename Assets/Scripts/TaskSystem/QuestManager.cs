using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestInfoSO> tasksQue = new List<QuestInfoSO>();
    private int _currentTaskIndex = 0;
    private PlayerUiManager _playerUi;

    [Inject]
    void setup(PlayerUiManager playerui)
    {
        _playerUi = playerui;
    }

    public void StartQuest()
    {
        if (_currentTaskIndex < tasksQue.Count)
        {
            QuestInfoSO currentTask = tasksQue[_currentTaskIndex];
            Debug.Log($"Görev Baþladý: {currentTask.TaskName}");

            if (currentTask.subtitles != null)
            {
                _playerUi.ShowSubtitle(currentTask.subtitles);
                _playerUi.SetMissionText(currentTask.TaskName);
            }
            currentTask.OnTaskCompleted += CompleteCurrentQuest;
        }
        else
        {
            Debug.Log("Tüm görevler tamamlandý!");
        }
    }

    private void CompleteCurrentQuest()
    {
        _currentTaskIndex++;

        if (_currentTaskIndex < tasksQue.Count)
        {
            _playerUi.SetMissionText("No mission for now");
            //StartQuest();
        }
        else
        {
            Debug.Log("Tüm görevler tamamlandý!");
        }
    }
}