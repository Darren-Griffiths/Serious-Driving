using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveWayCheck : MonoBehaviour {

	public bool isFree = true;

	void OnTriggerEnter() {
		isFree = false;
	}

	void OnTriggerExit() {
		isFree = true;
	}
}
