using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _handikTarget;
    [SerializeField] private GameObject _aimingCamera;
    [SerializeField] private GameObject _onChairCamera;
    [SerializeField] private MouseLook _mouseLookCs;

    private InteractionHandler _interactionHandler;
    private CharacterController _characterController;
    private PlayerInventory _playerInventory;
    private Animator _animator;
    private Vector3 _movementInput;
    private Vector3 _verticalVelocity;
    private Vector3 _sittingPosition;
    private float _currentSpeed;
    private bool _isGrounded;
    private bool _isSitting = false;
    private static readonly int _VelocityXHash = Animator.StringToHash("VelocityX");
    private static readonly int _VelocityZHash = Animator.StringToHash("VelocityZ");

    [Inject]
    void InjectDependencies(PlayerInventory playerýnventory, InteractionHandler interact)
    {
        _playerInventory = playerýnventory;
        _interactionHandler = interact;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _currentSpeed = _walkSpeed;
    }

    private void OnEnable()
    {
        EventBus.PlayerEvents.OnMove += HandleMovement;
        EventBus.PlayerEvents.OnJump += HandleJump;
        EventBus.PlayerEvents.OnSprint += HandleSprint;
        EventBus.CameraEvents.OnAiming += HandleAiming;
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

        if (!_isSitting)
        {
            _characterController.Move(movement * Time.deltaTime);
            UpdateAnimatorParameters();
        }  
    }

    public void SitChair(Vector3 chairPosition, IInteractable chair)
    {
        if (!_isSitting)
        {
            _isSitting = true;
            _sittingPosition = transform.position;
            _characterController.enabled = false;
            EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.OnChair);
            EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.OnChair);
            _interactionHandler.SetHandObject(chair);
            this.transform.position = chairPosition;
            _animator.SetBool("isSitting", true);
        }
        else
        {
            _isSitting = false;
            EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.Player);
            EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
            _interactionHandler.DropObject();
            this.transform.position = _sittingPosition;
            _animator.SetBool("isSitting", false);
            _characterController.enabled = true;
        }
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
        _aimingCamera.SetActive(isAiming);
        _mouseLookCs.SetSensitivity(isAiming);
    }

    private void HandleSprint(bool isSprinting)
    {
        _currentSpeed = isSprinting ? _runSpeed : _walkSpeed;
    }

    public void HandlePickAnimation(IInteractable obj)
    {
        _handikTarget.position = obj.GetInteractionTarget().position;
        _handikTarget.rotation = obj.GetInteractionTarget().rotation;
        _animator.SetTrigger("isPickingUp");
        StartCoroutine(deleteObjFromScene(obj));
    }
    IEnumerator deleteObjFromScene(IInteractable obje)
    {
        yield return new WaitForSeconds(0.5f);
        _playerInventory.AddToInventory(obje);
    }

    private void UpdateAnimatorParameters()
    {
        float velocityX = _movementInput.x * (_currentSpeed / _runSpeed);
        float velocityZ = _movementInput.z * (_currentSpeed / _runSpeed);

        _animator.SetFloat(_VelocityXHash, velocityX, 0.1f, Time.deltaTime);
        _animator.SetFloat(_VelocityZHash, velocityZ, 0.1f, Time.deltaTime);
    }

    private void OnDisable()
    {
        EventBus.PlayerEvents.OnMove -= HandleMovement;
        EventBus.PlayerEvents.OnJump -= HandleJump;
        EventBus.PlayerEvents.OnSprint -= HandleSprint;
        EventBus.CameraEvents.OnAiming -= HandleAiming;
    }
}