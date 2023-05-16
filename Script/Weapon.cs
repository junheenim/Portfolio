using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject lockOn;
    public GameObject enTarget;
    public GameObject enemyInfo;
    public GameObject hpBar;
    public Enemy target;
    public WildEnemy wildTarget;
    public TutorialEnemy teTarget;
    public Boss boss;
    public BoxCollider area;
    public TrailRenderer trailEffect;
    public GameObject[] weapon;
    public GameObject curWeapon;
    private void Awake()
    {
        area.enabled = false;
    }
    public void ChaingeWeapon(int itemcode)
    {
        if (curWeapon != null)
        {
            curWeapon.SetActive(false);
        }
        if (itemcode >= 0)
        {
            curWeapon = weapon[itemcode];
            curWeapon.SetActive(true);
        }
        else
        {
            curWeapon.SetActive(false);
        }
    }
    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        area.enabled = true;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);
        area.enabled = false;
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    public void LockOn()
    {
        lockOn.SetActive(true);
        enemyInfo.SetActive(true);
        hpBar.SetActive(true);
    }
    public void BossLockOn()
    {
        lockOn.SetActive(true);
        enemyInfo.SetActive(true);
        hpBar.SetActive(false);
    }
    public void LockOff()
    {
        lockOn.SetActive(false);
        enemyInfo.SetActive(false);
        hpBar.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TutorialEnemy")
        {
            enTarget = other.gameObject;
            teTarget = other.GetComponent<TutorialEnemy>();
            if (teTarget.curHP <= 0)
            {
                enTarget = null;
                return;
            }
            LockOn();
        }
        else if (other.tag == "Enemy")
        {
            enTarget = other.gameObject;
            target = other.GetComponent<Enemy>();
            LockOn();
        }
        else if(other.tag == "WildEnemy")
        {
            enTarget = other.gameObject;
            wildTarget = other.GetComponent<WildEnemy>();
            if (wildTarget.curHP <= 0)
            {
                enTarget = null;
                return;
            }
            LockOn();
        }
        else if (other.tag == "Boss")
        {
            enTarget = other.gameObject;
            boss = other.GetComponent<Boss>();
            if (boss.curHP <= 0)
            {
                enTarget = null;
                return;
            }
            BossLockOn();
        }
    }
}
