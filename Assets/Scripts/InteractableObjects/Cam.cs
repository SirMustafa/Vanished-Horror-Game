using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cam : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject virtualCamera;

    public void MyInterract()
    {
        EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.OnCamera);
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.OnCameraState);
        virtualCamera.SetActive(true);
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.Player);
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
        virtualCamera.SetActive(false);
    }
}
