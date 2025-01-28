using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subtitleTxt;
    [SerializeField] TextMeshProUGUI _missionTxt;
    [SerializeField] float textSpeed;
    [SerializeField] Image _imageComponent;
    [SerializeField] AudioSource _mySource;
    [SerializeField] PlayerUiManager _uiManager;

   //public event Action OnSubtitleFinished;
    AudioClip narratorsAudio;
    SubtitlesSO _currentLine;
    int _index;

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
        _mySource.PlayOneShot(myLines.narratorsClip);
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
        yield return new WaitUntil(() => !_mySource.isPlaying);
       // OnSubtitleFinished?.Invoke();
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