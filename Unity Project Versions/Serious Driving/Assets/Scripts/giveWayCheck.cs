using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveWayCheck : MonoBehaviour {

	public bool isFree = true;

	private int carsInTrigger = 0;

	void OnTriggerEnter() {
		this.isFree = false;
		this.carsInTrigger++;
	}

	void OnTriggerExit() {
		this.carsInTrigger--;

		if(this.carsInTrigger == 0) {
			this.isFree = true;
		}

		Debug.Log ("cars in trigger " + this.carsInTrigger);
	}
}
