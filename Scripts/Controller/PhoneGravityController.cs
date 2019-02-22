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
                float y = Input.acceleration.y;
                if (y > 0.15f)
                {
                    y = 1;
                }else if (y < -0.15f)
                {
                    y = -1;
                }
                Physics.gravity = new Vector3(Input.acceleration.x*2,y,0);
            }else{
                Physics.gravity = Input.acceleration;
            }
#endif
        }
    }
}
