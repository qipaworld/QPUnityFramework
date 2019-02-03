using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMuiscSliderController : MonoBehaviour
{
    // Start is called before the first frame update
    SliderController sliderController;
    void Start()
    {
        sliderController = GetComponent<SliderController>();
        sliderController.SetAction(ChangeMusic);
        sliderController.SetSaveKey(MusicEnum.bgm.ToString() + "musicVolume");
    }

   void ChangeMusic(float f)
    {
        //BgMusicManager.Instance.SetVolume(f);
        if (f <= 0.00001f)
        {
            sliderController.SetValue("musicStatus", 0);
        }
        else
        {
            sliderController.SetValue("musicStatus", 1);
        }
    }
}
