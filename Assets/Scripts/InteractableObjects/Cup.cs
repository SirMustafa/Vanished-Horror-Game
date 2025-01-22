using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cup : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private UnityEvent<GameObject> pickMeEvent;

    public void MyInterract()
    {
        PickMeUp();
    }

    public void PickMeUp()
    {
        pickMeEvent.Invoke(gameObject);
    }
}