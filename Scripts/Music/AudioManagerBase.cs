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