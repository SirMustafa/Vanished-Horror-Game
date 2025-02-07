using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite mySprite;
    public void MyInterract()
    {
        Debug.Log("amcuk");
    }

    public bool CanBePickedUp()
    {
        return true;
    }

    public Transform GetInteractionTarget()
    {
        return this.transform;
    }

    public Sprite GetSprite()
    {
        return mySprite;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}