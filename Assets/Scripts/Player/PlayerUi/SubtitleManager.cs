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

    private SubtitlesSO _currentDialogue;
    private int _index;
    private bool _isTextFinished;

    public void SetAndStart(SubtitlesSO dialogue)
    {
        _subtitleTxt.text = "";
        _currentDialogue = dialogue;
        _isTextFinished = false;
        _index = 0;

        UpdateSpeakerData();
        StartDialogue();
    }

    public void SetMissionText(string shortDescription)
    {
        _missionTxt.text = shortDescription;
    }

    private void StartDialogue()
    {
        AudioManager.AudioInstance.PlaySubtitle(_currentDialogue.dialogueLines[_index].speakerAudio);
        StartCoroutine(TypeLine());  
    }

    private void CallNextLine()
    {
        //if (_subtitleTxt.text == _currentDialogue.dialogueLines[_index].line)
        //{
        //    NextLine();
        //}
        //else
        //{
        //    StopAllCoroutines();
        //    _subtitleTxt.text = _currentDialogue.dialogueLines[_index].line;
        //}
    }

    private void UpdateSpeakerData()
    {
        _imageComponent.sprite = _currentDialogue.dialogueLines[_index].speakerSprite;
    }

    private void NextLine()
    {
        if (_index < _currentDialogue.dialogueLines.Count - 1)
        {
            _index++;
            _subtitleTxt.text = "";
            UpdateSpeakerData();
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
       // string line = _currentDialogue.dialogueLines[_index].line;
       // _subtitleTxt.text = "";
       // foreach (char c in line)
       // {
       //     _subtitleTxt.text += c;
            yield return new WaitForSeconds(textSpeed);
       // }
       // NextLine();
    }

    private IEnumerator CheckCompletion()
    {
        if (_isTextFinished)
        {
            yield return null;
            OnSubtitleEndEvent?.Invoke();
        }
    }
}