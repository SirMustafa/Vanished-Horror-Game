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

    //private GameManager _gameManager;

    private void Awake()
    {
        //_gameManager = FindObjectOfType<GameManager>();
       
        //_gameManager.gameStateTypeChangedEvent.AddListener(OnGameStateTypeChanged);
        _currentSensitivity = _mouseSensitivity;

        //SetLockByGameState(_gameManager.GetGameStateType());
    }

   // private void SetLockByGameState(GameStateType gameStateType)
   // {
   //     Cursor.lockState = gameStateType == GameStateType.FreeRoam ? CursorLockMode.Locked : CursorLockMode.None;
   // }

    private void Update()
    {
        //if(_gameManager.GetGameStateType() != GameStateType.FreeRoam) return;
        RotateCamera();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void RotateCamera()
    {
        float mouseX = _lookInput.x * _currentSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _currentSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }

    public void SetSensitivity(bool isAiming)
    {
        if(isAiming)
        {
            _currentSensitivity = _aimSensitivity;
        }
        else
        {
            _currentSensitivity = _mouseSensitivity;
        }
    }
}