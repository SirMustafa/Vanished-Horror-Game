using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic0Manager : MonoBehaviour
{
    [SerializeField] PlayerUiManager _playerUi;
    [SerializeField] List<SubtitlesSO> _subtitles = new List<SubtitlesSO>();
    [SerializeField] List<Doors> _theDoors = new List<Doors>();
    int _index = 0;
    int _doorIndex = 0;

    public void ActivateSubtitlePanel()
    {
        _playerUi.ShowSubtitle(_subtitles[_index]);
        _index++;
    }
    public void OpenDoor()
    {
        _theDoors[_doorIndex].MyInterract();
        _doorIndex++;
    }
}