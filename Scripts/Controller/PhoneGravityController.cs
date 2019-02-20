using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGravityController : MonoBehaviour
{
    public Text testTex  = null;

    // Update is called once per frame
    void Update()
    {
        if(testTex != null)
        {
            testTex.text = Input.acceleration.ToString();
        }
        if (QipaWorld.Utils.IsPhone())
        {
#if !UNITY_EDITOR
            Physics.gravity = Input.acceleration;
#endif
        }
    }
}
