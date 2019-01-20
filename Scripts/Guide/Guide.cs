using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlatformEnum { Any,Phone,PC }
public class Guide : MonoBehaviour {

    // Use this for initialization
    public GameObject hintObj = null;
    public string hintNum = "";
    public string hintNumNext = "";
    public PlatformEnum platform = PlatformEnum.Any;
    PlatformEnum runTimePlatform = PlatformEnum.Any;

    public virtual void Start () {
        if (QipaWorld.Utils.IsPhone())
        {
            runTimePlatform = PlatformEnum.Phone;
        }
        else
        {
            runTimePlatform = PlatformEnum.PC;
        }

        if (hintObj == null)
        {
            hintObj = gameObject;
        }
        GuideManager.Instance.BindGuide(Change);
    }
	
    public virtual void OnDestroy()
    {
        GuideManager.Instance.UnbindGuide(Change);
    }
    public bool CheckPlatform()
    {
        if(platform == PlatformEnum.Any || platform == runTimePlatform )
        {
            return true;
        }
        return false;
    }
    public virtual void Change(DataBase data)
    {
        if (GuideManager.Instance.GetGuideNum()== hintNum&& CheckPlatform())
        {
            hintObj.SetActive(true);
        }
        else
        {
            hintObj.SetActive(false);
        }
    }
    public void GuideNext()
    {
        if (GuideManager.Instance.GetGuideNum() == hintNum)
        {
            GuideManager.Instance.SetGuideNum(hintNumNext);
        }
    }
}
