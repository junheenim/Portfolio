using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponStore : MonoBehaviour
{
    public GameObject weaponSlot;
    public GameObject shieldSlot;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        shieldSlot.SetActive(false);
    }

    public void OnClickWeaponSLot()
    {
        weaponSlot.SetActive(true);
        shieldSlot.SetActive(false);
    }
    public void OnClickShieldSlot()
    {
        weaponSlot.SetActive(false);
        shieldSlot.SetActive(true);
    }
    public void OnClickClose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
