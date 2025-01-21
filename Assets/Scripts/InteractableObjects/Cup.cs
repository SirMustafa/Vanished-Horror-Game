using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cup : MonoBehaviour, IInteractable, IPickable
{
    public UnityEvent<GameObject> OnPickUpEvent { get; private set; } = new UnityEvent<GameObject>();

    public void MyInterract()
    {
        OnPickUpEvent.Invoke(gameObject);
    }
}