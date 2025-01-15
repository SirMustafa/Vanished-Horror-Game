using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GameObject aimingCamera;
    [SerializeField] private MouseLook _mouseLookCs;
    [SerializeField] private Inputs _inputs;

    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _movementInput;
    private Vector3 _verticalVelocity;
    private float _currentSpeed;
    private bool _isGrounded;

    private static readonly int _VelocityXHash = Animator.StringToHash("VelocityX");
    private static readonly int _VelocityZHash = Animator.StringToHash("VelocityZ");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _currentSpeed = _walkSpeed;

        _inputs.OnMoveEvent += HandleMovement;
        _inputs.OnJumpEvent += HandleJump;
        _inputs.OnAimingEvent += HandleAiming;
        _inputs.OnSprintEvent += HandleSprint;
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
        }

        _verticalVelocity.y += _gravity * Time.deltaTime;

        Vector3 movement = transform.TransformDirection(_movementInput) * _currentSpeed;
        movement.y = _verticalVelocity.y;

        _characterController.Move(movement * Time.deltaTime);
        UpdateAnimatorParameters();
    }

    private void HandleMovement(Vector2 input)
    {
        _movementInput = new Vector3(input.x, 0, input.y);
    }

    private void HandleJump()
    {
        if (_isGrounded)
        {
            _animator.SetTrigger("isJumping");
            _verticalVelocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }
    }

    private void HandleAiming(bool isAiming)
    {
        _animator.SetBool("isAiming", isAiming);
        aimingCamera.SetActive(isAiming);
        _mouseLookCs.SetSensitivity(isAiming);
    }

    private void HandleSprint(bool isSprinting)
    {
        _currentSpeed = isSprinting ? _runSpeed : _walkSpeed;
    }

    private void UpdateAnimatorParameters()
    {
        float velocityX = _movementInput.x * (_currentSpeed / _runSpeed);
        float velocityZ = _movementInput.z * (_currentSpeed / _runSpeed);

        _animator.SetFloat(_VelocityXHash, velocityX, 0.1f, Time.deltaTime);
        _animator.SetFloat(_VelocityZHash, velocityZ, 0.1f, Time.deltaTime);
    }
}