using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoolTime : MonoBehaviour
{
    float coolTime;
    float curTime;

    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void LookCoolTime(float cool)
    {
        coolTime = cool;
        curTime = cool;
        StartCoroutine("CoolTimeImage");
    }

    IEnumerator CoolTimeImage()
    {
        while (curTime >= 0)
        {
            curTime -= Time.deltaTime;
            image.fillAmount = (curTime / coolTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
