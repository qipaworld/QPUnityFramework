using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneGravityController : MonoBehaviour
{
    public Text testTex  = null;
    public bool is2D = true;
    float gravityScale = 1;
    public float scale = 9.81f;
    // Update is called once per frame
    private void Start()
    {
        GravityManager.Instance.Bind(ChangeStatus);
    }
    private void OnDestroy()
    {
        GravityManager.Instance.Unbind(ChangeStatus);
    }
    void ChangeStatus(DataBase data)
    {
        gravityScale = (float)data.GetNumberValue("gravityScale");
    }
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
                
                Physics.gravity = new Vector3(Input.acceleration.x*scale*gravityScale,y*scale*gravityScale,0);
            }else{
                Physics.gravity = Input.acceleration;
            }
#endif
        }
    }
}
