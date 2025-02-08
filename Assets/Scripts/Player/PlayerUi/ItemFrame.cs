using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color defaultColor;
    private Image itemImage;
    private Image myImageComponent;

    private void Awake()
    {
        myImageComponent = GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        ClearItem();
    }
    public void SetItem(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

    public void ClearItem()
    {
        itemImage.sprite = null;
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
