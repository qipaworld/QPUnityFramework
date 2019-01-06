using UnityEngine;
using System.Collections;

public class FollowingPlayer : MonoBehaviour 
{
	public Transform target;
	//public float speed = 0.1f;
    //public float z = -10f;
    public float smooth = 4.5f;
    public bool lockingX = false;
    public bool lockingY = false;
    public bool lockingZ = false;
    public void Start()
    {
        if (target)
        {
            //Vector3 v = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = GetPosition();
        }
    }
    Vector3 GetPosition()
    {
       
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (!lockingX)
        {
            x = target.position.x;
        }
        if (!lockingY)
        {
            y = target.position.y;
        }
        if (!lockingZ)
        {
            z = target.position.z;
        }
        return  new Vector3(x, y, z);
      
    }
    public void Update()
	{
		if (target) 
		{
            transform.position = Vector3.Lerp(transform.position, GetPosition(), smooth * Time.deltaTime);
			//transform.position = new Vector3(v.x, v.y,z);
        }
	}
}
