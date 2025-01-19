using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic0Manager : MonoBehaviour
{
    [SerializeField] PlayerUiManager playerUi; 
    [SerializeField] Inputs gameInputs;

    public void ActivateSubtitlePanel()
    {
        playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.SubtitlePanel);
    }
}