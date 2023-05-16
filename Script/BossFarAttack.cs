using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFarAttack : MonoBehaviour
{
    bool up = true;
    public Collider collier;
    public int atk;
    void Update()
    {
        if(gameObject.activeSelf)
        {
            if (up)
            {
                transform.localPosition += new Vector3(0, 0.2f, 0);
                if (transform.localPosition.y >= -23f)
                {
                    collier.enabled = false;
                    up = false;
                }
            }

            else 
            {
                transform.localPosition -= new Vector3(0, 0.05f, 0);
                if (transform.localPosition.y <= -27)
                {
                    collier.enabled = true;
                    up = true;
                    gameObject.SetActive(false);
                }
            }
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCounter" || other.tag == "ShiledPlayer")
        {
            PlayerManager.instance.nowPlayer.curHP -= atk;
            collier.enabled = false;
        }
    }

}
