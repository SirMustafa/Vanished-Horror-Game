using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionRange = 3f;
    private PlayerUiManager _uiManager;
    private Inputs _inputs;
    private IInteractable _currentInteractable;
    private bool _isInteracting;

    [Inject]
    void InjectDependencies(PlayerUiManager uiManager, Inputs inputs)
    {
        _uiManager = uiManager;
        _inputs = inputs;
        _inputs.OnInteractEven += Interact;
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

    private void Interact()
    {
        _currentInteractable?.MyInterract();
    }
}
