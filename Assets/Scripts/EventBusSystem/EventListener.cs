using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour, IEventListener
{
    public EventSO gameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegister(this);
    }

    public void Raise()
    {
        response.Invoke();
    }
}
