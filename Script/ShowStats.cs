using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowStats : MonoBehaviour
{
    Text stat;
    public GameObject but1;
    public GameObject but2;
    public GameObject but3;
    public GameObject but4;
    private void Awake()
    {
        stat = GetComponent<Text>();
    }
    private void Update()
    {
        stat.text = "½º  ÅÝ : " + PlayerManager.instance.nowPlayer.statusPoint.ToString();
        if(PlayerManager.instance.nowPlayer.statusPoint>0)
        {
            but1.SetActive(true);
            but2.SetActive(true);
            but3.SetActive(true);
            but4.SetActive(true);
        }
        else
        {
            but1.SetActive(false);
            but2.SetActive(false);
            but3.SetActive(false);
            but4.SetActive(false);
        }
    }
}
