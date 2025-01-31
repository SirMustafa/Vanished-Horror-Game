using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    Transform objestTransform;
    public void AttachToHand(GameObject pickedObject)
    {
        objestTransform = pickedObject.transform;
        objestTransform.transform.SetParent(transform);
        objestTransform.transform.localPosition = Vector3.zero;
        objestTransform.transform.localRotation = Quaternion.identity;
    }
    public void LeaveHand(GameObject pickedObject)
    {
        objestTransform.parent = null;
        objestTransform.localPosition = Vector3.zero;
        objestTransform.localRotation = Quaternion.identity;
        objestTransform = null;
    }
}