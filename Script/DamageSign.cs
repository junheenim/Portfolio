using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSign : MonoBehaviour
{
    GameObject damageText;
    public Canvas damageUI;
    public GameObject Damagetext;
    public GameObject enemy;
    public void DamageOn(int damage, Vector3 pos, bool cri)
    {
        Damagetext.GetComponent<DamageText>().enemy = pos;
        if(cri)
        {
            Damagetext.GetComponent<DamageText>().color = Color.red;
            Damagetext.GetComponent<DamageText>().textSize = 50;
        }
        else
        {
            Damagetext.GetComponent<DamageText>().color = Color.yellow;
            Damagetext.GetComponent<DamageText>().textSize = 40;
        }
        damageText = Instantiate(Damagetext);
        damageText.transform.SetParent(damageUI.gameObject.transform);
        damageText.GetComponent<Text>().text = damage.ToString();
    }
    
}
