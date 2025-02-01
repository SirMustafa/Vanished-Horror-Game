using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerUiManager : MonoBehaviour
{
    public event Action OnDialogueFinished;

    [SerializeField] List<GameObject> Panels;
    [SerializeField] SubtitleManager _subtitleManager;
    [SerializeField] GamePanel _gamePanelManager;
    [SerializeField] GameObject _interractSymbol;
    [SerializeField] GameObject _corshair;

    Inputs _gameInputs;
    int _panelsCount;

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
        _gameInputs.OnScrollingEvent += Scrolling;
    }

    private void Start()
    {
        _panelsCount = Panels.Count;
    }

    private void CheckLeftMouseInput()
    {
        if (CurrentPanel == UiPanels.SubtitlePanel)
        {
            _subtitleManager.CallNextLine();
        }
    }

    private void Scrolling(float scrollvalue)
    {
        if (CurrentPanel == UiPanels.GamePlayPanel)
        {
            _gamePanelManager.Scrolling(scrollvalue);
        }
    }

    public void ShowInteractSymbol(bool isInterracting)
    {
        _interractSymbol.SetActive(isInterracting);
        _corshair.SetActive(!isInterracting);
    }

    public void OnEndSubtitle()
    {
        SetCurrentPanel(UiPanels.GamePlayPanel);
    }

    public void ShowSubtitle(SubtitlesSO whichSubtitle)
    {
        SetCurrentPanel(UiPanels.SubtitlePanel);
        _subtitleManager.SetAndStart(whichSubtitle);
    }

    public void SetMissionText(string text)
    {
        _subtitleManager.SetMissionText(text);
    }

    public void SetCurrentPanel(UiPanels whichPanelisActive)
    {
        if (CurrentPanel == whichPanelisActive) return;

        CurrentPanel = whichPanelisActive;

        for (int i = 0; i < _panelsCount; i++)
        {
            Panels[i].SetActive(i == (int)whichPanelisActive);
        }
    }

    public void RestartGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneTransition.Sceneinstance.NextLevel(currentScene);
    }

    public void MainMenu()
    {
        SceneTransition.Sceneinstance.NextLevel(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}