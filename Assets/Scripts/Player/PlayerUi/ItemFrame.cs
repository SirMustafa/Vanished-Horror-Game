using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] Color defaultColor;
    Image myImageComponent;

    private void Awake()
    {
        myImageComponent = GetComponent<Image>();
    }

    public void Select()
    {
        SetColor(selectedColor);
    }

    public void Deselect()
    {
        SetColor(defaultColor);
    }

    private void SetColor(Color newColor)
    {
        newColor.a = 1;
        myImageComponent.color = newColor;
    }
}
