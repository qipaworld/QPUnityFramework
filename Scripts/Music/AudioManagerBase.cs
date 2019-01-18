using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManagerBase : MonoBehaviour {

	//音乐文件
	public AudioSource music;

    public MusicEnum musicType;
	public bool isPlay = true;
	public bool repeat = false;
    
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
		}
		if(musicType.ToString()=="bgm"){
	        dataBase.Bind(Change);
		}else{
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
		isPlay = (data.GetNumberValue ("musicStatus") == 1);
        if (music.loop)
        {
            if (isPlay)
            {
                play();
            }
            else
            {
                stop();
            }
        }
	}
    virtual public void play(){
		if ((!music.isPlaying|| repeat) && isPlay){
			music.Play();
		}
	}
    virtual public void stop(){
		if (music.isPlaying){
			music.Stop();
		}
	}
    virtual public void pause(){
		if (music.isPlaying){
			music.Pause();
		}
	}
    virtual public void SetVolume(float musicVolume){
		music.volume = musicVolume;
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