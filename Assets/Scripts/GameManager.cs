using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<SubtitlesSO> _subtitles = new List<SubtitlesSO>();

    public enum GameState
    {
        None,
        PlayState,
        TabState,
        CinematicState,
        PauseState,
        OnCameraState
    }

    public GameState CurrentGameState { get; private set; }
    private PlayerUiManager _playerUi;
    private QuestManager _taskManager;  

    [Inject]
    void InjectDependencies(PlayerUiManager playerUiManager, QuestManager taskmanager)
    {  
        _playerUi = playerUiManager;
        _taskManager = taskmanager;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeGameState(GameState.PlayState);
        Invoke("StartQuestCycle", 1f);
    }

    void StartQuestCycle()
    {
        _taskManager.StartQuest();
    }
    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
        _playerUi.SetCurrentPanel((PlayerUiManager.UiPanels)newState);

        switch (newState)
        {
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