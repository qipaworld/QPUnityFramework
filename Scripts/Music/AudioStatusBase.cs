using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioStatusBase : MonoBehaviour {

	//音乐文件
	public AudioSource music;

    public MusicEnum musicType;
	public bool isPlay = true;
	public bool repeat = false;
    protected bool isReady = false;
    public bool isAudioPlay = false;
    float maxVolume = -1;
    float nowVolume = 1;
    //
    //音量
    void Start() {
    	DataBase dataBase = DataManager.Instance.getData("musicData").GetDataValue(musicType.ToString());
		if (!music)
		{
			music = transform.GetComponent<AudioSource>();
		}
		if (!music)
		{
            
			music = dataBase.GetObjectValue("defaultMusic") as AudioSource;
            maxVolume = (dataBase.GetObjectValue("defaultAudioStatusBase") as AudioStatusBase).maxVolume;

        }
        if (music)
        {
            isAudioPlay = IsPlaying();
            if (maxVolume == -1)
            {
                maxVolume = music.volume;
            }
        }
		if(musicType.ToString()=="bgm"){
	        dataBase.Bind(Change);
        }
        else
        {
			DataManager.Instance.getData("musicData").GetDataValue("sound").Bind(Change);
        }
    }
    private void OnDestroy()
    {
    	if(musicType.ToString()=="bgm"){
	        DataManager.Instance.getData("musicData").GetDataValue(musicType.ToString()).Unbind(Change);
		}else{
			DataManager.Instance.getData("musicData").GetDataValue("sound").Unbind(Change);
		}
        
    }
    public virtual void Change(DataBase data)
    {
        UpdateStatus(data);
        if (isPlay)
        {
            if (!IsPlaying()&&music.loop&&(isAudioPlay||isReady))
            {
                PlayEx();
            }
        }
        else
        {
            StopEx();
        }
        //SetVolume();
	}
    virtual public void UpdateStatus(DataBase data)
    {
        nowVolume = (float)data.GetNumberValue("musicVolume");
        SetVolume(1);

        isPlay = (data.GetNumberValue ("musicStatus") == 1);
    }
    virtual public void PlayEx()
    {
        if ((!music.isPlaying || repeat) && isPlay)
        {
            music.Play();
        }

    }
    virtual public void play(){
        PlayEx();
        isReady = true;

    }
    virtual public void StopEx()
    {
        if (music.isPlaying)
        {
            music.Stop();
        }
    }
    virtual public void stop(){
        StopEx();
        isReady = false;

    }
    virtual public void PauseEx()
    {
        if (music.isPlaying)
        {
            music.Pause();
        }
    }
    virtual public void pause(){
        PauseEx();
        isReady = false;
    }
    virtual public void SetVolume(float musicVolume){
		music.volume = musicVolume * maxVolume*nowVolume;
	}
    virtual public float GetVolume()
    {
        return music.volume;
    }
    public bool IsLoop()
    {
        return music.loop;
    }
    public void SetClip(AudioClip clip)
    {
        music.clip = clip;
    }
    public bool IsPlaying()
    {
        return music.isPlaying;
    }
}