using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager: MonoBehaviour
{
    [SerializeField] Transform cameraObject;
    [SerializeField] AudiosSO sounds;

    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject CreditsPanel;

    public enum Panels
    {
        Option,
        Main,
        Credits
    }
    public Panels CurrentPanel;

    void Start()
    {
        cameraObject.DORotate(new Vector3(0, 360, 0), 60f, RotateMode.FastBeyond360)
             .SetLoops(-1, LoopType.Incremental)
             .SetEase(Ease.Linear);

        AudioManager.AudioInstance.PlayAmbiance(sounds.AmbianceSound);
    }

    public void StartGame()
    {
        SceneTransition.Sceneinstance.NextLevel(1);
    }

    public void SetCurrentPanel(int whichPanel)
    {
        CurrentPanel = (Panels)whichPanel;

        OptionsPanel.SetActive(CurrentPanel is Panels.Option);
        MainPanel.SetActive(CurrentPanel is Panels.Main);
        CreditsPanel.SetActive(CurrentPanel is Panels.Credits);
    }

    public void OnEscBtn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CurrentPanel is not Panels.Main)
            {
                SetCurrentPanel((int)Panels.Main);
            }
        }
    }
}
