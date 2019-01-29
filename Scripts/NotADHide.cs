using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotADHide : MonoBehaviour
{
    void Start()
    {
#if QIPAWORLD_NOTAD
        gameObject.SetActive(false);
#endif
    }
}
