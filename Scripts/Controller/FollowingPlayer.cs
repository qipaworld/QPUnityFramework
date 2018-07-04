using UnityEngine;
using System.Collections;

public class FollowingPlayer : MonoBehaviour 
{
	public Transform target;
	public float speed = 0.1f;
    public float z = -10f;
    
	public void Update()
	{
		if (target) 
		{
		    Vector3 v = Vector3.Lerp(transform.position, target.position, speed);
			transform.position = new Vector3(v.x, v.y,z);
        }
	}
}
