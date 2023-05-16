using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Boss boss;
    Collider normalAtkCollider;
    public int atk;

    public AudioSource audioSource;
    public AudioClip hit;
    public AudioClip ShiledHit;
    public AudioClip counter;
    private void Awake()
    {
        normalAtkCollider = GetComponent<Collider>();
    }
    private void Start()
    {
        normalAtkCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.clip = hit;
            audioSource.Play();
            PlayerManager.instance.nowPlayer.curHP -= atk;

            normalAtkCollider.enabled = false;
        }
        else if(other.tag == "PlayerCounter")
        {
            audioSource.clip = counter;
            audioSource.Play();
            boss.stunCount++;
            boss.stun_Count.Count(boss.stunCount);
            normalAtkCollider.enabled = false;
        }
        else if(other.tag == "ShiledPlayer")
        {
            audioSource.clip = ShiledHit;
            audioSource.Play();
            PlayerManager.instance.nowPlayer.curHP -= (atk - (int)(atk * PlayerManager.instance.nowPlayer.def));
            normalAtkCollider.enabled = false;
        }
        if(PlayerManager.instance.nowPlayer.curHP<=0)
        {
            PlayerManager.instance.nowPlayer.curHP = 0;
        }
    }
}
