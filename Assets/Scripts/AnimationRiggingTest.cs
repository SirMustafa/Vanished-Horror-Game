using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRiggingTest : MonoBehaviour
{
    public Transform rightHandTarget;
    public Transform cup;
    public Rig handRig;
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
            {
                PickUp();
            }
            else
            {
                Drop();
            }
        }
    }

    void PickUp()
    {
        cup.SetParent(rightHandTarget);
        cup.localPosition = Vector3.zero;
        cup.localRotation = Quaternion.identity;
        handRig.weight = 1f;
        isHolding = true;
    }

    void Drop()
    {
        cup.SetParent(null);
        handRig.weight = 0f;
        isHolding = false;
    }
}