using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHowDefense : MonoBehaviour
{
    Text def;
    private void Awake()
    {
        def = GetComponent<Text>();
    }
    private void Update()
    {
        def.text = "¹æ¾î·Â : " + Mathf.Round(PlayerManager.instance.nowPlayer.def * 100).ToString() + " %";
    }

    public void OnClickUP()
    {
        if (PlayerManager.instance.nowPlayer.statusPoint > 0)
        {
            PlayerManager.instance.nowPlayer.def += 0.01f;
            PlayerManager.instance.nowPlayer.statusPoint--;
        }
    }
}
