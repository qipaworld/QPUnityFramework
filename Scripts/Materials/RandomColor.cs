using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color minColor;
    public Color maxColor;
    public bool isBright = true;
    // Start is called before the first frame update
    void Start()
    {
        float[] c = { Random.value + minColor.r , Random.value + minColor.g, Random.value + minColor.b, Random.value + minColor.a };
        
        if (isBright)
        {
            int k = (int)((c.Length-1) * Random.value);
            c[k] = maxColor[k];
            if (k != 0)
            {
                c[k - 1] = 0;
            }
            else
            {
                c[2] = 0;
            }
        }
        GetComponent<Renderer>().material.color = new Color(c[0]<=maxColor.r? c[0] : maxColor.r,
            c[1] <= maxColor.g ? c[1] : maxColor.g,
            c[2] <= maxColor.b ? c[2] : maxColor.b,
            c[3] <= maxColor.a ? c[3] : maxColor.a);
        
    }


}
