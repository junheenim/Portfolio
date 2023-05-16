using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPBar : MonoBehaviour
{
    private Slider expBar;
    private float maxEXP;
    private float curEXP;

    [SerializeField]
    Text expPersent;
    private void Awake()
    {
        expBar = GetComponent<Slider>();
    }
    void Update()
    {
        maxEXP = PlayerManager.instance.nowPlayer.maxEXP;
        curEXP = PlayerManager.instance.nowPlayer.curEXP;
        expBar.value = curEXP / maxEXP;
        expPersent.text = ((curEXP / maxEXP) * 100).ToString("F1") + "%";
        if (curEXP >= maxEXP)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        if (PlayerManager.instance.nowPlayer.level >= 10)
        {
            PlayerManager.instance.nowPlayer.curEXP = (int)(curEXP - maxEXP);
            PlayerManager.instance.nowPlayer.maxEXP += 10;
            PlayerManager.instance.nowPlayer.level++;
            PlayerManager.instance.nowPlayer.statusPoint += 5;
        }
    }


}
