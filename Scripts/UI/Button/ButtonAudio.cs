using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerDownHandler {

	public AudioSource music;
	public MusicEnum musicType;
	bool isPlay = true;
	void Start() {
		if (!music)
		{
			music = transform.GetComponent<AudioSource>();
		}
		DataManager.Instance.getData("musicData").GetDataValue(musicType.ToString()).Bind(change);
	}	
	public void change(DataBase data)
	{
		isPlay = (data.GetIntValue ("musicStatus") == 1);
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (isPlay) {
			//播放音乐
			music.Play();
		}
	}

}
