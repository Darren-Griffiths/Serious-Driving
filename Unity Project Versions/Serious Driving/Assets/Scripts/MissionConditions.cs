using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MissionConditions : MonoBehaviour {

	public GameObject Player;
	public int maxSpeed = 30;
	public float currentSpeed =0;
	public float playerMaxSpeed = 0;

	public List<MissionStopLocation> stopLocs = new List<MissionStopLocation> ();

	//public MissionStopLocation test;
	private CarController carCtrl;

	//stop locations
	void Start()
    {
		carCtrl = Player.GetComponent<CarController> ();
        playerMaxSpeed = 0;
}

	void checkSpeed() {
		currentSpeed = carCtrl.CurrentSpeed;

		if (currentSpeed > maxSpeed && currentSpeed > playerMaxSpeed) {
			playerMaxSpeed = currentSpeed;
		}
	}

	void LateUpdate() {
		checkSpeed ();
	}
}