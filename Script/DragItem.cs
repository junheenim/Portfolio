using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragItem : MonoBehaviour
{
    static public DragItem instance;
    public Slot dragSlot;

    [SerializeField]
    private Image itemImage;

    private void Start()
    {
        instance = this;
    }
    public void DragSetIamge(Image _itemIamge)
    {
        itemImage.sprite = _itemIamge.sprite;
        SetImageColor(0.8f);
    }

    public void SetImageColor(float a)
    {
        Color color = itemImage.color;
        color.a = a;
        itemImage.color = color;
    }
}
