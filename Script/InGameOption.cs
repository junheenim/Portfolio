using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameOption : MonoBehaviour
{
    public GameObject checkQuit;
    public GameObject CheckMainMenu;
    public GameObject soundOption;

    void Start()
    {

        gameObject.SetActive(false);
    }
    public void OnClickContinue()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }
    public void OnClickSaveGame()
    {
        PlayerManager.instance.SaveData();
    }
    public void OnClickOption()
    {
        soundOption.SetActive(true);
        gameObject.SetActive(false);
    }
    public void OnClickMainMenu()
    {
        CheckMainMenu.SetActive(true);
    }
    public void OnClickQuit()
    {
        checkQuit.SetActive(true);
    }
}
