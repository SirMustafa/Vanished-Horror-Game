using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite mySprite;
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

    public void Interract()
    {
        Debug.Log("babucuk");
    }
}
