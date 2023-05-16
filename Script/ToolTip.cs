using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject toolTip;

    public Text itemName;
    public Text itemtip;

    public GameObject itemUseTip;
    [SerializeField]
    private Text itemUse;

    public GameObject priceToolTop;
    [SerializeField]
    private Text price;
    void Start()
    {
        toolTip.SetActive(false);
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void ShowToolTip(Item item, bool checkInven)
    {
        toolTip.SetActive(true);
        itemName.text = item.itemName;
        itemtip.text = item.itemInfo;
        price.text = item.price.ToString();
        if (checkInven)
        {
            if (item.type == Item.Type.Weapon || item.type == Item.Type.shield)
            {
                itemUseTip.SetActive(true);
                priceToolTop.SetActive(true);
                itemUse.text = "우클릭 : 장착";
            }
            else if(item.type == Item.Type.skill)
            {
                itemUseTip.SetActive(false);
                priceToolTop.SetActive(false);
                itemUse.text = "";
            }
            else if (item.type == Item.Type.potion)
            {
                itemUseTip.SetActive(true);
                priceToolTop.SetActive(true);
                itemUse.text = "우클릭 : 사용";
            }
            else
            {
                itemUse.text = "";
            }
        }
        else
        {
            itemUseTip.SetActive(true);
            priceToolTop.SetActive(true);
            itemUse.text = "우클릭 : 해제";
        }
    }
    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }
}
