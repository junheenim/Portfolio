using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    private Slider hpBar;
    public float maxHP;
    public float curHP;
    private void Awake()
    {
        hpBar = GetComponent<Slider>();
    }
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            hpBar.value = curHP / maxHP;
        }
    }
}
