using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBgMusices : TriggerUpdateBgMusic
{
    public List<AudioClip> clips = new List<AudioClip>();
    int musicIndex = 0;
    public Text nameTex = null;
    // Start is called before the first frame update
    protected override void Awake()
    {
        //base.Awake();

        if (clips.Count > 0)
        {
            foreach (AudioClip ac in clips)
            {
                BgMusicManager.Instance.AddClip(ac.name, ac);
            }
        }
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
        foreach (AudioClip ac in clips)
        {
            BgMusicManager.Instance.RemoveClip(ac.name);
        }
        string key = GameBaseStatus.Instance.GetRunSceneName();
        EncryptionManager.SetString(key + "bgMusicName", GameBaseStatus.Instance.GetGBMusicName());
        EncryptionManager.Save();
    }
    public void NextMusic() {
        musicIndex++;
        if(musicIndex >= clips.Count)
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
        musicName = clips[index].name;
        if(nameTex != null)
        {
            nameTex.text = musicName.Replace('_',' ');
        }
        musicIndex = index;
    }
    public void PlayMusic(string name)
    {
        int i = 0;
        foreach (AudioClip ac in clips)
        {
            if(ac.name == name)
            {
                PlayMusic(i);
                break;
            }
            i++;
        }
    }
}
