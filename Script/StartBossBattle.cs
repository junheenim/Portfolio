using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public GameObject wall;
    Boss boss;
    public GameObject bossUI;
    GameObject player;

    public AudioClip battleStartSound;
    private void Start()
    {
        bossUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine("BattleStart");
        }
    }
    IEnumerator BattleStart()
    {
        bossUI.SetActive(true);
        boss = GameObject.Find("Ω£¿« ¡÷¿Œ").GetComponent<Boss>();
        boss.anim.SetBool("StartBattle", true);
        boss.bossSound.clip = battleStartSound;
        boss.bossSound.Play();
        boss.inbattle = true;
        yield return new WaitForSeconds(1.5f);
        boss.player = player;
        wall.SetActive(true);
        gameObject.SetActive(false);
    }
}
