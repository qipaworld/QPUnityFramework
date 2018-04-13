using System;
using System.Collections.Generic;
using UnityEngine;
public enum HintListCellType{Ready,MoveStart,MoveEnd,Remove};
public class HintListCell : MonoBehaviour {

	/// <summary>
    /// 当前状态
    /// </summary>
	public HintListCellType hintListCellType;

	/// <summary>
    /// UI名字
    /// </summary>
	public string uiName;

	/// <summary>
    /// 前一个layer
    /// </summary>
	public GameObject frontLayer;

	/// <summary>
    /// 前一个layer 大小的一半
    /// </summary>
	public Vector3 frontLayerSize;

	/// <summary>
    /// 状态改变回掉方法
    /// </summary>
	public Action<HintListCell> changeCallback = null;

	/// <summary>
    /// 目标位置
    /// </summary>
	public Vector2 targetPosition;

	/// <summary>
    /// 开始的位置
    /// </summary>
	public Vector2 startPosition;

	/// <summary>
    /// 移动速度
    /// </summary>
	public float speed = 500;

	/// <summary>
    /// 移动方向
    /// </summary>
	public Vector2 direction;

	/// <summary>
    /// 停留时间
    /// </summary>
	public float stayTime = 1;

	void sendChange(){
		if (changeCallback!=null){
			changeCallback(this);
		}
	}

	/// <summary>
    /// 初始化方法，需要手动调用
    /// </summary>
	/// <value>sPosition 开始的位置.</value>
	/// <value>tPosition 目标位置.</value>
	/// <value>f 前一个layer，可以为空.</value>
	public void Init(string name, Vector2 sPosition,Vector2 tPosition, GameObject f = null){
		frontLayer = f;
		targetPosition = tPosition;
		startPosition = sPosition;
		transform.position = startPosition;
		direction = tPosition - sPosition;
        direction.Normalize();
        hintListCellType = HintListCellType.Ready;
        uiName = name;
        frontLayerSize = new Vector3();
        if (f){
	        var bg = f.transform.Find("Bg");
	        if(bg){
	            Vector2 tempPosition = bg.GetComponent<RectTransform>().sizeDelta;
	        	if (direction.x>0){
	        		frontLayerSize.x = -tempPosition.x/2;
	        	}
	        	else if(direction.x < 0){
	        		frontLayerSize.x = tempPosition.x/2;
	        	}
	        	if (direction.y>0){
					frontLayerSize.y = -tempPosition.y/2;
	        	}
	        	else if (direction.y < 0){
					frontLayerSize.y = tempPosition.y/2;
	        	}

	        }
        }
		sendChange();
		
	}
	public void StartAction(){
		hintListCellType = HintListCellType.MoveStart;
		sendChange();
	}
	public void EndAction(){
		hintListCellType = HintListCellType.MoveEnd;
		sendChange();
	}
	public void popUI(){
		hintListCellType = HintListCellType.Remove;
		sendChange();
	}

	void Start()
    {
        StartAction();
    }
    void Update()
    {
		if(hintListCellType == HintListCellType.MoveStart){
			bool isMove = false;
        	Vector3 tempDistance = direction *speed*Time.deltaTime;
    		Vector3 tempPosition = transform.position+ tempDistance;
	        if(frontLayer!=null){
	    		Vector3 tempTarget = frontLayerSize + frontLayer.transform.position;
				if(Vector3.Distance(tempTarget, transform.position) <= Vector3.Distance(tempPosition, transform.position)){
					transform.position = tempTarget;
					isMove = true;
				} 
				else if ((direction.x > 0&&tempTarget.x < transform.position.x)||
					(direction.x < 0&&tempTarget.x > transform.position.x)||
					(direction.y > 0&&tempTarget.y < transform.position.y)||
					(direction.y < 0&&tempTarget.y > transform.position.y)
					){
	        		transform.position = tempTarget;
	        		isMove = true;
	        	}
	    	}

	    	if(!isMove){
				if(Vector3.Distance(targetPosition, transform.position) <= Vector3.Distance(tempPosition, transform.position)){
					transform.position = targetPosition;
					EndAction();
				}else{
					transform.position = tempPosition;
				}

    		}
    	}else if (hintListCellType == HintListCellType.MoveEnd){
    		stayTime-=Time.deltaTime;
    		if(stayTime <= 0){
    			popUI();
    			UIController.Instance.Pop(uiName);
    		}
    	}

    }
    
}
