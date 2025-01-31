using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Cam : InteractableBase
{
    [SerializeField] GameObject virtualCamera;
    private Inputs _inputs;
    private GameManager _gameManager;

    [Inject]
    void InjectDependencies(GameManager gamemanager, Inputs inputs)
    {
        _inputs = inputs;
        _gameManager = gamemanager;
    }

    public override void MyInterract()
    {
        _inputs.SwitchActionMap(Inputs.ActionMap.OnCamera);
        _gameManager.ChangeGameState(GameManager.GameState.OnCameraState);
        virtualCamera.SetActive(true);
    }
    public void OnExit(InputAction.CallbackContext context)
    {
        _inputs.SwitchActionMap(Inputs.ActionMap.Player);
        _gameManager.ChangeGameState(GameManager.GameState.PlayState);
        virtualCamera.SetActive(false);
    }
}
