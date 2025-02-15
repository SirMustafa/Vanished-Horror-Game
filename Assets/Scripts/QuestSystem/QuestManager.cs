using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestInfoSO> tasksQue = new List<QuestInfoSO>();
    private PlayerUiManager _playerUi;
    private QuestInfoSO currentTask;

    [Inject]
    void setup(PlayerUiManager playerui)
    {
        _playerUi = playerui;
    }

    public void StartQuest()
    {
        if (tasksQue.Count > 0)
        {
            currentTask = tasksQue[0];

            if (currentTask.subtitles != null)
            {
                EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.SubtitleState);

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

    public void CheckQuest()
    {
        if (currentTask.isJustSubtitle)
        {
            CompleteCurrentQuest();
        }
    }

    private void CompleteCurrentQuest()
    {
        tasksQue.RemoveAt(0);

        if (tasksQue.Count > 0)
        {
            StartCoroutine(waitAlitte());
        }
        else
        {
            Debug.Log("Tüm görevler tamamlandý!");
        }
    }
    IEnumerator waitAlitte()
    {
        yield return new WaitForSeconds(1);
        _playerUi.SetMissionText("No mission for now");
        StartQuest();
    }
}