using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _aimSensitivity;
    [SerializeField] private Transform _playerBody;

    private Vector2 _lookInput;
    private float _xRotation = 0f;
    private float _currentSensitivity;
    private bool _canRotate = true;

    private void Awake()
    {
        _currentSensitivity = _mouseSensitivity;

        //daha sonra lockstate gamemanager a taþýncak
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_canRotate)
        {
            RotateCamera();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void RotateCamera()
    {
        float mouseX = _lookInput.x * _currentSensitivity;
        float mouseY = _lookInput.y * _currentSensitivity;

        _xRotation = Mathf.Clamp(_xRotation - mouseY, -80f, 80f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.rotation *= Quaternion.Euler(0f, mouseX, 0f);
    }

    public void SetSensitivity(bool isAiming)
    {
        _currentSensitivity = isAiming ? _aimSensitivity : _mouseSensitivity;
    }

    public void SetCanRotate(bool value)
    {
        _canRotate = value;
    }
}