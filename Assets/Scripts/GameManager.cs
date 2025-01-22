using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        None,
        PlayState,
        SubtitleState,
        TabState,
        CinematicState,
        PauseState,
    }
    public GameState CurrentGameState { get; private set; }
    private PlayerUiManager playerUi;

    [Inject]
    void InjectDependencies(PlayerUiManager playerUiManager)
    {
        playerUi = playerUiManager;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeGameState(GameState.PlayState);
        StartCoroutine(waiter(1));    
    }

    IEnumerator waiter(float howMuchTime)
    {
        yield return new WaitForSeconds(howMuchTime);
        ChangeGameState(GameState.SubtitleState);
    }

    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
        playerUi.SetCurrentPanel((PlayerUiManager.UiPanels)newState);

        switch (newState)
        {
            case GameState.SubtitleState:
                playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.SubtitlePanel);
                break;
            case GameState.PlayState:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.PauseState:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void PauseGame()
    {
        if (CurrentGameState == GameState.PauseState)
        {
            ChangeGameState(GameState.PlayState);
        }
        else
        {
            ChangeGameState(GameState.PauseState);
        }
    }
}