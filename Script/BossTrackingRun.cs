using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrackingRun : MonoBehaviour
{
    Boss boss;
    public int atk = 20;
    Collider strongAtkCollider;
    public ParticleSystem heading;
    public GameObject rock;

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip rockHit;
    private void Awake()
    {
        boss = transform.root.GetComponent<Boss>();
        strongAtkCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        strongAtkCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCounter" || other.tag == "ShiledPlayer")
        {
            audioSource.clip = hit;
            audioSource.Play();
            PlayerManager.instance.nowPlayer.curHP -= atk;
            if (PlayerManager.instance.nowPlayer.curHP <= 0)
            {
                PlayerManager.instance.nowPlayer.curHP = 0;
            }
            strongAtkCollider.enabled = false;
            boss.anim.SetBool("AttackRun", false);
            boss.specialAtkOn = false;
            boss.tracking = false;
            boss.attacking = false;
            rock.SetActive(false);
        }
        else if(other.tag == "Rock")
        {
            audioSource.clip = rockHit;
            audioSource.Play();
            strongAtkCollider.enabled = false;
            Instantiate(heading, transform);
            boss.curHP -= 50;
            boss.anim.SetTrigger("OnHit");
            boss.anim.SetBool("Stun", true);
            boss.anim.SetBool("AttackRun", false);
            boss.specialAtkOn = false;
            boss.tracking = false;
            boss.OnStun();
            rock.SetActive(false);
        }
    }
}
