using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        ChangeGameState(GameState.PlayTime);
    }
    public enum GameState
	{
		PlayTime,
		CinematicTime,
		Pause
	}
	public GameState GameStates;

	public void ChangeGameState(GameState whichState)
	{
		if(whichState == GameState.PlayTime)
		{
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}