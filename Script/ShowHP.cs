using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowHP : MonoBehaviour
{
    Text hpText;

    private void Awake()
    {
        hpText = GetComponent<Text>();
    }
    private void Update()
    {
        hpText.text = "H   P : "+PlayerManager.instance.nowPlayer.curHP.ToString() + " / " + PlayerManager.instance.nowPlayer.maxHP.ToString();
    }
    public void OnClickUP()
    {
        if (PlayerManager.instance.nowPlayer.statusPoint > 0)
        {
            PlayerManager.instance.nowPlayer.maxHP += 10;
            PlayerManager.instance.nowPlayer.statusPoint--;
        }
    }
}
