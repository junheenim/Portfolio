using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttack : MonoBehaviour
{
    public int atk = 20;
    public Collider strongAtkCollider;
    public bool up = true;
    private void Start()
    {
        strongAtkCollider.enabled = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine("Brake");
            if (up)
            {
                transform.position += new Vector3(0, 0.3f, 0);
                if (transform.position.y >= -22)
                {
                    if(up)
                    {
                        strongAtkCollider.enabled = false;
                    }
                    
                    up = false;
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerCounter" || other.tag == "ShiledPlayer")
        {
            PlayerManager.instance.nowPlayer.curHP -= atk;
            strongAtkCollider.enabled = false;
        }
    }
    IEnumerator Brake()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
