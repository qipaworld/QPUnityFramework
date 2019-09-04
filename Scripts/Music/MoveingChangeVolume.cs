using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingChangeVolume : MonoBehaviour
{
    public AudioStatusBase music;
    public Transform target;
    Vector3 lastPosition;
    public float maxDistance = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(music == null)
        {
            music = GetComponent<AudioStatusBase>();
            target = transform;
        }
        lastPosition = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPosition == target.position&&music.GetVolume()!=0)
        {
            
            music.SetVolume(0);
        }
        else
        {
            float sqrMagnitude = (lastPosition - target.position).magnitude;
            if (sqrMagnitude > maxDistance)
            {
                music.SetVolume(1);
            }
            else
            {
                music.SetVolume(sqrMagnitude / maxDistance);
            }
        }
        lastPosition = target.position;
    }
}
