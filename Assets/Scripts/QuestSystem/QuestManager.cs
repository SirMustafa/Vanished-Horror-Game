using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestInfoSO> questList = new List<QuestInfoSO>();

    private PlayerUiManager _playerUi;
    private QuestInfoSO currentQuest;

    [Inject]
    public void Setup(PlayerUiManager playerUi)
    {
        _playerUi = playerUi;
    }

    public void StartQuest()
    {
        StartCoroutine(StartQuestRoutine());
    }

    private IEnumerator StartQuestRoutine()
    {
        yield return new WaitForSeconds(1f);

        if (questList.Count > 0)
        {
            currentQuest = questList[0];
            _playerUi.SetMissionText(currentQuest.TaskName);

            if (currentQuest.subtitles is not null)
            {
                EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.SubtitleState);
                _playerUi.ShowSubtitle(currentQuest.subtitles);
            }
            else
            {
                EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
            }
            currentQuest.OnTaskCompleted += QuestComplete;
            Debug.Log($"Görev '{currentQuest.TaskName}' baþlatýldý.");
        }
        else
        {
            Debug.Log("Tüm görevler tamamlandý!");
            _playerUi.SetMissionText("Tüm görevler tamamlandý!");
        }
    }
    public void CheckQuestType()
    {
        if (currentQuest.isJustSubtitle)
        {
            QuestComplete();
        }
    }
    private void QuestComplete()
    {
        if (currentQuest != null)
        {
            currentQuest.OnTaskCompleted -= QuestComplete;
            Debug.Log($"Görev '{currentQuest.TaskName}' tamamlandý.");
            questList.RemoveAt(0);
            StartQuest();
        }
    }
}