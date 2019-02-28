using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTapHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IAPManager.Instance.isTapTap)
        {
            gameObject.SetActive(false);
        }
    }
}
