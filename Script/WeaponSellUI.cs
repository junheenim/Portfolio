using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSellUI : MonoBehaviour
{
    public Slot slot;
    public Image itemImage;
    public Text coin;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnClickYes()
    {
        PlayerManager.instance.nowPlayer.coin += slot.item.price;
        slot.ClearSlot();
        gameObject.SetActive(false);
    }
    public void OnClickNo()
    {
        gameObject.SetActive(false);
    }
}
