using UnityEngine;
using System.Collections;

[System.Reflection.Obfuscation(Exclude = true)]
public class ShowFps : MonoBehaviour
{

    // Use this for initialization  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {
        UpdateTick();
    }

    void OnGUI()
    {
        DrawFps();
    }

    private void DrawFps()
    {
        GUIStyle bb = new GUIStyle();

        if (mLastFps > 50)
        {
            bb.normal.textColor = new Color(0, 1, 0);
        }
        else if (mLastFps > 40)
        {
            bb.normal.textColor = new Color(1, 1, 0);
        }
        else
        {
            bb.normal.textColor = new Color(1.0f, 0, 0);
        }
        bb.fontSize = 40;       //当然，这是字体大小
        GUILayout.Label( "fps: " + mLastFps, bb);

    }

    private long mFrameCount = 0;
    private long mLastFrameTime = 0;
    static long mLastFps = 0;
    private void UpdateTick()
    {
        if (true)
        {
            mFrameCount++;
            long nCurTime = TickToMilliSec(System.DateTime.Now.Ticks);
            if (mLastFrameTime == 0)
            {
                mLastFrameTime = TickToMilliSec(System.DateTime.Now.Ticks);
            }

            if ((nCurTime - mLastFrameTime) >= 1000)
            {
                long fps = (long)(mFrameCount * 1.0f / ((nCurTime - mLastFrameTime) / 1000.0f));

                mLastFps = fps;

                mFrameCount = 0;

                mLastFrameTime = nCurTime;
            }
        }
    }
    public static long TickToMilliSec(long tick)
    {
        return tick / (10 * 1000);
    }
}