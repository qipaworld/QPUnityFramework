using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TriggerEnemyType { Idle,Run, Complete, Restore }
public class TriggerEnemyBase : MonoBehaviour {
    public TriggerEnemyType type = TriggerEnemyType.Idle;

    public List<string> targetTags = new List<string>();
    //public string targetTag;
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        foreach(string t in targetTags)
        {
            if(t == collider.tag)
            {
                Trigger(collider);
            }
        }
    }
    
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        foreach (string t in targetTags)
        {
            if (t == collider.tag)
            {
                Trigger(collider);
            }
        }
    }
    
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        //foreach (string t in targetTags)
        //{
        //    if (t == collider.tag)
        //    {
        //        Trigger(collider);
        //    }
        //}
    }
    // Use this for initialization
    public virtual void Trigger (Collider2D collider) {
		
	}
	
}
