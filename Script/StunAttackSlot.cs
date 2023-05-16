using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StunAttackSlot : MonoBehaviour
{
    public LockOn target;
    public Image coolTimeImage;
    public Image backCoolTimeImage;

    void Update()
    {
        if (target.target.teTarget != null)
        {
            if(target.target.teTarget.groggyHit)
            {
                coolTimeImage.color = new Color(1, 1, 1, 1);
                backCoolTimeImage.color = new Color(1, 1, 1, 0.6f);
            }
            else
            {
                coolTimeImage.color = new Color(1, 0, 0, 1f);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
            if (target.target.teTarget.isDie)
            {
                coolTimeImage.color = new Color(1, 0, 0, 1);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
        }
        else if(target.target.target != null)
        {
            if(target.target.target.groggyHit)
            {
                coolTimeImage.color = new Color(1, 1, 1, 1);
                backCoolTimeImage.color = new Color(1, 1, 1, 0.6f);
            }
            else
            {
                coolTimeImage.color = new Color(1, 0, 0, 1);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
            if(target.target.target.isDie)
            {
                coolTimeImage.color = new Color(1, 0, 0, 1);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
        }
        else if (target.target.boss != null)
        {
            if (target.target.boss.groggyHit)
            {
                coolTimeImage.color = new Color(1, 1, 1, 1);
                backCoolTimeImage.color = new Color(1, 1, 1, 0.6f);
            }
            else
            {
                coolTimeImage.color = new Color(1, 0, 0, 1);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
            if (target.target.boss.isDie)
            {
                coolTimeImage.color = new Color(1, 0, 0, 1);
                backCoolTimeImage.color = new Color(1, 0, 0, 0.6f);
            }
        }
    }
    public void StunAttack()
    {
        StartCoroutine("CoolTime");
    }
    IEnumerator CoolTime()
    {
        float ct = 5f;
        float curTime = 0f;
        while (curTime <= ct)
        {
            curTime += Time.deltaTime;
            coolTimeImage.fillAmount = (curTime / ct);
            yield return new WaitForFixedUpdate();
        }
    }
}
