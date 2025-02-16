using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{
    [SerializeField] private float _rotationDuration = 0.5f;
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    private OcclusionPortal _occlusionPortal;
    private bool _isOpen = false;
    private bool _isMoving;

    private void Awake()
    {
        _occlusionPortal = GetComponent<OcclusionPortal>();
    }
    public bool CanBePickedUp()
    {
        return false;
    }

    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetInteractionTarget()
    {
        return this.transform;
    }

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public void MyInterract()
    {
        if (_isMoving) return;

        _isMoving = true;

        Vector3 targetRotation = _isOpen
            ? transform.eulerAngles + new Vector3(0, -90, 0)
            : transform.eulerAngles + new Vector3(0, 90, 0);

        transform.DORotate(targetRotation, _rotationDuration)
            .OnComplete(() => _isMoving = false);

        _isOpen = !_isOpen;
        _occlusionPortal.open = _isOpen;

        AudioClip clipToPlay = _isOpen ? _openSound : _closeSound;
        AudioManager.AudioInstance.PlaySfx(clipToPlay);
    }
}