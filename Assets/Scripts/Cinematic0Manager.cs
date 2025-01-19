using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic0Manager : MonoBehaviour
{
    [SerializeField] SubtitlesSO cinematicLines;
    [SerializeField] PlayerUiManager playerUi;
    [SerializeField] Inputs gameInputs;
    void Start()
    {
        playerUi.SetCurrentPanel(PlayerUiManager.UiPanels.SubtitlePanel);
    }
}
