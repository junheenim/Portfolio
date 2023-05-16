using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowMP : MonoBehaviour
{
    Text mpText;
    private void Awake()
    {
        mpText = GetComponent<Text>();
    }
    private void Update()
    {
        mpText.text = "M   P : " + PlayerManager.instance.nowPlayer.curMP.ToString() + " / " + PlayerManager.instance.nowPlayer.maxMP.ToString();
    }
    public void OnClickUP()
    {
        if (PlayerManager.instance.nowPlayer.statusPoint > 0)
        {
            PlayerManager.instance.nowPlayer.maxMP++;
            PlayerManager.instance.nowPlayer.statusPoint--;
        }
    }
}
