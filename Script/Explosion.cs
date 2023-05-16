using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("LifeSpan");
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
