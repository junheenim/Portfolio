using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public GameObject character;
    GameObject player;
    public TutorialTalk tt;
    public GameObject manager;
    void Start()
    {
        player = Instantiate(character);
        player.transform.position = transform.position;
        tt.Player = player;
    }
}
