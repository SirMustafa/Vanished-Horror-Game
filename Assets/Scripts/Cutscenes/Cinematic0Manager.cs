using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic0Manager : MonoBehaviour
{
    [SerializeField] PlayerUiManager playerUi;
    [SerializeField] List<SubtitlesSO> subtitles = new List<SubtitlesSO>();
    int index = 0;

    public void ActivateSubtitlePanel()
    {
        playerUi.ShowSubtitle(subtitles[index]);
        index++;
    }
}