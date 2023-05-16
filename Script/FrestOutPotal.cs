using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FrestOutPotal : MonoBehaviour
{
    public Vector3 pos = new Vector3();
    GameObject palyer;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(palyer.tag == "Player")
            {
                palyer.transform.position = pos;
                SceneManager.LoadScene("MainWorld");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            palyer = other.gameObject;
        }
    }
}
