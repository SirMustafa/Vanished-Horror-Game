using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] List<ItemFrame> frames = new List<ItemFrame>();
    [SerializeField] Transform itemSeceltor;
    ItemFrame CurrentFrame;
    int currentIndex = 0;

    public void Scrolling(float value)
    {
        if (value > 0)
        {
            if (currentIndex < frames.Count - 1)
            {
                currentIndex++;
                SetCurrentFrame(currentIndex);
            }
        }
        else if (value < 0)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                SetCurrentFrame(currentIndex);
            }
        }
    }
    public void SetCurrentFrame(int whichFrame)
    {
        CurrentFrame = frames[whichFrame];
        itemSeceltor.SetParent(CurrentFrame.transform);
        itemSeceltor.localPosition = Vector2.zero;


    }
}
