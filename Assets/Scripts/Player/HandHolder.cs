using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    public void AttachToHand(GameObject pickedObject)
    {
        pickedObject.transform.SetParent(transform);
        pickedObject.transform.localPosition = Vector3.zero;
        pickedObject.transform.localRotation = Quaternion.identity;
    }
    public void SubscribeToPickable(IPickable pickable)
    {
        pickable.OnPickUpEvent.AddListener(AttachToHand);
    }
}