using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subtitleTxt;
    [SerializeField] float textSpeed;
    [SerializeField] Image _imageComponent;

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

    void StartDialouge(SubtitlesSO myLines)
    {
        _index = 0;
        _subtitleTxt.text = "";
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
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in _currentLine.lines[_index].ToCharArray())
        {
            _subtitleTxt.text += c;
            yield return new WaitForSeconds(textSpeed);
        } 
    }
}