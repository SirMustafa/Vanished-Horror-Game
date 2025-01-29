using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener<T> : MonoBehaviour, IEventListener<T>
{
    public EventSO<T> gameEvent;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegister(this);
    }

    public void Raise(T parameter)
    {
        response.Invoke(parameter);
    }
}
