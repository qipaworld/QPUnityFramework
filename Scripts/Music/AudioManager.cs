using UnityEngine;

public class AudioManager : AudioManagerBase {
    public bool isAudioPlay = true;
	public override void Change(DataBase data)
    {
        
    	//base.Change(data);
		isPlay = (data.GetNumberValue ("musicStatus") == 1);
        
        
        if (isPlay)
        {
            if (!isAudioPlay && isReady)
            {
                play();
            }
        }
        else
        {
            pause();
        }
        
	}
    //override public void play()
    //{
    //    base.play();
    //}
    //override public void stop()
    //{
    //    base.stop();
    //}
}