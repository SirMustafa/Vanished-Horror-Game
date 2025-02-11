using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    bool isOpened = false;
    bool canSleep = false;
    float _rotationDuration = 1f;

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
        if (isOpened)
        {
            transform.DORotate(new Vector3(0, -90, 0), _rotationDuration);
            isOpened = false;
        }
        else
        {
            transform.DORotate(new Vector3(90, 0, 90), _rotationDuration);
            isOpened = true;
        }
    }
}