using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpdateBgMusic : TriggerEnemyBase
{
    public AudioClip clip = null;
    public string musicName = "";
    public float time = 0.5f;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (clip != null)
        {
            musicName = clip.name;
            BgMusicManager.Instance.AddClip(clip.name, clip);
        }
    }
    
    protected virtual void Start()
    {
        
    }
    protected virtual void OnDestroy()
    {
        if (clip != null)
        {
            BgMusicManager.Instance.RemoveClip(clip.name);
        }
    }
    public override void Trigger(Collider2D collider)
    {
        BgMusicManager.Instance.UpdateMusic(clip, time);
    }
}
