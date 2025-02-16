using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnCameraInputs : MonoBehaviour
{
    [SerializeField] private float maxVerticalAngle = 80f;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float rayDistance;

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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            impulseSource.GenerateImpulse();
            ShootRay();
        }
    }

    private void RotateCamera()
    {
        float mouseX = -_lookInput.x * Time.deltaTime;
        float mouseY = -_lookInput.y * Time.deltaTime;

        xRotation -= mouseY;

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

    private void ShootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Debug.Log("Çarpýlan Obje: " + hit.collider.gameObject.name);
        }
    }
}