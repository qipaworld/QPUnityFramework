using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuiscSliderController : MonoBehaviour
{
    // Start is called before the first frame update
    SliderController sliderController;
    public AudioStatusBase music;
    public MusicEnum musicType = MusicEnum.sound;
    void Start()
    {
        music = GetComponent<AudioStatusBase>();
        sliderController = GetComponent<SliderController>();
        sliderController.SetAction(ChangeMusic);
        sliderController.SetSaveKey(musicType.ToString() + "musicVolume");
    }

   void ChangeMusic(float f)
    {
        //AudioManager.Instance
        //BgMusicManager.Instance.SetVolume(f);
        if (f <= 0.00001f)
        {
            sliderController.SetValue("musicStatus", 0);
        }
        else
        {
            sliderController.SetValue("musicStatus", 1);
        }
        music.play();
    }
}
