using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBgMusic : TriggerUpdateBgMusic
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BgMusicManager.Instance.UpdateMusic(clip, time);
    }
}
