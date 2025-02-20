using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUiManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Panels;
    [SerializeField] SubtitleManager _subtitleManager;
    [SerializeField] GameObject _interractSymbol;
    [SerializeField] GameObject _corshair;
    int _panelsCount;

    public enum UiPanels
    {
        SubtitlePanel,
        GamePlayPanel,
        CinematicPanel,
        PausePanel,
        OnCameraPanel,
        OnChairPanel,
    }
    public UiPanels CurrentPanel { get; private set; } = UiPanels.OnChairPanel;

    private void Start()
    {
        _panelsCount = Panels.Count;
    }

    public void ShowInteractSymbol(bool isInterracting)
    {
        _interractSymbol.SetActive(isInterracting);
        _corshair.SetActive(!isInterracting);
    }

    public void OnEndSubtitle()
    {
        EventBus.InputEvents.TriggerGameStateChange(GameManager.GameState.PlayState);
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
    public void BtnSound(AudioClip btnSfx)
    {
        AudioManager.AudioInstance.PlaySfx(btnSfx);
    }
}