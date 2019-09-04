using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandom : MonoBehaviour
{
    public List<AudioClip> musicList = new List<AudioClip>();
    public float time = 0f;
    private void Start()
    {
        play();
    }
    public void play()
    {
        BgMusicManager.Instance.UpdateMusic(musicList[Mathf.FloorToInt(musicList.Count * Random.value)], time);
        //if ((!music.isPlaying || repeat) && isPlay)
        //{
        //    musicList[Mathf.FloorToInt(musicList.Count * Random.value)].Play();
        //    music.Play();
        //}
    }
    //override public void stop()
    //{
    //    foreach(AudioSource audio in musicList)
    //    {
    //        if (audio.isPlaying)
    //        {
    //            audio.Stop();
    //        }
    //    }
    //}
    //override public void pause()
    //{
        
    //}
}
