using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<ItemFrame> frames = new List<ItemFrame>();
    [SerializeField] Transform itemSeceltor;
    ItemFrame CurrentFrame;
    Inputs _inputs;
    int currentIndex = 0;

    [Inject]
    void InjectDependencies(Inputs inputs)
    {
        _inputs = inputs;
        _inputs.OnScrollEvent += OnScroll;
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
}
