using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficeLightInverse : CurrentLight {

	public Light rlLight;
	public Light alLight;
	public Light glLight;

	public bool isInverse;
	public trafficlights mainLight;

	//private int currentLight;

	//this script inverses the lioghts from the main light

	// Use this for initialization
	void Start ()
	{

	}

//	public int getCurrentLight() {
//		return this.currentLight;
//	}

	int InverseMainLight(int lightINT) {

		if (lightINT == 1) {
			return 3;
		} else if(lightINT == 2) {
			return 4;
		} else if (lightINT == 3) {
			return 1;
		} else {
			return 2;
		}
	}
		
	
	// Update is called once per frame
	void Update () {


		//read what the main light is doing and then inverse it
		this.currentLight = InverseMainLight(mainLight.currentLight);


		if (this.currentLight == 1)
		{
			rlLight.enabled = true;
			alLight.enabled = false;
			glLight.enabled = false;
		}
		if (this.currentLight == 2)
		{
			rlLight.enabled = true;
			alLight.enabled = true;
			// start a new timer
			glLight.enabled = false;
		}
		if (this.currentLight == 3)
		{
			rlLight.enabled = false;
			alLight.enabled = false;
			// start a new timer
			glLight.enabled = true;
		}
		if (this.currentLight == 4)
		{
			rlLight.enabled = false;
			alLight.enabled = true;
			// start a new timer
			glLight.enabled = false;
		}
	}
}
