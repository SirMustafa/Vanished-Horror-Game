using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager: MonoBehaviour
{
    [SerializeField] Transform cameraObject;
    void Start()
    {
        cameraObject.DORotate(new Vector3(0, 360, 0), 60f, RotateMode.FastBeyond360)
             .SetLoops(-1, LoopType.Incremental)
             .SetEase(Ease.Linear);
    }

    public void StartGame()
    {
        SceneTransition.Sceneinstance.NextLevel(1);
    }
}
