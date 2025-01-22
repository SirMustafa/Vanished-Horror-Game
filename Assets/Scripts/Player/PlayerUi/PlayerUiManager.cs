using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Panels;
    [SerializeField] SubtitlesSO[] _subtitles;
    [SerializeField] SubtitleManager _subtitleManager;
    [SerializeField] GameObject _interractSymbol;
    [SerializeField] GameObject _corshair;

    Inputs _gameInputs;
    int _panelsCount;
    int subtitleCount = 0;

    public enum UiPanels
    {
        None,
        GamePlayPanel,
        SubtitlePanel,
        TabPanel,
        CinematicPanel,
        PausePanel,   
        OnCameraPanel
    }
    public UiPanels CurrentPanel { get; private set; } = UiPanels.None;

    [Inject]
    void InjectDependencies(Inputs inputs)
    {
        _gameInputs = inputs;
        _gameInputs.OnLeftMouseEvent += CheckLeftMouseInput;
    }

    private void Start()
    {
        _subtitleManager.OnDialogueFinished += EndDialouge;
        _panelsCount = Panels.Count;
    }

    private void CheckLeftMouseInput()
    {
        if (CurrentPanel == UiPanels.SubtitlePanel)
        {
            _subtitleManager.CallNextLine();
        }
    }

    public void ShowInteractSymbol(bool isInterracting)
    {
        _interractSymbol.SetActive(isInterracting);
        _corshair.SetActive(!isInterracting);
    }

    void EndDialouge()
    {
        SetCurrentPanel(UiPanels.GamePlayPanel);
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
            _subtitleManager.SetAndStart(_subtitles[subtitleCount]);
            subtitleCount++;
        }
    }

    public void RestartGame()
    {

    }
    public void ResumeGame()
    {

    }
    public void MainMenu()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}