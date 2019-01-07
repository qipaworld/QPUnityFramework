using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBgMusic : MonoBehaviour
{
    
    public AudioClip clip;
    public float time = 1;
    // Start is called before the first frame update
    void Start()
    {
        BgMusicManager.Instance.UpdateMusic(clip, time);
    }
}
