using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public MusicEnum musicType;
    string saveKey;
    string musicName;
    DataBase data;
    void Start() {
        musicName = musicType.ToString();
        saveKey = musicName + "AudioController";
        data = DataManager.Instance.getData("musicData").GetDataValue(musicName);
        //data.SetNumberValue("musicStatus", EncryptionManager.GetInt(saveKey, 1));
    }
	public void change(){
        if (getStatus() == 1)
        {
            setStatus(0);
        }
        else
        {
            setStatus(1);
        }
    }
	public void play(){
		setStatus (1);
	}
	public void stop(){
		setStatus (0);
	}
	public void pause(){
		setStatus (0);
	}
	public void setStatus(int status){
		EncryptionManager.SetInt(saveKey, status);
		EncryptionManager.Save();
		data.SetNumberValue ("musicStatus",status);
	}
	public double getStatus(){
		return data.GetNumberValue("musicStatus");
	}
}