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
    private bool _isInteracting;

    [Inject]
    void InjectDependencies(PlayerUiManager uiManager)
    {
        _uiManager = uiManager;
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
        //Debug.DrawRay(ray.origin, ray.direction * interactionRange, hasHit ? Color.green : Color.red, 0.1f);

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

    public void PickObject(IInteractable handObject)
    {
        if (_handInteractable is null)
        {
            _handInteractable = handObject;
        }
    }

    public void DropObject()
    {
        _handInteractable = null;
    }

    private void Interact()
    {
        if (_handInteractable is not null)
        {
            _handInteractable.MyInterract();
        }
        else
        {
          _currentInteractable?.MyInterract();
        }
    }
    private void OnDisable()
    {
        EventBus.InteractionEvents.OnInteract -= Interact;
    }
}
