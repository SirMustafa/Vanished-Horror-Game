using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    [SerializeField] InteractionHandler _interactionHandler;
    Transform objestTransform;
    
    public void AttachToHand(GameObject pickedObject)
    {
        objestTransform = pickedObject.transform;
        objestTransform.transform.SetParent(transform);
        objestTransform.transform.localPosition = Vector3.zero;
        objestTransform.transform.localRotation = Quaternion.identity;
    }
    public void LeaveHand()
    {
        if (objestTransform != null) 
        {
            _interactionHandler.DropObject();
            objestTransform.parent = null;
            objestTransform = null;
        }
        
    }
}