using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    private Slider hpBar;
    private float maxHP;
    private float curHP;

    [SerializeField]
    Text curHPText;
    [SerializeField]
    Text maxHPText;
    private void Awake()
    {
        hpBar = GetComponent<Slider>();
    }
    void Update()
    {
        maxHPText.text = PlayerManager.instance.nowPlayer.maxHP.ToString();
        curHPText.text = PlayerManager.instance.nowPlayer.curHP.ToString();

        maxHP = PlayerManager.instance.nowPlayer.maxHP;
        curHP = PlayerManager.instance.nowPlayer.curHP;

        hpBar.value = curHP / maxHP;
    }

}
