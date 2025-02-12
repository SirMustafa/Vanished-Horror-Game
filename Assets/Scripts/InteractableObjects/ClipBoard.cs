using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBoard : MonoBehaviour, IInteractable
{
    public bool CanBePickedUp()
    {
        return false;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Transform GetInteractionTarget()
    {
        return this.transform;
    }

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public void MyInterract()
    {
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PauseState);
    }
}
