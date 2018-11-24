using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MissionStopLocation : MonoBehaviour {

	public bool playerStopped = false;
	public bool isTrafficLightCtrl = false;

	public trafficlights trafficLight;
	public bool wentOnRed = false;

	void OnTriggerStay(Collider col) {
		if (col.tag == "Player") {

			if (!isTrafficLightCtrl) {
				CarController car = col.transform.root.GetComponent<CarController> ();

				//check if the car is stopped or traveling below certain speed
				if (car.CurrentSpeed < 5) {
					playerStopped = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Player") {
			//check the lights state

			//check if this should check for traffic lights
			if (isTrafficLightCtrl) {
				//check if is the red light
				if (trafficLight.currentLight == 1) {
					wentOnRed = true;
				}
			}
		}
	}
}
