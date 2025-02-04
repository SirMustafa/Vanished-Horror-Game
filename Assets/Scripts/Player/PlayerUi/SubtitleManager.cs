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

    private AudioClip narratorsAudio;
    private SubtitlesSO _currentLine;
    private int _index;
    private float _duration;
    private bool _isTextFinished;
    private bool _isAudioFinished;

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
        narratorsAudio = myLines.narratorsClip;

        StartDialogue();
    }

    public void SetMissionText(string shortDescription)
    {
        _missionTxt.text = shortDescription;
    }

    private void StartDialogue()
    {
        _index = 0;
        _subtitleTxt.text = "";
        _isTextFinished = false;
        _isAudioFinished = false;

        AudioManager.AudioInstance.PlaySubtitle(narratorsAudio);
        _duration = narratorsAudio.length;

        StartCoroutine(TypeLine());
        StartCoroutine(WaitForAudioToEnd());
    }

    private void NextLine()
    {
        if (_index < _currentLine.lines.Length - 1)
        {
            _index++;
            _subtitleTxt.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            _isTextFinished = true;
            StartCoroutine(CheckCompletion());
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in _currentLine.lines[_index])
        {
            _subtitleTxt.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        NextLine();
    }

    private IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitForSeconds(_duration);
        _isAudioFinished = true;
        StartCoroutine(CheckCompletion());
    }

    private IEnumerator CheckCompletion()
    {
        if (_isTextFinished && _isAudioFinished)
        {
            yield return null;
            OnSubtitleEndEvent?.Invoke();
        }
    }
}