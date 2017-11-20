using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IphonexDown : MonoBehaviour
{

    // Use this for initializationiPhone10,3
    void Start()
    {
        if (SystemInfo.deviceModel == "iPhone10,3" || SystemInfo.deviceModel == "iPhone10,6")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 80, transform.position.z);
        }
    }
}
