using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static AudioManager instance = null;
    DataBase muiscDataBase;
    static public AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    static public void Init(GameObject MainGameObject)
    {
        if (instance == null)
        {
            instance = new AudioManager();

            instance.muiscDataBase = DataManager.Instance.addData("musicData");
            Transform soundObj = MainGameObject.transform.Find("DefaultSound");
            AudioSource[] music = soundObj.GetComponents<AudioSource>();
            int musicId = 0;
            foreach (MusicEnum key in Enum.GetValues(typeof(MusicEnum)))
            {
                
                DataBase muiscData = new DataBase();
                double musicVolume = EncryptionManager.GetDouble(key.ToString() + "musicVolume", 1);
                muiscData.SetNumberValue("musicStatus", musicVolume>0?1:0);
                muiscData.SetNumberValue("musicVolume", musicVolume);
                AudioStatusBase audio = null;
                
                if (key != MusicEnum.bgm)
                {
                    audio = soundObj.gameObject.AddComponent<AudioStatusBase>();
                }
                else
                {
                    audio = soundObj.gameObject.AddComponent<AudioStatusBg>();
                    BgMusicManager.Init(audio);
                }
                audio.musicType = key;
                audio.repeat = true;
                audio.music = music[musicId];
                muiscData.SetObjectValue("defaultAudioStatusBase", audio);
                muiscData.SetObjectValue("defaultMusic", music[musicId]);
                muiscData.Sync = true;
                instance.muiscDataBase.SetDataValue(key.ToString(), muiscData);
                musicId++;
            }
            //instance.audioManager = audio;
            //Timer.Instance.BindUpdate(instance.Update);
        }
    }
    //public void SetVolume(string key)
    //{

    //}
    public AudioStatusBase GetDefaultAudioStatusBase(string key)
    {
        return muiscDataBase.GetDataValue(key).GetObjectValue("defaultAudioStatusBase") as AudioStatusBase;
    }
    public AudioStatusBase GetDefaultSoundAudio()
    {
        return GetDefaultAudioStatusBase(MusicEnum.sound.ToString());
    }
    public float GetVolume(string key)
    {
        return (float)muiscDataBase.GetDataValue(key).GetNumberValue("musicVolume");
    }
    public float GetBGVolume()
    {
        return GetVolume(MusicEnum.bgm.ToString());
    }
    public float GetSoundVolume()
    {
        return GetVolume(MusicEnum.sound.ToString());
    }
}
