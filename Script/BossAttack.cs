using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int atk;
    Collider strongAtkCollider;

    public AudioSource audioSource;
    public AudioClip hit;
    private void Awake()
    {
        strongAtkCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCounter" || other.tag == "ShiledPlayer")
        {
            audioSource.clip = hit;
            audioSource.Play();
            PlayerManager.instance.nowPlayer.curHP -= atk;
            if(PlayerManager.instance.nowPlayer.curHP<=0)
            {
                PlayerManager.instance.nowPlayer.curHP = 0;
            }
            strongAtkCollider.enabled = false;
        }
    }
}
