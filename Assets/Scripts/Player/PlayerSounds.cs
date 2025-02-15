using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private float baseFootstepInterval = 0.5f;
    [SerializeField] private float sprintMultiplier = 0.7f;
    [SerializeField] private AudiosSO allSounds;

    private CharacterController characterController;
    private float footstepTimer;
    private bool IsSprinting { get; set; }

    private void OnEnable()
    {
        EventBus.PlayerEvents.OnSprint += HandleSprint;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.velocity.magnitude > 0.1f && characterController.isGrounded)
        {
            footstepTimer += Time.deltaTime;
            float currentInterval = IsSprinting ? baseFootstepInterval * sprintMultiplier : baseFootstepInterval;

            if (footstepTimer >= currentInterval)
            {
                AudioManager.AudioInstance.PlaySfx(allSounds.FootStepSound());
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    private void HandleSprint(bool isSprinting)
    {
        IsSprinting = isSprinting;
    }

    private void OnDisable()
    {
        EventBus.PlayerEvents.OnSprint -= HandleSprint;
    }
}