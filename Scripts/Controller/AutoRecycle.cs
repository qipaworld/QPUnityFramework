using UnityEngine;
using System.Collections;

public class AutoRecycle : MonoBehaviour {
    public float time;
    public void Init() {
        Invoke("DestroyIt",time);
    }
	void DestroyIt(){
        //Destroy(gameObject);
        GameObjManager.Instance.RecycleObj(gameObject);
    }

	
}
