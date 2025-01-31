using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cup : InteractableBase
{
    Rigidbody body;
    [SerializeField] private UnityEvent<GameObject> pickMeEvent;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public override void MyInterract()
    {
        PickMeUp();
    }

    public void PickMeUp()
    {
        
    }
}