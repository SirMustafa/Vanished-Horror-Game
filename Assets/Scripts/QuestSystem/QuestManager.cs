using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : MonoBehaviour
{
    private PlayerUiManager _playerUi;

    private Queue<QuestInfoSO> mainQuestQueue = new();
    private List<QuestInfoSO> sideQuests = new();

    private QuestInfoSO currentMainQuest;

    [Inject]
    void Setup(PlayerUiManager playerUi)
    {
        _playerUi = playerUi;
    }

    public void StartMainQuest(QuestInfoSO newMainQuest)
    {
        mainQuestQueue.Enqueue(newMainQuest);
        if (currentMainQuest is null)
        {
            StartNextMainQuest();
        }
    }

    private void StartNextMainQuest()
    {
        if (mainQuestQueue.Count > 0)
        {
            currentMainQuest = mainQuestQueue.Dequeue();
            ActivateQuest(currentMainQuest);
        }
        else
        {
            currentMainQuest = null;
        }
    }

    public void StartSideQuest(QuestInfoSO sideQuest)
    {
        if (!sideQuests.Contains(sideQuest))
        {
            sideQuests.Add(sideQuest);
            ActivateQuest(sideQuest);
        }
    }

    private void ActivateQuest(QuestInfoSO quest)
    {
        if (quest.subtitles is not null)
        {
            EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.SubtitleState);
            _playerUi.ShowSubtitle(quest.subtitles);
            _playerUi.SetMissionText(quest.TaskName);
        }

        quest.OnTaskCompleted += () => CompleteQuest(quest);
    }

    private void CompleteQuest(QuestInfoSO quest)
    {
        quest.OnTaskCompleted -= () => CompleteQuest(quest);
        quest.isCompleted = true;

        if (quest == currentMainQuest)
        {
            StartNextMainQuest();
        }
        else if (sideQuests.Contains(quest))
        {
            sideQuests.Remove(quest);
        }
    }
}