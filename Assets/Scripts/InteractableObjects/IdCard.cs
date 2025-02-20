using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdCard : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite mySprite;
    public bool CanBePickedUp()
    {
        return true;
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
        return mySprite;
    }

    public void MyInterract()
    {
        throw new System.NotImplementedException();
    }
}
