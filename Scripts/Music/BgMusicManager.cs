using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager
{
    public static BgMusicManager instance = null;
    AudioManagerBase audioManager;
    float time = 0;
    float maxTime = 0;
    AudioClip clip;
    bool isFadeOut = false;
    bool isUptate = false;
    static public BgMusicManager Instance
    {
        get
        {
            return instance;
        }
    }

    static public void Init(AudioManagerBase audio)
    {
        if (instance == null)
        {
            instance = new BgMusicManager();
            instance.audioManager = audio;
            Timer.Instance.BindUpdate(instance.Update);
        }
    }
    public void UpdateMusic(AudioClip clip,float t)
    {
        
        if (clip!=null&&GameBaseStatus.Instance.GetGBMusicName() == clip.name)
        {
            return;
        }
        this.clip = clip;
        this.time = 0;
        this.maxTime = t;
        this.isFadeOut = true;
        this.isUptate = true;
    }
    public void Update()
    {
        if (isUptate)
        {
            if (isFadeOut)
            {
                time += Time.deltaTime;
                if(time >= maxTime|| !audioManager.IsPlaying())
                {
                    isFadeOut = false;
                    time = maxTime;
                    if (clip != null)
                    {
                        audioManager.SetClip(clip);
                        audioManager.play();
                        GameBaseStatus.Instance.SetGBMusicName(clip.name);
                    }
                    else
                    {
                        GameBaseStatus.Instance.SetGBMusicName("");
                        audioManager.stop();
                    }
                    
                }
            }
            else
            {
                time -= Time.deltaTime;
                if (time<=0)
                {
                    isUptate = false;
                    time = 0;
                }
            }
            audioManager.SetVolume((maxTime - time) / maxTime);
        }
    }
}
