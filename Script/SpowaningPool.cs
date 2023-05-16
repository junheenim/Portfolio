using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class SpowaningPool : MonoBehaviour
{
    public GameObject monster;
    public Transform[] spowanPoints;
    GameObject sMonster;
    public int maxMonster = 10;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            int idx = Random.Range(1, spowanPoints.Length);
            sMonster = Instantiate(monster, spowanPoints[idx].position, spowanPoints[idx].rotation);
            sMonster.transform.parent = transform;
        }
        StartCoroutine("SpowanMonster");
    }

    IEnumerator SpowanMonster()
    {
        while (true)
        {
            int count = transform.childCount;
            if (count - 10 <= maxMonster)
            {
                int idx = Random.Range(1, spowanPoints.Length);
                sMonster = Instantiate(monster, spowanPoints[idx].position, spowanPoints[idx].rotation);
                sMonster.transform.parent = transform;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
