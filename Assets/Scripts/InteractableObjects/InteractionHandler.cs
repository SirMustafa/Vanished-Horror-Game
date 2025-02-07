using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionRange = 3f;

    private IInteractable _currentInteractable;
    private IInteractable _handInteractable;
    private PlayerUiManager _uiManager;
    private PlayerController _playerController;
    private bool _isInteracting;

    [Inject]
    void InjectDependencies(PlayerUiManager uiManager, PlayerController player)
    {
        _uiManager = uiManager;
        _playerController = player;
    }

    private void OnEnable()
    {
        EventBus.InteractionEvents.OnInteract += Interact;
    }

    private void Update()
    {
        CheckForInteractable();
    }
    private void CheckForInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer);

        if (hasHit != _isInteracting)
        {
            _isInteracting = hasHit;
            _uiManager.ShowInteractSymbol(_isInteracting);

            if (hasHit)
            {
                _currentInteractable = hit.collider.GetComponent<IInteractable>();
            }
            else
            {
                _currentInteractable = null;
            }
        }
    }

    public void SetHandObject(IInteractable handObject)
    {
        _handInteractable = handObject;
    }

    public void DropObject()
    {
        _handInteractable = null;
    }

    private void Interact()
    {
        if (_handInteractable is not null)
        {
            _handInteractable.Interract();
            return;
        }

        if (_currentInteractable is not null)
        {
            if (_currentInteractable.CanBePickedUp())
            {
                _playerController.HandlePickAnimation(_currentInteractable);
            }
            else
            {
                _currentInteractable.Interract();
            }
        }
    }

    IEnumerator waitforAnimation()
    {
        yield return null;
    }

    private void OnDisable()
    {
        EventBus.InteractionEvents.OnInteract -= Interact;
    }
}