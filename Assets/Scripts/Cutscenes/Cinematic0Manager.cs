using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic0Manager : MonoBehaviour
{
    [SerializeField] PlayerUiManager _playerUi;
    [SerializeField] List<SubtitlesSO> _subtitles = new List<SubtitlesSO>();
    int _index = 0;

    public void ActivateSubtitlePanel()
    {
        _playerUi.ShowSubtitle(_subtitles[_index]);
        _index++;
    }
}