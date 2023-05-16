using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    Boss boss;
    private Slider hpBar;
    public float maxHP;
    public float curHP;
    private void Awake()
    {
        hpBar = GetComponent<Slider>();
        boss = transform.root.GetComponent<Boss>();
    }
    private void Update()
    {
        maxHP = boss.maxHP;
        curHP = boss.curHP;
        hpBar.value = curHP / maxHP;
    }
}
