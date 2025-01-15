using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpForce;
    [SerializeField] GameObject aimingCamera;
    [SerializeField] MouseLook _mouseLookCs;

    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _movementInput;
    private Vector3 _verticalVelocity;  
    private float _currentSpeed;
    private float _gravityMultipier = 4;
    private float _groundCheckDistance;
    
    private bool _isGrounded;
    private static readonly int _VelocityXHash = Animator.StringToHash("VelocityX");
    private static readonly int _VelocityZHash = Animator.StringToHash("VelocityZ");

    //private GameManager _gameManager;

    private NavMeshAgent _navMeshAgent;
    //private Npc _npc;
    private void Awake()
    {
        //_gameManager = FindObjectOfType<GameManager>();
        
        //_npc = GetComponent<Npc>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _currentSpeed = _walkSpeed;
    }
    
    private void Update()
    {
        Vector3 normalizedMovement = new Vector3();
        //if(_gameManager.GetGameStateType() == GameStateType.FreeRoam)
        //{
        //    SetForCinematic(false);
        //    _isGrounded = _characterController.isGrounded;
        //    if (_isGrounded && _verticalVelocity.y < 0)
        //    {
        //        _verticalVelocity.y = -2f;
        //    }
        //    _verticalVelocity.y += _gravity * Time.deltaTime * _gravityMultipier;
        //    normalizedMovement = _movementInput.normalized;
        //    Vector3 movement = transform.TransformDirection(normalizedMovement) * _currentSpeed;
        //    movement.y = _verticalVelocity.y;
        //    _characterController.Move(movement * Time.deltaTime);
        //}
        //else if (_gameManager.GetGameStateType() == GameStateType.Cinematic)
        //{
        //    SetForCinematic(true);
        //    return;
        //}
        
        UpdateAnimatorParameters(normalizedMovement);

        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //if(Physics.Raycast(ray, out RaycastHit raycastHit,999f)) 
        //{
        //    transform.position = raycastHit.point;
        //}
    }


    void SetForCinematic(bool isCinematic)
    {
        //_npc.enabled = isCinematic;
        _navMeshAgent.enabled = isCinematic;
        _characterController.enabled = !isCinematic;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _movementInput = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _isGrounded)
        {
            _animator.SetTrigger("isJumping");
            _verticalVelocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetBool("isAiming",true);
            aimingCamera.SetActive(true);
            _mouseLookCs.SetSensitivity(true);
        }
        else if(context.canceled)
        {
            _animator.SetBool("isAiming", false);
            aimingCamera.SetActive(false);
            _mouseLookCs.SetSensitivity(false);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentSpeed = _runSpeed;
        }
        else if (context.canceled)
        {
            _currentSpeed = _walkSpeed;
        }
    }
    private void OnDrawGizmos()
    {
        Physics.CheckSphere(transform.position, _groundCheckDistance);
    }
    private void UpdateAnimatorParameters(Vector3 normalizedMovement)
    {
        float normalizedX = normalizedMovement.x;
        float normalizedZ = normalizedMovement.z;

        float speedFactor = _currentSpeed / _runSpeed;
        normalizedX *= speedFactor;
        normalizedZ *= speedFactor;

        _animator.SetFloat(_VelocityXHash, normalizedX, 0.1f, Time.deltaTime);
        _animator.SetFloat(_VelocityZHash, normalizedZ, 0.1f, Time.deltaTime);
    }
}