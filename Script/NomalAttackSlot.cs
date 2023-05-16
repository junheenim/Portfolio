using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NomalAttackSlot : MonoBehaviour
{
    [SerializeField]
    Image coolTimeImage;
    public void NormalAttack()
    {
        StartCoroutine("CoolTime");
    }

    IEnumerator CoolTime()
    {
        float ct = 0.3f;
        float curTime = 0f;
        while (curTime <= ct)
        {
            curTime += Time.deltaTime;
            coolTimeImage.fillAmount = (curTime / ct);
            yield return new WaitForFixedUpdate();
        }
    }
}
