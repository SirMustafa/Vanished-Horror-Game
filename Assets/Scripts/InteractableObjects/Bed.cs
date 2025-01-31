using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableBase
{
    bool isOpened = false;
    float _rotationDuration = 1f;

    public override void MyInterract()
    {
        if (isOpened)
        {
            transform.DORotate(new Vector3(0, -90, 0), _rotationDuration);
            isOpened = false;
        }
        else
        {
            transform.DORotate(new Vector3(90, 0, 90), _rotationDuration);
            isOpened = true;
        }
    }
}