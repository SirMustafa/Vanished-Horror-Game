using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public UnityEvent OnEscBtn;
    [SerializeField] private PlayerInput playerInput;
    bool isGamePaused = false;
    public enum ActionMap
    {
        Player,
        OnChair,
        OnCamera,
        OnPause,
        OnTask
    }
    private void OnEnable()
    {
        EventBus.InputEvents.OnActionMapChange += SwitchActionMap;
    }

    public void SwitchActionMap(ActionMap actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap.ToString());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        EventBus.PlayerEvents.TriggerMove(input);
    }
    public void OnInterract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.InteractionEvents.TriggerInteract();
        }
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.CameraEvents.TriggerAiming(true);
        }
        else if (context.canceled)
        {
            EventBus.CameraEvents.TriggerAiming(false);
        }
    }
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.InteractionEvents.TriggerLeftMouseClick();
        }
    }

    public void OnEscButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isGamePaused = !isGamePaused;
            OnEscBtn?.Invoke();
            SwitchActionMap(isGamePaused ? ActionMap.OnPause : ActionMap.Player);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.PlayerEvents.TriggerSprint(true);
        }
        else if (context.canceled)
        {
            EventBus.PlayerEvents.TriggerSprint(false);
        }
    }
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float scrollValue = context.ReadValue<Vector2>().y;
            EventBus.CameraEvents.TriggerScroll(scrollValue);
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.InteractionEvents.TriggerDrop();
        }
    }
    private void OnDisable()
    {
        EventBus.InputEvents.OnActionMapChange -= SwitchActionMap;
    }
}