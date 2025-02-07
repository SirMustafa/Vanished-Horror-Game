using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.InputEvents.OnFrameChange += SetMyPosition;
    }

    private void SetMyPosition(float xAxis)
    {
        this.transform.DOMoveX(xAxis, 0.2f);
    }

    private void OnDisable()
    {
        EventBus.InputEvents.OnFrameChange -= SetMyPosition;
    }
}