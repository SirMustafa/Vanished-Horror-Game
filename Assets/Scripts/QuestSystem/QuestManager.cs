using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : MonoBehaviour
{
    private PlayerUiManager _playerUi;
    private QuestInfoSO currentTask;

    [Inject]
    void setup(PlayerUiManager playerui)
    {
        _playerUi = playerui;
    }

    public void SetStartQuest(QuestInfoSO currentQuest)
    {
        currentTask = currentQuest;
        if (currentTask.subtitles is not null)
        {
            EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.SubtitleState);

            _playerUi.ShowSubtitle(currentTask.subtitles);
            _playerUi.SetMissionText(currentTask.TaskName);
        }
        currentTask.OnTaskCompleted += CompleteCurrentQuest;
    }

    

    private void CompleteCurrentQuest()
    {
        //tasksQue.RemoveAt(0);
        //
        //if (tasksQue.Count > 0)
        //{
        //    StartCoroutine(waitAlitte());
        //}
        //else
        //{
        //    Debug.Log("Tüm görevler tamamlandý!");
        //}
    }
    private void CheckQuest()
    {
        if (currentTask.isJustSubtitle)
        {
            CompleteCurrentQuest();
        }
    }
}