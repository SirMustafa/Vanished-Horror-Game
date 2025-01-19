using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _subtitleTxt;
    [SerializeField] float textSpeed;

    SubtitlesSO _currentLine;
    int _index;

    private void OnEnable()
    {
        //StartDialouge();
    }
    public void SetDialouge()
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

    public void StartDialouge(SubtitlesSO myLines)
    {
        _index = 0;
        _subtitleTxt.text = "";
        _currentLine = myLines;
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
        }
        yield return new WaitForSeconds(textSpeed);
    }
}