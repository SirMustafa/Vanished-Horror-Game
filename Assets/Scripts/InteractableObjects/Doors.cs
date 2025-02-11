using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{
    [SerializeField] private float _rotationDuration = 0.5f;
    [SerializeField] bool _isCinematic;
    [SerializeField] QuestInfoSO doorTask;
    private bool _isOpen = false;

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
        if (_isOpen)
        {
            _isOpen = false;
            transform.DORotate(transform.eulerAngles + new Vector3(0, -90, 0), _rotationDuration);
        }
        else
        {
            _isOpen = true;
            transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), _rotationDuration);
            if(!_isCinematic) doorTask.CompleteTask();
        }
    }
}