using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGravityController : MonoBehaviour
{
    public Text testTex  = null;
    public bool is2D = true;
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
            if(is2D){

                Physics.gravity = new Vector3(Input.acceleration.x*2,Input.acceleration.y*2,0);
            }else{
                Physics.gravity = Input.acceleration;
            }
#endif
        }
    }
}
