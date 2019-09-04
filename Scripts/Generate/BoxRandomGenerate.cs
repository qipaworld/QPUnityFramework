using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRandomGenerate : MonoBehaviour
{
    public BoxCollider box;
    public string objPath;
    public int num=10;
    public int maxNum;
    int nowNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    public IEnumerator Generate()
    {
        for (int i = 0; i < num; i++)
        {
            if (i % maxNum == 0)
            {
                yield return null;
            }
        }
    }
}
