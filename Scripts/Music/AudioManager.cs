using UnityEngine;

public class AudioManager : AudioManagerBase {
	
	public override void Change(DataBase data)
    {
    	base.Change(data);
		isPlay = (data.GetNumberValue ("musicStatus") == 1);

        if (isPlay) {
			play ();
		} else {
			pause ();
		}
	}
	
}