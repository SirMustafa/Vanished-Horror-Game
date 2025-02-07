using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chair : MonoBehaviour, IInteractable
{
    private Inputs _inputs;
    private PlayerController _player;
    private Vector3 _chairPosition;

    [Inject]
    void InjectDependencies(PlayerController player, Inputs inputs)
    {
        _inputs = inputs;
        _player = player;
    }
    private void Start()
    {
        _chairPosition = transform.position + new Vector3(-1f, 0f, 0f);
    }

    public void Interract()
    {
        _inputs.SwitchActionMap(Inputs.ActionMap.OnChair);
        _player.SitChair(_chairPosition);
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