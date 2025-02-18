using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isTesting;
    [SerializeField] private QuestInfoSO _firstQuest;

    public enum GameState
    {
        SubtitleState,
        PlayState,
        TaskState,
        CinematicState,
        PauseState,
        OnCameraState,
        OnChair,
    }

    public GameState CurrentGameState { get; private set; }
    private Stack<GameState> _gameStateStack = new Stack<GameState>();
    private PlayerUiManager _playerUi;
    private QuestManager _taskManager;

    private Dictionary<GameState, PlayerUiManager.UiPanels> _gameStateToUiPanel = new()
    {
        { GameState.SubtitleState, PlayerUiManager.UiPanels.SubtitlePanel },
        { GameState.PlayState, PlayerUiManager.UiPanels.GamePlayPanel },
        { GameState.TaskState, PlayerUiManager.UiPanels.TaskPanel },
        { GameState.CinematicState, PlayerUiManager.UiPanels.CinematicPanel },
        { GameState.PauseState, PlayerUiManager.UiPanels.PausePanel },
        { GameState.OnCameraState, PlayerUiManager.UiPanels.OnCameraPanel },
        { GameState.OnChair, PlayerUiManager.UiPanels.OnChairPanel }
    };

    [Inject]
    void InjectDependencies(PlayerUiManager playerUiManager, QuestManager taskManager)
    {
        _playerUi = playerUiManager;
        _taskManager = taskManager;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EventBus.InputEvents.OnGameStateChange += ChangeGameState;
    }

    private void Start()
    {
        ChangeGameState(GameState.PlayState);
        if (_isTesting) return;
        Invoke("StartQuestCycle", 1f);
    }

    private void StartQuestCycle()
    {
        _taskManager.SetStartQuest(_firstQuest);
    }

    private void ChangeGameState(GameState newState)
    {
        if (CurrentGameState == newState) return;

        _gameStateStack.Push(CurrentGameState);
        CurrentGameState = newState;

        if (_gameStateToUiPanel.TryGetValue(newState, out var panel))
        {
            _playerUi.SetCurrentPanel(panel);
        }

        switch (newState)
        {
            case GameState.PlayState:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                EventBus.InputEvents.TriggerActionMapChange(Inputs.ActionMap.Player);
                break;

            case GameState.PauseState:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case GameState.TaskState:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void PauseGame()
    {
        if(_isTesting) return;
        if (CurrentGameState == GameState.PauseState)
        {
            ChangeGameState(_gameStateStack.Pop());
        }
        else
        {
            ChangeGameState(GameState.PauseState);
        }
    }

    private void OnDisable()
    {
        EventBus.InputEvents.OnGameStateChange -= ChangeGameState;
    }
}