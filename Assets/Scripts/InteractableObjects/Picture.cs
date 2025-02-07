using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour, IInteractable
{
    public bool CanBePickedUp()
    {
        return false;
    }

    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
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
