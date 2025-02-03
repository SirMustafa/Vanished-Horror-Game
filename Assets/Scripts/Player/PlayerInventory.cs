using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<ItemFrame> frames = new List<ItemFrame>();
    [SerializeField] Transform itemSeceltor;
    ItemFrame CurrentFrame;
    int currentIndex = 0;

    private void OnEnable()
    {
        EventBus.CameraEvents.OnScroll += OnScroll;
    }

    private void Start()
    {
        SetCurrentFrame(currentIndex);
    }

    public void PickUp(IInteractable obj)
    {

    }

    public void OnScroll(float scrollValue)
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

    public void SetCurrentFrame(int whichFrame)
    {
        if (CurrentFrame is not null)
        {
            CurrentFrame.Deselect();
        }

        CurrentFrame = frames[whichFrame];
        itemSeceltor.SetParent(CurrentFrame.transform);
        itemSeceltor.localPosition = Vector2.zero;

        CurrentFrame.Select();
    }
    private void OnDisable()
    {
        EventBus.CameraEvents.OnScroll -= OnScroll;
    }
}
