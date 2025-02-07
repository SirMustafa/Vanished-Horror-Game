using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    [SerializeField] InteractionHandler _interactionHandler;
    private Transform objectTransform;

    public void AttachToHand(GameObject pickedObject)
    {
        if (pickedObject is null) return;

        pickedObject.SetActive(true);
        objectTransform = pickedObject.transform;
        objectTransform.SetParent(transform);
        objectTransform.localPosition = Vector3.zero;
        //objectTransform.localRotation = Quaternion.identity;
    }

    public void DetachFromHand()
    {
        if (objectTransform is not null)
        {
            objectTransform.gameObject.SetActive(false);
            objectTransform = null;
        }
    }


    public void DropObject()
    {
        if (objectTransform is not null)
        {

            GameObject droppedObject = objectTransform.gameObject;
            Rigidbody rb = droppedObject.GetComponent<Rigidbody>();

            if (rb is not null)
            {
                rb.isKinematic = false;
                rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
            }

            droppedObject.transform.SetParent(null);
            objectTransform = null;
        }
    }
}