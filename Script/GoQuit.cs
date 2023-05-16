using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoQuit : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickYes()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void OnClickNo()
    {
        gameObject.SetActive(false);
    }
}
