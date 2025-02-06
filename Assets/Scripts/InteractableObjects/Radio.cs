using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    public bool CanBePickedUp()
    {
        return false;
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
        
    }
}
