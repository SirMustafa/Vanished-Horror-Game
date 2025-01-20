using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Panels;
    [SerializeField] SubtitlesSO[] subtitles;
    [SerializeField] SubtitleManager _subtitle;
    [SerializeField] GameObject _interractSymbol;
    [SerializeField] GameObject _corshair;
    Inputs _gameInputs;
    int _panelsCount;
    int subtitleCount = 0;

    public enum UiPanels
    {
        GamePlayPanel,
        PausePanel,
        SubtitlePanel,
        OnCameraPanel
    }
    public UiPanels CurrentPanel;

    [Inject]
    void InjectDependencies(Inputs inputs)
    {
        _gameInputs = inputs;
        _gameInputs.OnLeftMouseEvent += CheckLeftMouseInput;
    }

    private void Start()
    {
        _panelsCount = Panels.Count;
    }

    private void CheckLeftMouseInput()
    {
        if (CurrentPanel == UiPanels.SubtitlePanel)
        {
            _subtitle.CallNextLine();
        }
    }

    public void ShowInteractSymbol(bool isInterracting)
    {
        _interractSymbol.SetActive(isInterracting);
        _corshair.SetActive(!isInterracting);
    }

    public void SetCurrentPanel(UiPanels whichPanelisActive)
    {
        CurrentPanel = whichPanelisActive;

        for (int i = 0; i < _panelsCount; i++)
        {
            Panels[i].SetActive(i == (int)whichPanelisActive);
        }
        if (CurrentPanel == UiPanels.SubtitlePanel)
        {
            _subtitle.SetAndStart(subtitles[subtitleCount]);
            subtitleCount++;
        }
    }
}