using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeWeapon2 : MonoBehaviour
{
    [SerializeField]
    private GameObject slotParent;
    private Slot[] slots;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    Item item;

    public Text itme1t;
    public Item item1;
    int item1Count;

    public Text itme2t;
    public Item item2;
    int item2Count;
    public int needitem2;

    public Text itme3t;
    public Item item3;
    int item3Count;
    public int needitem3;

    public Text moneyt;
    int money;
    public int needMoney;

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
            if (item1Count >= 1 && item2Count >= needitem2 && item2Count >= needitem3 && money >= needMoney)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item == item1)
                    {
                        slots[i].ClearSlot();
                    }
                    else if (slots[i].item == item2)
                    {
                        slots[i].SetSlotCount(-needitem2);
                    }
                    else if (slots[i].item == item3)
                    {
                        slots[i].SetSlotCount(-needitem3);
                    }
                }

                inventory.AcquirelItem(item);

                PlayerManager.instance.nowPlayer.coin -= needMoney;
            }
        }
    }

    void SetCount()
    {
        item1Count = 0;
        item2Count = 0;
        item3Count = 0;
        itme1t.color = Color.red;
        itme2t.color = Color.red;
        itme3t.color = Color.red;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item1)
            {
                item1Count = slots[i].itemCount;
                if (item1Count >= 1)
                {
                    itme1t.color = Color.black;
                }
            }
            else if (slots[i].item == item2)
            {
                item2Count = slots[i].itemCount;
                if (item2Count >= needitem2)
                {
                    itme2t.color = Color.black;
                }
            }
            else if (slots[i].item == item3)
            {
                item3Count = slots[i].itemCount;
                if (item3Count >= needitem3)
                {
                    itme3t.color = Color.black;
                }
            }
        }

        itme1t.text = item1Count.ToString() + " / 1";
        itme2t.text = item2Count.ToString() + " / " + needitem2.ToString();
        itme3t.text = item3Count.ToString() + " / " + needitem3.ToString();

        money = PlayerManager.instance.nowPlayer.coin;
        moneyt.text = money.ToString() + " / " + needMoney.ToString();
        if (money >= needMoney)
        {
            moneyt.color = Color.black;
        }
        else
        {
            moneyt.color = Color.red;
        }
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
