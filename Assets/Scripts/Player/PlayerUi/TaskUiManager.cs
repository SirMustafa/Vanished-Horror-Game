using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskUiManager : MonoBehaviour
{
    public void OnExit(InputAction.CallbackContext context)
    {
        EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.Player);
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
    }
}