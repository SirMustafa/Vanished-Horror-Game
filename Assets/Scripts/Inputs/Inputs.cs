using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnInteractEven;
    public event Action OnLeftMouseEvent;
    public event Action<bool> OnAimingEvent;
    public event Action<bool> OnSprintEvent;
    [SerializeField] private PlayerInput playerInput;
    public enum ActionMap
    {
        Player,
        OnChair,
        OnCamera
    }

    public void SwitchActionMap(ActionMap actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap.ToString());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(input);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
        }
    }
    public void OnInterract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractEven?.Invoke();
        }
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAimingEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnAimingEvent?.Invoke(false);
        }
    }
    public void OnLeftMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnLeftMouseEvent?.Invoke();
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSprintEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnSprintEvent?.Invoke(false);
        }
    }
}