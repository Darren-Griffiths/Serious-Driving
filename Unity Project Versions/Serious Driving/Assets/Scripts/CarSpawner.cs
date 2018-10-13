using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

	public GameObject car;//could be a list to have multiplee cars??
	public Transform initialPath;
	public Vector3 carRoatation;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnCar", 1f, 10f);
	}

	void SpawnCar() {
		var carAI = car.GetComponent<carAI> ();
		carAI.currentPath = initialPath;

		Instantiate (car, transform.position, Quaternion.Euler(carRoatation)); 
	}
}
