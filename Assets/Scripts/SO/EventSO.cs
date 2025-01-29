using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event")]
public class EventSO<T> : ScriptableObject
{
    private List<IEventListener<T>> _listeners = new List<IEventListener<T>>();

    public void Register(IEventListener<T> listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void UnRegister(IEventListener<T> listener)
    {
        if (_listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }

    public void Raise(T parameter)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].Raise(parameter);
        }
    }
}
