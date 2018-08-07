using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlay : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> obj = new List<GameObject>();
	public float time = 1;
	void Start () {
		
		StartCoroutine(Ready());
	}
	
	 private IEnumerator Ready()
    {
        yield return new WaitForEndOfFrame();

        foreach(GameObject o in obj){
    		yield return new WaitForSeconds( UnityEngine.Random.value*time);

    		if(o==null){
    			Debug.Log(gameObject.name + "___________________________________________________");
    		}else{
		         o.SetActive(true);
    		}
    	}
           
        
    }
}
