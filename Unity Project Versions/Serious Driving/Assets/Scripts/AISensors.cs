using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensors : MonoBehaviour {
	public carAI AI;
	public Vector3 frontLineLoc = new Vector3(-3f, 2f, 0f);
	public float sideLinePos = 4f;
	public float frontSensorAngle = 30f;

	public float sensorLength = 5f;
	public float avoidMultiInt = 0.2f;
	public bool waitForGiveWay = false;

	void Start() {
		AI = transform.parent.GetComponent<carAI>();
	}

	void Update() {
		CheckSensors ();
	}

	public void CheckSensors() {
		RaycastHit hit;
		Vector3 sensorStartPos = transform.up;//position + frontLineLoc;

		//sensorStartPos += transform.forward * frontLineLoc.z;
		//sensorStartPos += transform.up * frontLineLoc.y;

		float avoidMultiplier = 0;
		AI.isAvoiding = false;



		Debug.Log ("sensor pos" + sensorStartPos);

		for (int i = 0; i < 3; i++) {
	

			//sensorStartPos += transfrom.localPosition //new Vector3 (1f, 0, 0);

			//sensorStartPos.localPosition.x += 1;
			Debug.DrawRay(sensorStartPos, transform.forward * sensorLength, Color.red, 0.1f);

			if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
				if (!hit.collider.CompareTag ("Terrain")) {

					Debug.Log (hit.collider.gameObject);
					GameObject something = hit.collider.gameObject;

					//carAI ai = something.GetComponent<carAI> ();

					Debug.Log("something is in front better brake");



					//if (ai.isBreaking) {
					//	this.isBreaking = true;
					//}
					//


					AI.isAvoiding = true;

					if (hit.normal.x < 0) {
						avoidMultiplier = -(2*avoidMultiInt);
					} else {
						avoidMultiplier = (2 * avoidMultiInt);
					}

				}
			}

		}




		//front right edge sensor
		//sensorStartPos += transform.right * sideLinePos;


		//Debug.DrawRay(sensorStartPos, transform.forward * sensorLength, Color.red, 0.5f);

		Debug.DrawRay(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward * sensorLength, Color.red, 0.5f);
		/*
		//Debug.DrawRay(sensorStartPos, transform.forward * sensorLength, Color.green);
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				//Debug.DrawLine (sensorStartPos, hit.point);

				Debug.Log ("HITTING " + hit.collider.gameObject + " AVOIDING");

				isAvoiding = true;
				avoidMultiplier -= (2 * avoidMultiInt);
			}
		}



		//front right angle sensor
		else */
			if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
				if (!hit.collider.CompareTag ("Terrain")) {
					Debug.DrawLine (sensorStartPos, hit.point);
					Debug.Log ("HITTING " + hit.collider.gameObject + " AVOIDING");
					AI.isAvoiding = true;
					avoidMultiplier -= avoidMultiInt;
				}
			}

		/*

		//front left edge sensor
		sensorStartPos -= transform.right * sideLinePos * 2;

		Debug.DrawRay(sensorStartPos, transform.forward * sensorLength, Color.red, 0.5f);

		Debug.DrawRay(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward * sensorLength, Color.red, 0.5f);

		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				//Debug.DrawLine (sensorStartPos, hit.point);
				//Debug.Log ("HITTING " + hit.collider.tag + " AVOIDING");
				isAvoiding = true;
				avoidMultiplier += (2 * avoidMultiInt);
			}
		}

		//front left angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				//Debug.DrawLine (sensorStartPos, hit.point);
				Debug.Log ("HITTING " + hit.collider.gameObject + " AVOIDING");
				isAvoiding = true;
				avoidMultiplier += avoidMultiInt;
			}
		}

		//front center sensor
		//if(avoidMultiplier == 0) {




		//}
		*/

		if (AI.isAvoiding) {
			//AI.targetSteerAngle = AI.maxSteerAngle * avoidMultiplier;
		}

	}
}
