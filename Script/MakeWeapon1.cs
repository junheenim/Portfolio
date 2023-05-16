using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class MakeWeapon1 : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    [SerializeField]
    private GameObject slotParent;
    private Slot[] slots;

    [SerializeField]
    Item item;

    public Text text;
    string coin;

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Update()
    {
        SetCount();
    }
    public void OnclickMade()
    {
        if (CheckSlot(slots))
        {
            if (PlayerManager.instance.nowPlayer.coin >= 300)
            {
                inventory.AcquirelItem(item);
                PlayerManager.instance.nowPlayer.coin -= 300;
            }
        }
    }

    void SetCount()
    {
        coin = PlayerManager.instance.nowPlayer.coin.ToString();
        if (PlayerManager.instance.nowPlayer.coin >= 300)
        {
            text.color = Color.black;
        }
        else
        {
            text.color = Color.red;
        }
        text.text = coin + " / 300";
    }
    bool CheckSlot(Slot[] slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }
}
