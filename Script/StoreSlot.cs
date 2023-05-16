using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    private Text textCount;

    public GameObject weponSell;
    public GameObject sell;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(item.type==Item.Type.Weapon)
        {

        }
        else
        {

        }
    }
}
