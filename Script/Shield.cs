using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject[] shield;
    public GameObject curShield;

    public void ChaingeShield(int itemcode)
    {
        if (curShield != null)
        {
            curShield.SetActive(false);
        }
        if (itemcode >= 0)
        {
            curShield = shield[itemcode - 5];
            curShield.SetActive(true);
        }
        else
        {
            curShield.SetActive(false);
        }
    }
}
