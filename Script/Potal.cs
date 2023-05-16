using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Potal : MonoBehaviour
{
    public GameObject player;
    public TutorialTalk tt;
    public GameObject inter;
    public Text potal;
    public Text destination;

    public AudioClip hero;
    void Start()
    {
        gameObject.SetActive(false);
        inter.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            if (player != null)
            {
                tt.input.mainQuest.QuestUpdate();
                Loading.LoadScene("MainWorld", player, new Vector3(-1, 7.1f, 415.3f));
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            potal.text = "<color=yellow>" + "(E)" + "</color>" + "키를 눌러 이동";
            destination.text = "목적지 : 마을";
            inter.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inter.SetActive(false);
    }
}
