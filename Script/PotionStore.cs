using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PotionStore : MonoBehaviour
{
    [SerializeField]
    Item hpPotion;
    [SerializeField]
    Item mpPotion;
    [SerializeField]
    SelectNumber selectNumber;
    public GameObject selectNum;

    public GameObject sellItemUI;
    public GameObject sellWeaponUI;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnClickClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sellItemUI.SetActive(false);
        sellWeaponUI.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnClickHPPotion()
    {
        sellItemUI.SetActive(false);
        sellWeaponUI.SetActive(false);
        selectNum.SetActive(true);
        selectNum.transform.position = Input.mousePosition;
        selectNumber.item = hpPotion;
    }
    public void OnClickMPPotion()
    {
        sellItemUI.SetActive(false);
        sellWeaponUI.SetActive(false);
        selectNum.SetActive(true);
        selectNum.transform.position = Input.mousePosition;
        selectNumber.item = mpPotion;
    }
}
