using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoMainMenu : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickYes()
    {
        Destroy(Player);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickNo()
    {
        gameObject.SetActive(false);
    }

}
