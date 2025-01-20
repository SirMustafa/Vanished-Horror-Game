using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnCameraInputs : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float maxVerticalAngle = 80f;
    private Vector2 _lookInput;
    private float xRotation = 0f;

    private void Update()
    {
        RotateCamera();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void RotateCamera()
    {
        float mouseX = -_lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = -_lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}