using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject inventoryActive;
    public CharacterUI UI;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void CloseEquipment()
    {
        gameObject.SetActive(false);
        UI.MouseControl();
    }
}
