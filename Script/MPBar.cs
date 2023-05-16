using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MPBar : MonoBehaviour
{
    private Slider mpBar;
    private float maxMP;
    private float curMP;

    [SerializeField]
    Text curMPText;
    [SerializeField]
    Text maxMPText;
    private void Awake()
    {
        mpBar = GetComponent<Slider>();
    }

    void Update()
    {
        maxMPText.text = PlayerManager.instance.nowPlayer.maxMP.ToString();
        curMPText.text = PlayerManager.instance.nowPlayer.curMP.ToString();
        maxMP = PlayerManager.instance.nowPlayer.maxMP;
        curMP = PlayerManager.instance.nowPlayer.curMP;
        mpBar.value = curMP / maxMP;

    }
}
