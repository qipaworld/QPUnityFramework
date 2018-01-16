using UnityEngine;

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
		dataBase.Bind(Change);
    }
	public virtual void Change(DataBase data)
    {
		isPlay = (data.GetNumberValue ("musicStatus") == 1);
	}
	public void play(){
		if ((!music.isPlaying|| repeat) && isPlay){
			music.Play();
		}
	}
	public void stop(){
		if (music.isPlaying){
			music.Stop();
		}
	}
	public void pause(){
		if (music.isPlaying){
			music.Pause();
		}
	}
	public void setVolume(float musicVolume){
		music.volume = musicVolume;
	}
}