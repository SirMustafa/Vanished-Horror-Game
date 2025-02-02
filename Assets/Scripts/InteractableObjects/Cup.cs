using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cup : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent<GameObject> pickMeEvent;
    [SerializeField] private UnityEvent dropMeEvent;
    Rigidbody body;
    bool isPicked;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    public void MyInterract()
    {
        if (!isPicked)
        {
            isPicked = true;
            body.isKinematic = true;
            PickMeUp();
        }
        else
        {
            isPicked = false;
            body.isKinematic = false;
            dropMeEvent.Invoke();
        }
    }

    public void PickMeUp()
    {
        pickMeEvent.Invoke(this.gameObject);
    }
}