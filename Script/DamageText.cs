using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
    public Color color = Color.yellow;
    public Vector3 enemy;
    public Vector3 pos;
    Text text;
    public int textSize = 40;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void Start()
    {
        text.color = color;
        text.fontSize = textSize;
        transform.position = Camera.main.WorldToScreenPoint(enemy);
        StartCoroutine("Destroy");
    }
    void Update()
    {
        if(gameObject.activeSelf)
        {
            text.fontSize = textSize;
            enemy.y += 0.005f;
            transform.position = Camera.main.WorldToScreenPoint(enemy);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    IEnumerator Destroy()
    {
        for (int i = 0; i < 20; i++)
        {
            if (textSize >= 20)
            {
                textSize -= 1;
            }
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }

}
