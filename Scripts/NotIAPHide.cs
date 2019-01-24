using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotIAPHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if QIPAWORLD_NOTIAP
        gameObject.SetActive(false);
#endif
    }
}
