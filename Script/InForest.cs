using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InForest : MonoBehaviour
{
    public Text text;
    public Image image1;
    public Image image2;
    void Start()
    {
        text.text = "¿¤Æù½£ ±íÀº°÷";
        StartCoroutine("Fade");
    }
    IEnumerator Fade()
    {

        text.gameObject.SetActive(true);
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(true);
        float a = 0;
        for (int i = 0; i < 20; i++)
        {
            a += 0.05f;
            text.color = new Color(1, 1, 1, a);
            image1.color = new Color(1, 1, 1, a);
            image2.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 20; i++)
        {
            a -= 0.05f;
            text.color = new Color(1, 1, 1, a);
            image1.color = new Color(1, 1, 1, a);
            image2.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.05f);
        }
        text.gameObject.SetActive(false);
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
