using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject player;
    public int damage;
    public GameObject explosion;
    public Weapon weapon;
    Enemy enemy;
    WildEnemy wild;
    Boss boss;

    private void Start()
    {
        StartCoroutine("LifeSpan");
    }
    private void Update()
    {
        transform.Translate(player.transform.forward * 0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponentInChildren<Enemy>();
            weapon.target = enemy;
            weapon.enTarget = other.gameObject;
            enemy.target = player;
            weapon.LockOn();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.tag == "WildEnemy")
        {
            wild = other.GetComponentInChildren<WildEnemy>();
            weapon.wildTarget = wild;
            weapon.enTarget = other.gameObject;
            wild.target = player;
            weapon.LockOn();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.tag == "Boss")
        {
            boss = other.GetComponent<Boss>();
            weapon.enTarget = other.gameObject;
            weapon.boss = boss;
            weapon.BossLockOn();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
