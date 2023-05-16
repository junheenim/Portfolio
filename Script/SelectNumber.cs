using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectNumber : MonoBehaviour
{
    public Inventory inventory;
    public Text number;
    public Text totalPrice;
    public Item item;
    public int num;

    public AudioSource audioSource;
    public AudioClip buy;
    private void Start()
    {
        num = 0;
        totalPrice.text = "0";
        number.text = num.ToString();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            num += 10;
            if (num >= 99)
            {
                num = 99;
            }
            LookText();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            num -= 10;
            if (num <= 0)
            {
                num = 0;
            }
            LookText();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            num++;
            if (num >= 99)
            {
                num = 99;
            }
            LookText();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            num--;
            if (num <= 0)
            {
                num = 0;
            }
            LookText();
        }
    }

    public void LookText()
    {
        number.text = num.ToString();
        totalPrice.text = (num * 100).ToString();
        if(PlayerManager.instance.nowPlayer.coin >= 100 * num)
        {
            totalPrice.color = Color.black;
        }
        else
        {
            totalPrice.color = Color.red;
        }
    }
    public void OnclickUp()
    {
        num += 10;
        if (num >= 99)
        {
            num = 99;
        }
        LookText();
    }
    public void OnclickDown()
    {
        num -= 10;
        if (num <= 0)
        {
            num = 0;
        }
        LookText();
    }
    public void OnclickRight()
    {
        num++;
        if (num >= 99)
        {
            num = 99;
        }
        LookText();
    }
    public void OnClickLeft()
    {
        num--;
        if (num <= 0)
        {
            num = 0;
        }
        LookText();
    }
    public void OnclickBuy()
    {
        if (PlayerManager.instance.nowPlayer.coin >= 100 * num && num >= 1)
        {
            audioSource.clip = buy;
            audioSource.Play();
            inventory.AcquirelItem(item, num);
            PlayerManager.instance.nowPlayer.coin -= num * 100;
            num = 0;
            totalPrice.text = (num * 100).ToString();
            number.text = num.ToString();
            totalPrice.color = Color.black;
            gameObject.SetActive(false);
        }
    }
    public void OnClickClose()
    {
        num = 0;
        totalPrice.text = (num * 100).ToString();
        number.text = num.ToString();
        totalPrice.color = Color.black;
        gameObject.SetActive(false);
    }
}
