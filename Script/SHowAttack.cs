using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SHowAttack : MonoBehaviour
{
    Text atk;
    private void Awake()
    {
        atk = GetComponent<Text>();
    }
    void Update()
    {
        atk.text = "°ø°Ý·Â : " + PlayerManager.instance.nowPlayer.minattack.ToString() + " ~ " + PlayerManager.instance.nowPlayer.maxattack.ToString();
    }
    public void OnClickUP()
    {
        if (PlayerManager.instance.nowPlayer.statusPoint > 0)
        {
            PlayerManager.instance.nowPlayer.minattack++;
            PlayerManager.instance.nowPlayer.maxattack++;
            PlayerManager.instance.nowPlayer.statusPoint--;
        }
    }
}
