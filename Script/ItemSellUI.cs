using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSellUI : MonoBehaviour
{
    public Slot slot;
    public Image itemImage;
    public Text coin;
    public Text countText;
    public int count = 0;
    private void Start()
    {
        coin.text = "0";
        countText.text = count.ToString();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            count += 10;
            if (slot.itemCount <= count)
            {
                count = slot.itemCount;
            }
            TextCont();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            count -= 10;
            if (0 >= count)
            {
                count = 0;
            }
            TextCont();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count++;
            if (slot.itemCount <= count)
            {
                count = slot.itemCount;
            }
            TextCont();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count--;
            if (0 >= count)
            {
                count = 0;
            }
            TextCont();
        }
    }
    void TextCont()
    {
        countText.text = count.ToString();
        coin.text = (count * slot.item.price).ToString();
    }
    public void OnClickYes()
    {
        PlayerManager.instance.nowPlayer.coin += slot.item.price * count;
        slot.SetSlotCount(-count);
        gameObject.SetActive(false);
    }

    public void OnClickNo()
    {
        gameObject.SetActive(false);
    }
    public void OnclickUp()
    {
        count += 10;
        if (slot.itemCount <= count)
        {
            count = slot.itemCount;
        }
        TextCont();
    }
    public void OnclickDown()
    {
        count -= 10;
        if (0 >= count)
        {
            count = 0;
        }
        TextCont();
    }
    public void OnclickRight()
    {
        count++;
        if (slot.itemCount <= count)
        {
            count = slot.itemCount;
        }
        TextCont();
    }
    public void OnClickLeft()
    {
        count--;
        if (0 >= count)
        {
            count = 0;
        }
        TextCont();
    }
}
