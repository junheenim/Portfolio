using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    public CharacterUI UI;
    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    GameObject qSlotParent;

    [SerializeField]
    private Text money;

    private Slot[] slots;
    private Slot[] qslots;
    public GameObject eqiupmentActive;

    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        qslots = qSlotParent.GetComponentsInChildren<Slot>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        money.text = PlayerManager.instance.nowPlayer.coin.ToString();
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
        UI.MouseControl();
    }
    public void AcquirelItem(Item _itme, int _count = 1)
    {
        if (_itme.type != Item.Type.Weapon && _itme.type != Item.Type.shield)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.name == _itme.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
            for (int i = 0; i < qslots.Length; i++)
            {
                if (qslots[i].item != null)
                {
                    if (qslots[i].item.name == _itme.itemName)
                    {
                        qslots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_itme, _count);
                return;
            }
        }
    }

    // 빈 슬롯 없이 정렬 (아이템 앞으로 다 끌어오기)
    public void Trim()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                for (int j = i + 1; j < slots.Length; j++)
                {
                    if (slots[j].item != null)
                    {
                        slots[i].AddItem(slots[j].item, slots[j].itemCount);
                        slots[j].ClearSlot();
                        break;
                    }
                    if (j == slots.Length - 1)
                    {
                        return;
                    }
                }
            }
        }

    }
    // 정렬
    public void Sort()
    {
        LookAllSlots();
        Trim();

        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].item == null)
            {
                return;
            }
            for (int j = i + 1; j < slots.Length; j++)
            {
                if (slots[j].item == null)
                {
                    break;
                }
                if (slots[i].item.itemCode > slots[j].item.itemCode)
                {
                    Item t_item = slots[i].item;
                    int t_count = slots[i].itemCount;
                    slots[i].AddItem(slots[j].item, slots[j].itemCount);
                    slots[j].AddItem(t_item, t_count);
                }
            }
        }
    }

    // 모든 아이템 보기
    public void LookAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].SetColor(1);
                slots[i].lookSlot = true;
            }
        }
    }

    //장비만 보기
    public void LookEquiment()
    {
        LookAllSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.type != Item.Type.Weapon && slots[i].item.type != Item.Type.shield)
                {
                    slots[i].SetColor(0.5f);
                    slots[i].lookSlot = false;
                }
                else
                {
                    slots[i].lookSlot = true;
                }
            }
        }
    }
    // 포션만 보기
    public void LookPotion()
    {
        LookAllSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.type != Item.Type.potion)
                {
                    slots[i].SetColor(0.5f);
                    slots[i].lookSlot = false;
                }
                else
                {
                    slots[i].lookSlot = true;
                }
            }
        }
    }

    // 재료만 보기
    public void LookIngredient()
    {
        LookAllSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.type != Item.Type.ingredient)
                {
                    slots[i].SetColor(0.5f);
                    slots[i].lookSlot = false;
                }
                else
                {
                    slots[i].lookSlot = true;
                }
            }
        }
    }
}
