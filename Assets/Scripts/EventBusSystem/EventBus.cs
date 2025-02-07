using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static class PlayerEvents
    {
        public static event Action<Vector2> OnMove;
        public static event Action OnJump;
        public static event Action<bool> OnSprint;

        public static void TriggerMove(Vector2 direction) => OnMove?.Invoke(direction);
        public static void TriggerJump() => OnJump?.Invoke();
        public static void TriggerSprint(bool isSprinting) => OnSprint?.Invoke(isSprinting);
    }

    public static class InteractionEvents
    {
        public static event Action OnInteract;
        public static event Action OnDrop;
        public static event Action OnLeftMouseClick;

        public static void TriggerInteract() => OnInteract?.Invoke();
        public static void TriggerDrop() => OnDrop?.Invoke();
        public static void TriggerLeftMouseClick() => OnLeftMouseClick?.Invoke();
    }

    public static class CameraEvents
    {
        public static event Action<bool> OnAiming;
        public static event Action<float> OnScroll;

        public static void TriggerAiming(bool isAiming) => OnAiming?.Invoke(isAiming);
        public static void TriggerScroll(float scrollValue) => OnScroll?.Invoke(scrollValue);
    }
    public static class InputEvents
    {
        public static event Action<GameManager.GameState> OnGameStateChange;
        public static event Action<Inputs.ActionMap> OnActionMapChange;
        public static event Action<float> OnFrameChange;

        public static void TriggerGameStateChange(GameManager.GameState currentState)
            => OnGameStateChange?.Invoke(currentState);
        public static void TriggerActionMapChange(Inputs.ActionMap actionMap)
            => OnActionMapChange?.Invoke(actionMap);
        public static void TriggerFrameChangeEvent(float position) => OnFrameChange?.Invoke(position);
    }
}