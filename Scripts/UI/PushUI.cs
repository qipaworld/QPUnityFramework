using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushUI : MonoBehaviour {

	public string uiName;
	public void Push() {
		UIController.Instance.Push(uiName);
	}
}
