using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerDownHandler {

    public AudioStatusBase audioStatusBase = null;
    private void Start()
    {
        if (!audioStatusBase)
        {
            audioStatusBase = QipaWorld.AudioManager.Instance.GetDefaultSoundAudio();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
	{
        audioStatusBase.play();

    }

}
