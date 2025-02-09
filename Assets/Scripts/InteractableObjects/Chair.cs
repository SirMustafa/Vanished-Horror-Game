using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _chairPosition;
    private Inputs _inputs;
    private PlayerController _player;
    
    [Inject]
    void InjectDependencies(PlayerController player, Inputs inputs)
    {
        _inputs = inputs;
        _player = player;
    }

    public void MyInterract()
    {
        _player.SitChair(_chairPosition.position, this);
    }

    public bool CanBePickedUp()
    {
        return false;
    }

    public Transform GetInteractionTarget()
    {
        return this.transform;
    }

    public Sprite GetSprite()
    {
        throw new NotImplementedException();
    }

    public GameObject GetGameObject()
    {
        throw new NotImplementedException();
    }
}