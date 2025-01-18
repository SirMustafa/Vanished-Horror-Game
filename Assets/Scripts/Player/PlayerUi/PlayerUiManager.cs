using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiManager : MonoBehaviour
{
    int panelsCount;
    public enum UiPanels
    {
        GamePlayPanel,
        PausePanel,
        SubtitlePanel
    }
    public UiPanels CurrentPanel;

    private void Start()
    {
        panelsCount = Enum.GetValues(typeof(UiPanels)).Length;
    }

    public void CheckLeftMouseInput()
    {
        if(CurrentPanel == UiPanels.SubtitlePanel)
        {

        }
    }

    public void SetCurrentPanel(UiPanels whichPanelisActive)
    {
        CurrentPanel = whichPanelisActive;

        for (int i = 0; i < panelsCount; i++)
        {

        }
    }
}