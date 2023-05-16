using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    float y;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        y += 100 * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, y, 0);
    }
}
