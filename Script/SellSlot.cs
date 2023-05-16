using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellSlot : MonoBehaviour
{
    public GameObject inventorySlot;
    Slot[] invenSlots;

    public GameObject sellSlot;
    StoreSlot[] sellSlots;
    private void Awake()
    {
        invenSlots = inventorySlot.GetComponentsInChildren<Slot>();
        sellSlots = sellSlot.GetComponentsInChildren<StoreSlot>();
    }

    private void Update()
    {
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (invenSlots[i].item != null)
            {
                sellSlots[i].item = invenSlots[i].item;
                sellSlots[i].itemCount = invenSlots[i].itemCount;
                sellSlots[i].itemImage.sprite = sellSlots[i].item.itemImage;
            }
        }
    }
}
