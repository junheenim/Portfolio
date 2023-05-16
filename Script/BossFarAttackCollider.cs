using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFarAttackCollider : MonoBehaviour
{
    int atk = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCounter" || other.tag == "ShiledPlayer")
        {
            PlayerManager.instance.nowPlayer.curHP -= atk;
        }
    }
}
