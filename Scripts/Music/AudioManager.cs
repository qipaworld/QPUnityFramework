using UnityEngine;

public class AudioManager : MonoBehaviour {

	//音乐文件
	public AudioSource music;

    string musicName = "name";
    public MusicEnum musicType;
    //音量
    void Start() {
        if (!music)
        {
            music = transform.GetComponent<AudioSource>();
        }
        musicName = musicType.ToString();
        DataManager.Instance.getData("musicData").GetDataValue(musicName).Bind(change);
    }
	public void change(DataBase data)
    {
        if (data.GetNumberValue("musicStatus") == 1) {
			play ();
		} else {
			pause ();
		}
	}
	public void play(){
		if (!music.isPlaying){
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