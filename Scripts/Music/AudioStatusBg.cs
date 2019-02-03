using UnityEngine;

public class AudioStatusBg : AudioStatusBase
{
    // public override void Change(DataBase data)
    //    {

    //    	//base.Change(data);
    // 	isPlay = (data.GetNumberValue ("musicStatus") == 1);


    //        if (isPlay)
    //        {
    //            if (!isAudioPlay && isReady)
    //            {
    //                play();
    //            }
    //        }
    //        else
    //        {
    //            pause();
    //        }
    public override void Change(DataBase data)
    {
        UpdateStatus(data);
        if (isPlay)
        {
            if (!IsPlaying() && music.loop && (isAudioPlay || isReady))
            {
                PlayEx();
            }
        }
        else
        {
            PauseEx();
        }
    }
    // }
    //override public void play()
    //{
    //    base.play();
    //}
    //override public void stop()
    //{
    //    base.stop();
    //}
}