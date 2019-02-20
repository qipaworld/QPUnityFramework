using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIosScoreBox : MonoBehaviour
{
    public float popTime = 10;
    bool isPop = true;

    // Update is called once per frame
    void Update()
    {
        if (isPop)
        {
            popTime -= Time.deltaTime;
            if (popTime <= 0)
            {
                isPop = false;
                ScoreTheGameManager.Instance.PopScoreBox();
            }
        }
    }
}
