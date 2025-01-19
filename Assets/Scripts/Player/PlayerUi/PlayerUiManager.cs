using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Panels;

    Inputs _gameInputs;
    SubtitleManager _subtitle;
    int _panelsCount;

    public enum UiPanels
    {
        GamePlayPanel,
        PausePanel,
        SubtitlePanel
    }
    public UiPanels CurrentPanel;

    [Inject]
    void InjectDependencies(Inputs inputs, SubtitleManager sub)
    {
        _gameInputs = inputs;
        _subtitle = sub;
        _gameInputs.OnLeftMouseEvent += CheckLeftMouseInput;
    }

    private void Start()
    {
        _panelsCount = Panels.Count;
    }

    public void CheckLeftMouseInput()
    {
        if (CurrentPanel == UiPanels.SubtitlePanel)
        {
            _subtitle.SetDialouge();
        }
    }

    public void SetCurrentPanel(UiPanels whichPanelisActive)
    {
        CurrentPanel = whichPanelisActive;

        for (int i = 0; i < _panelsCount; i++)
        {
            Panels[i].SetActive(i == (int)whichPanelisActive);
        }
    }
}