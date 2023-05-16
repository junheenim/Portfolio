using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InFrestPotal : MonoBehaviour
{
    public Vector3 pos;
    GameObject palyer;
    public GameObject ui;
    public Text Inter;
    public Text Destination;
    public string destinationName;
    public AudioClip forest;
    private void Start()
    {
        Inter.text = "<color=yellow>" + "(E)" + "</color>" + "키를 눌러 이동";
        Destination.text = "목적지 : " + gameObject.name;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (palyer != null && palyer.tag == "Player")
            {
                SoundManager.instance.bgmAudio.clip = forest;
                SoundManager.instance.bgmAudio.Play();
                palyer.transform.position = pos;
                Loading.LoadScene(destinationName, palyer, pos);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            ui.SetActive(true);
            palyer = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        palyer = null;
        ui.SetActive(false);
    }
}
