using UnityEngine;
using System.Collections;

public class FollowingPlayer : MonoBehaviour 
{
	public Transform target;
	//public float speed = 0.1f;
    public float z = -10f;
    public float smooth = 4.5f;
    public void Start()
    {
        if (target)
        {
            //Vector3 v = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = new Vector3(target.position.x, target.position.y, z);
        }
    }
    public void Update()
	{
		if (target) 
		{
		    Vector3 v = Vector3.Lerp(transform.position, target.position, smooth * Time.deltaTime);
			transform.position = new Vector3(v.x, v.y,z);
        }
	}
}
