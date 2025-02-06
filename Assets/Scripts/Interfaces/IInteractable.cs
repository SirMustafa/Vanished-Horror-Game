using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void MyInterract();
    bool CanBePickedUp();
    Transform GetInteractionTarget();
    Sprite GetSprite();
}
