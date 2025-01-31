using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : InteractableBase
{
    [SerializeField] private float _rotationDuration = 0.5f;
    [SerializeField] QuestInfoSO doorTask;
    private bool _isOpen = false;

    public override void MyInterract()
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
            doorTask.CompleteTask();
        }
    }
}