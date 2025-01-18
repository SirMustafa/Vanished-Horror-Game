using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleTxt;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;
    int index;

    private void Awake()
    {
        //_inputs.OnMoveEvent += HandleMovement;
    }
    private void Start()
    {
        subtitleTxt.text = "";
    }

    private void SetDialouge()
    {
        if (subtitleTxt.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            subtitleTxt.text = lines[index];
        }
    }

    public void StartDialouge()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            subtitleTxt.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            subtitleTxt.text += c;
        }
        yield return new WaitForSeconds(textSpeed);
    }
}