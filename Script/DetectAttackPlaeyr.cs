using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAttackPlaeyr : MonoBehaviour
{
    public WildEnemy enemy;
    public Collider detect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.target = other.gameObject;
        }
    }
}
