using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    int index = 0;
    public enum GameState
    {
        Subtitle,
        PlayTime,
        CinematicTime,
        Pause
    }
    public GameState GameStates;
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
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(waiter(2));
        
    }

    IEnumerator waiter(float howMuchTime)
    {
        yield return new WaitForSeconds(howMuchTime);
        StartTask(GameState.Subtitle);
    }

    public void StartTask(GameState whichState)
    {
        switch (GameStates)
        {
            case GameState.Subtitle:
                playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.SubtitlePanel);
                break;

            case GameState.PlayTime:
                break;

            case GameState.CinematicTime:
                break;

            case GameState.Pause:
                break;

            default:
                break;
        }
    }

    public void ChangeGameState(GameState whichState)
	{
		if(whichState == GameState.PlayTime)
		{
            Cursor.lockState = CursorLockMode.Locked;
        }
	}  
}