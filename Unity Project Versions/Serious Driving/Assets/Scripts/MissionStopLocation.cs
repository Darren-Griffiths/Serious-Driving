using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MissionStopLocation : MonoBehaviour {

	public bool playerStopped = false;

	void OnTriggerStay(Collider col) {
		if (col.tag == "Player") {
			CarController car = col.transform.root.GetComponent<CarController> ();

			//check if the car is stopped or traveling below certain speed
			if (car.CurrentSpeed < 5) {
				playerStopped = true;
			}
		}
	}
}
