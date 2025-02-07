using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interract();
    bool CanBePickedUp();
    Transform GetInteractionTarget();
    Sprite GetSprite();
    GameObject GetGameObject();
}
