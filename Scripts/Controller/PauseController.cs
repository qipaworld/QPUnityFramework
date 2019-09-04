using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    // Update is called once per frame
    public void ChangePause()
    {
        if (!GameBaseStatus.Instance.IsPauseGameKey("Space"))
        {
            GameBaseStatus.Instance.PauseGame("Space");
        }
        else if (GameBaseStatus.Instance.IsPauseGameKey("Space"))
        {
            GameBaseStatus.Instance.ResumeGame("Space");
        }
    }
}
