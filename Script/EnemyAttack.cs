using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    public GameObject Stun;
    public Enemy enemy;
    Collider coll;
    bool isHitOn = true;
    //°ø°Ý·Â
    public int strongAtk = 8;
    public int atk = 5;
    AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip shiledSound;
    public AudioClip counterSound;
    private void Awake()
    {
        coll = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        coll.enabled = false;
    }
    private void Update()
    {
        if (PlayerManager.instance.nowPlayer.curHP <= 0)
        {
            enemy.RetrunToCurPos();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coll.enabled = false;
            if (isHitOn)
            {
                audioSource.clip = attackSound;
                audioSource.Play();
                if (enemy.strongAtk)
                {
                    PlayerManager.instance.nowPlayer.curHP -= strongAtk;
                }
                else
                {
                    PlayerManager.instance.nowPlayer.curHP -= atk;
                }
                
                if (PlayerManager.instance.nowPlayer.curHP <= 0)
                {
                    PlayerManager.instance.nowPlayer.curHP = 0;
                    if(enemy.target!=null)
                    {
                        enemy.target.GetComponent<PlayerInpt>().Die();
                    }
                    enemy.RetrunToCurPos();
                }
                isHitOn = false;
                StartCoroutine("PlayerHit");
            }
        }
        else if (other.tag == "ShiledPlayer")
        {
            coll.enabled = false;
            audioSource.clip = shiledSound;
            audioSource.Play();
            if (enemy.strongAtk)
            {
                PlayerManager.instance.nowPlayer.curHP -= (strongAtk - (int)(strongAtk * PlayerManager.instance.nowPlayer.def));
            }
            else
            {
                PlayerManager.instance.nowPlayer.curHP -= (atk - (int)(atk * PlayerManager.instance.nowPlayer.def));
            }
            if (PlayerManager.instance.nowPlayer.curHP <= 0)
            {
                PlayerManager.instance.nowPlayer.curHP = 0;
                enemy.target.GetComponent<PlayerInpt>().Die();
                enemy.RetrunToCurPos();
            }
        }
        else if (other.tag == "PlayerCounter")
        {
            audioSource.clip = counterSound;
            audioSource.Play();
            coll.enabled = false;
            enemy.groggy = true;
            enemy.groggyHit = true;
            StartCoroutine("OnGroggy");
        }
    }

    IEnumerator PlayerHit()
    {
        yield return new WaitForSeconds(1.0f);
        isHitOn = true;
    }
    IEnumerator OnGroggy()
    {
        enemy.nav.enabled = false;
        Stun.SetActive(true);
        enemy.detect.detect.enabled = false;
        GameObject target = enemy.target;
        enemy.detect.target = null;
        enemy.target = null;
        enemy.anim.SetBool("MoveRun", false);
        enemy.anim.SetTrigger("GroggySet");
        enemy.anim.SetBool("Groggy", true);
        yield return new WaitForSeconds(3.0f);
        Stun.SetActive(false);
        enemy.anim.SetBool("Groggy", false);
        enemy.detect.target = target;
        enemy.target = target;
        enemy.transform.LookAt(target.transform);
        enemy.nav.enabled = true;
        enemy.nav.isStopped = false;
        enemy.detect.detect.enabled = true;
        enemy.groggy = false;
        enemy.groggyHit = false;
        enemy.groggyOut = true;
    }
}
