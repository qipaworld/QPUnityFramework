using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandom : AudioManagerBase {
    public List<AudioSource> musicList = new List<AudioSource>();
    override public void play()
    {
        if ((!music.isPlaying || repeat) && isPlay)
        {
            musicList[Mathf.FloorToInt(musicList.Count * Random.value)].Play();
            music.Play();
        }
    }
    override public void stop()
    {
        foreach(AudioSource audio in musicList)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
    }
    override public void pause()
    {
        
    }
}
