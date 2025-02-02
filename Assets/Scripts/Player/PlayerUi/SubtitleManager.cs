using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subtitleTxt;
    [SerializeField] TextMeshProUGUI _missionTxt;
    [SerializeField] Image _imageComponent;
    [SerializeField] PlayerUiManager _uiManager;
    [SerializeField] UnityEvent OnSubtitleEndEvent;
    [SerializeField] float textSpeed;

    AudioClip narratorsAudio;
    SubtitlesSO _currentLine;
    int _index;
    float _duration;

    public void CallNextLine()
    {
        if (_subtitleTxt.text == _currentLine.lines[_index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            _subtitleTxt.text = _currentLine.lines[_index];
        }
    }

    public void SetAndStart(SubtitlesSO myLines)
    {
        _subtitleTxt.text = "";
        _currentLine = myLines;
        _imageComponent.sprite = myLines.narratorsSprite;
        StartDialouge(myLines);
    }
    public void SetMissionText(string shortDescription)
    {
        _missionTxt.text = shortDescription;
    }

    void StartDialouge(SubtitlesSO myLines)
    {
        _index = 0;
        _subtitleTxt.text = "";
        AudioManager.AudioInstance.PlaySubtitle(myLines.narratorsClip);
        _duration = myLines.narratorsClip.length;
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if (_index < _currentLine.lines.Length - 1)
        {
            _index++;
            _subtitleTxt.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(WaitSpeech());
        }
    }

    IEnumerator WaitSpeech()
    {
        yield return new WaitForSeconds(_duration);
        OnSubtitleEndEvent?.Invoke();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in _currentLine.lines[_index].ToCharArray())
        {
            _subtitleTxt.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        NextLine();
    }
}