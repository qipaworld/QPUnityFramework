
using UnityEngine;
public class LoadingCell 
{
    private System.Action<Object> callback;
    private ResourceRequest request;
    public bool IsOver = false;
    public LoadingCell(string key, System.Action<Object> callback)
    {
        request = Resources.LoadAsync(key);
        this.callback = callback;

        Timer.Instance.BindUpdate(Update);
    }
    public void Update()
    {
        //Debug.Log("aaaaa!"); //异步方法至少需要一帧的时间才能生效！

        if (request != null&& !IsOver)
        {
            if (request.isDone)
            {
                //Debug.Log("异步加载完成了！");
                IsOver = true;

                Timer.Instance.UnbindUpdate(Update);
                callback(request.asset);
            }
            else
            {
                //Debug.Log("已经过了一帧了!"); //异步方法至少需要一帧的时间才能生效！
            }
        }

        
    }

}
public class LoadObjManager {

	public static LoadObjManager instance = null;
    //protected Dictionary<string, Object> gameObjLoadDatas = new Dictionary<string, Object>();
    //protected List<LoadingCell> LoadingCells = new List<LoadingCell>();

	static public LoadObjManager Instance
	{
	   get {
            if (instance == null) {
                Init();
            }
	       return instance;
	   }
	   // set {instance = value; }
	}
	public static void Init(){
		instance = new LoadObjManager();
	}
	
	public T GetLoadObj<T>(string key) where T : Object
    {

        //T objLoad = default(T);
        //if (gameObjLoadDatas.ContainsKey(key))
        //{
        //    objLoad = gameObjLoadDatas[key] as T;
        //    if(!objLoad){
        //        objLoad = Resources.Load<T>(key);
        //        gameObjLoadDatas[key] = objLoad;
        //    }
        //    return objLoad;
        //}
        
        //objLoad = Resources.Load<T>(key);
        //gameObjLoadDatas.Add(key, objLoad);
        
        //return objLoad;
        return Resources.Load<T>(key);
    }
    public void GetLoadObjAsync(string key, System.Action<Object> a )
    {
        new LoadingCell(key, a);
    }
    // public void RemoveObj(string key)
    // {
    //     if (gameObjDatas.ContainsKey(key))
    //     {
    //         foreach (var o in gameObjDatas[key])
    //         {
    //             GameObject.Destroy(o);
    //         }
    //         gameObjDatas.Clear();
    //     }
    // }

}
