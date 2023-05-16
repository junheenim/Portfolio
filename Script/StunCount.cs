using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunCount : MonoBehaviour
{
    Image[] image;

    private void Awake()
    {
        image = GetComponentsInChildren<Image>();
    }

    public void CountReset()
    {
        for (int i = 0; i < 5; i++)
        {
            image[i].color = Color.white;
        }
    }
    public void Count(int count)
    {
        for (int i = 0; i < count; i++)
        {
            image[i].color = Color.magenta;
            if(count == 5)
            {
                image[i].color = Color.red;
            }
        }
    }
}
