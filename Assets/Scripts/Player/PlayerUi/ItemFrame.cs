using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrame : MonoBehaviour
{
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Image itemImage;

    private Image myImageComponent;

    private void Awake()
    {
        myImageComponent = GetComponent<Image>();
        ClearItem();
    }

    public void SetItem(Sprite itemSprite)
    {
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
            itemImage.enabled = true;
        }
    }

    public void ClearItem()
    {
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
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
