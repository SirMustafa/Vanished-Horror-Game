using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemFrame> frames = new List<ItemFrame>();
    [SerializeField] private List<IInteractable> items = new List<IInteractable>(5);
    [SerializeField] private Transform itemSelector;

    private ItemFrame currentFrame;
    private int currentIndex = 0;
    private const int MaxInventorySize = 5;

    private void OnEnable()
    {
        EventBus.CameraEvents.OnScroll += OnScroll;
    }

    private void Start()
    {
        SetCurrentFrame(currentIndex);
    }

    public bool AddToInventory(IInteractable obj)
    {
        if (items.Count >= MaxInventorySize)
        {
            return false;
        }

        items.Add(obj);
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < frames.Count; i++)
        {
            if (i < items.Count)
            {
                Sprite itemSprite = items[i].GetSprite();
                frames[i].SetItem(itemSprite);
            }
            else
            {
                frames[i].ClearItem();
            }
        }
    }

    public bool IsInventoryFull()
    {
        return items.Count >= MaxInventorySize;
    }

    private void OnScroll(float scrollValue)
    {
        if (scrollValue < 0 && currentIndex < frames.Count - 1)
        {
            currentIndex++;
            SetCurrentFrame(currentIndex);
        }
        else if (scrollValue > 0 && currentIndex > 0)
        {
            currentIndex--;
            SetCurrentFrame(currentIndex);
        }
    }

    private void SetCurrentFrame(int whichFrame)
    {
        if (currentFrame != null)
        {
            currentFrame.Deselect();
        }

        currentFrame = frames[whichFrame];
        itemSelector.SetParent(currentFrame.transform);
        itemSelector.localPosition = Vector2.zero;

        currentFrame.Select();
    }

    private void OnDisable()
    {
        EventBus.CameraEvents.OnScroll -= OnScroll;
    }
}
