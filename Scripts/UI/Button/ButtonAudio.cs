using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : AudioManagerBase, IPointerDownHandler {

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isPlay && music) {
			//播放音乐
			music.Play();
		}
	}

}
