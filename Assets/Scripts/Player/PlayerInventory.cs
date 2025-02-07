using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemFrame> _frames = new List<ItemFrame>();
    [SerializeField] private HandHolder _handHolder;
    [SerializeField] private InteractionHandler _interactHandler;

    private Dictionary<int, IInteractable> _inventorySlots = new Dictionary<int, IInteractable>();
    private ItemFrame _currentFrame;
    private int _currentIndex = 0;
    private const int _maxInventorySize = 5;

    private void OnEnable()
    {
        EventBus.CameraEvents.OnScroll += OnScroll;
        EventBus.InteractionEvents.OnDrop += RemoveInventory;
    }

    private void Start()
    {
        SetCurrentFrame(_currentIndex);
        EquipCurrentFrameItem();
    }

    public void AddToInventory(IInteractable obj)
    {
        if (_inventorySlots.Count >= _maxInventorySize && !_inventorySlots.ContainsKey(_currentIndex))
        {
            return;
        }

        _inventorySlots[_currentIndex] = obj;

        GameObject itemGO = obj.GetGameObject();
        if (itemGO != null)
        {
            Rigidbody rb = itemGO.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            itemGO.SetActive(false);
        }

        UpdateUI();
        EquipCurrentFrameItem();
    }

    private void RemoveInventory()
    {
        if (_inventorySlots.ContainsKey(_currentIndex))
        {
            _inventorySlots.Remove(_currentIndex);
            UpdateUI();
        }

        _handHolder.DropObject();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _frames.Count; i++)
        {
            if (_inventorySlots.ContainsKey(i))
            {
                Sprite itemSprite = _inventorySlots[i].GetSprite();
                _frames[i].SetItem(itemSprite);
            }
            else
            {
                _frames[i].ClearItem();
            }
        }
    }

    private void OnScroll(float scrollValue)
    {
        if (scrollValue < 0 && _currentIndex < _frames.Count - 1)
        {
            _currentIndex++;
            SetCurrentFrame(_currentIndex);
        }
        else if (scrollValue > 0 && _currentIndex > 0)
        {
            _currentIndex--;
            SetCurrentFrame(_currentIndex);
        }
        EquipCurrentFrameItem();
    }

    private void SetCurrentFrame(int whichFrame)
    {
        if (_currentFrame != null)
        {
            _currentFrame.Deselect();
        }
        _currentFrame = _frames[whichFrame];
        EventBus.InputEvents.TriggerFrameChangeEvent(_currentFrame.transform.position.x);
        _currentFrame.Select();
    }

    private void EquipCurrentFrameItem()
    {
        if (_inventorySlots.ContainsKey(_currentIndex))
        {
            IInteractable currentItem = _inventorySlots[_currentIndex];
            _interactHandler.SetHandObject(currentItem);
            _handHolder.AttachToHand(currentItem.GetGameObject());
        }
        else
        {
            _handHolder.DetachFromHand();
        }
    }

    private void OnDisable()
    {
        EventBus.CameraEvents.OnScroll -= OnScroll;
        EventBus.InteractionEvents.OnDrop -= RemoveInventory;
    }
}