using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefPush : MonoBehaviour
{
    [SerializeField]
    Image coolTimeImage;
    public void DefPushOn()
    {
        StartCoroutine("DefCoolTime");
    }

    IEnumerator DefCoolTime()
    {
        float ct = 1f;
        float curTime = 0f;
        while (curTime <= ct)
        {
            curTime += Time.deltaTime;
            coolTimeImage.fillAmount = (curTime / ct);
            yield return new WaitForFixedUpdate();
        }
    }
}
