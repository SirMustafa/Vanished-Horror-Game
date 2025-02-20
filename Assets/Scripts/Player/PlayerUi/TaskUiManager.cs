using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskUiManager : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    public void OnQuestButtonPressed(QuestInfoSO quest)
    {
        questManager.StartSideQuest(quest);
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.Player);
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
    }
}