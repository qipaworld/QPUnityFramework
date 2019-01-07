using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager
{
    public static BgMusicManager instance = null;
    AudioManagerBase audioManager;
    Dictionary<string,AudioClip> clipDic = new Dictionary<string, AudioClip>();
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
    public void UpdateMusic(AudioClip clip,float t,bool b = false)
    {
        
        if (clip!=null&&GameBaseStatus.Instance.GetGBMusicName() == clip.name)
        {
            return;
        }
        if (clip != null)
        {
            GameBaseStatus.Instance.SetGBMusicName(clip.name);
        }
        else
        {
            GameBaseStatus.Instance.SetGBMusicName("Null");
        }
        this.clip = clip;
        this.time = 0;
        this.maxTime = t;
        this.isFadeOut = true;
        this.isUptate = true;
    }
    public void UpdateMusic(string clip, float t)
    {
        if (clipDic.ContainsKey(clip))
        {
            UpdateMusic(clipDic[clip], t);
        }
        else if(clip == "Null")
        {
            UpdateMusic(null,t,true);
        }
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
                    }
                    else
                    {
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
    public void AddClip(string k,AudioClip a)
    {
        if (!clipDic.ContainsKey(k))
        {
            clipDic.Add(k, a);
        }
    }
    public void RemoveClip(string k)
    {
        if (clipDic.ContainsKey(k))
        {
            clipDic.Remove(k);
        }
    }
}
