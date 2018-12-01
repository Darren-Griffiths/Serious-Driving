using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class carSpeed : MonoBehaviour {

	public GameObject needle;
	public float currentSpeed;
	public float maxSpeed = 140;


	private int needleMin = 207;
	private int needleMax = 57;
	private CarController carCtrl;
	private Rigidbody ctest;

	// Use this for initialization
	void Start () {
		carCtrl = GetComponent<CarController> ();
		ctest = GetComponent<Rigidbody> ();

		needle.transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, 207));	
	}
	
	// Update is called once per frame
	void Update ()
    {
		currentSpeed = carCtrl.CurrentSpeed;
		needle.transform.localRotation = Quaternion.Euler(new Vector3 (0, 0, needleMin - (currentSpeed * 1.5f) ));	
    }
}
