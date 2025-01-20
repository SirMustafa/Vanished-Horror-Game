using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Cam : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject virtualCamera;
    private Inputs _inputs;
    private PlayerUiManager _playerUi;

    [Inject]
    void InjectDependencies(PlayerUiManager playerUi, Inputs inputs)
    {
        _inputs = inputs;
        _playerUi = playerUi;
    }

    public void MyInterract()
    {
        _inputs.SwitchActionMap(Inputs.ActionMap.OnCamera);
        _playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.OnCameraPanel);
        virtualCamera.SetActive(true);
    }
    public void OnExit(InputAction.CallbackContext context)
    {
        _inputs.SwitchActionMap(Inputs.ActionMap.Player);
        _playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.GamePlayPanel);
        virtualCamera.SetActive(false);
    }
}
