using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCamera : MonoBehaviour {

	public GameObject camera;

	private Vector3 orgRotat;

	// Use this for initialization
	void Start () {
		orgRotat = camera.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 tempAngles = camera.transform.localEulerAngles;
		//orgRotat = camera.transform.eulerAngles;

		if (Input.GetKey (KeyCode.Q)) {
			
			tempAngles.y = orgRotat.y - 25f;


		} else if (Input.GetKey (KeyCode.E)) {
			tempAngles.y = orgRotat.y + 25f;
		} 
		else {
			tempAngles = orgRotat;
		}

		camera.transform.localRotation = Quaternion.Euler (tempAngles);


	}
}
