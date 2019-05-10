using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBgMusices : TriggerUpdateBgMusic
{
    public List<string> clips = new List<string>();
    int musicIndex = 0;
    public Text nameTex = null;
    // Start is called before the first frame update
    protected override void Awake()
    {
        //base.Awake();

        //if (clips.Count > 0)
        //{
        //    //foreach (string ac in clips)
        //    //{
        //    //    Debug.Log(ac);
        //    //    BgMusicManager.Instance.AddClip(ac, ac);
        //    //}
        //}
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        string musicN = EncryptionManager.GetString(GameBaseStatus.Instance.GetRunSceneName() + "bgMusicName", "");
        if(musicN != "")
        {
            //BgMusicManager.Instance.UpdateMusic(musicN, 0.5f);
            PlayMusic(musicN);
        }
        else
        {
            PlayMusic(0);
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        //foreach (AudioClip ac in clips)
        //{
        //    BgMusicManager.Instance.RemoveClip(ac.name);
        //}
        
    }
    public void NextMusic() {
        musicIndex++;
        if (musicIndex >= clips.Count)
        {
            musicIndex = 0;
        }
        PlayMusic(musicIndex);
    }
    public void PreviousMusic()
    {
        musicIndex--;
        if (musicIndex <0)
        {
            musicIndex = clips.Count-1;
        }
        PlayMusic(musicIndex);
    }
    public void PlayMusic(int index)
    {
        BgMusicManager.Instance.UpdateMusic(clips[index], time);
        musicName = clips[index];
        if(nameTex != null)
        {
            nameTex.text = musicName.Replace('_',' ');
        }
        musicIndex = index;
        string key = GameBaseStatus.Instance.GetRunSceneName();
        EncryptionManager.SetString(key + "bgMusicName", GameBaseStatus.Instance.GetGBMusicName());
        EncryptionManager.Save();
    }
    public void PlayMusic(string name)
    {
        int i = 0;
        foreach (string ac in clips)
        {
            if(ac == name)
            {
                PlayMusic(i);
                break;
            }
            i++;
        }
    }
}
