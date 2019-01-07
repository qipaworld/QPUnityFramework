using UnityEngine;

public class AudioManager : AudioManagerBase {
    public bool isAudioPlay = true;
    bool isReady = false;
	public override void Change(DataBase data)
    {
        
    	base.Change(data);
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
    override public void play()
    {
        base.play();
        isReady = true;
    }
    override public void stop()
    {
        base.stop();
        isReady = false;
    }
}