using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour {

    public float distance = 0;
	Tweener nowTweener;
	RectTransform rTransform;
    private void Start()
    {
        if(distance == 0)
        {
            distance = Screen.width;
        }
		rTransform = GetComponent<RectTransform> ();
    }
    public void moveStart()
    {
		float positionX = distance;
		if (rTransform.position.x <= 0) {
			positionX = 0;
		} 
		nowTweener = transform.DOLocalMoveX(positionX, 1);
    }
	public void moveAutoStart()
	{
		float positionX = distance;
		if (rTransform.position.x >= 0) {
			positionX = 0;
		} 
		nowTweener = transform.DOLocalMoveX(positionX, 1);
	}
	public void moveCancel()
	{
//		nowTweener = transform.DOLocalMoveX(positons[(status-1) %2], 1);
	}
//	public void moveToEnd()
//	{
//		nowTweener = transform.DOLocalMoveX(distance, 1);
//	}
//	public void moveToBegin()
//	{
//		nowTweener = transform.DOLocalMoveX(-distance, 1);
//	}
	public void moveKill()
	{
		nowTweener.Kill ();
	}

}
