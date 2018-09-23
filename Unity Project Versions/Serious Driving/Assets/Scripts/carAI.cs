using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour {

	public Transform currentPath;
	private List<Transform> nodes;
	private int currentPathNode = 0;
	private bool isAvoiding = false;
	private float targetSteerAngle = 0;

	public Vector3 frontLineLoc = new Vector3(0f, 2f, 0.5f);
	public float sideLinePos = 4f;
	public float frontSensorAngle = 30f;


	[Header("Car Setup")]
	public float maxSteerAngle = 40f;
	public float turnSpeed = 5f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public Vector3 COM;

	[Header("AI properties")]
	public float distCheckLimit = 2f;
	public float maxMotorTorque = 200f;
	public float currentSpeed;
	public float maxSpeed = 100f;
	public float maxBrakeTorque = 150f;

	[Header("Cur AI Behaviour")]
	public bool isBreaking = false;
	public MeshRenderer brakeLights;

	[Header("Sensors")]
	public float sensorLength = 5f;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().centerOfMass = COM;
		loadNewPath (currentPath);
	}

	public void switchPath(Transform path) {
		
		loadNewPath(path);

		currentPathNode = 0;
	}

	//sets up a full path for the car
	void loadNewPath(Transform travelPath) {
		//get the current path
		Transform[] pathTransforms = travelPath.GetComponentsInChildren<Transform> ();
		//clear nodes list
		nodes = new List<Transform>();

		//loop over all the nodes and story locally in the object
		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != currentPath.transform) {
				nodes.Add (pathTransforms [i]);
			}
		}
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		CheckSensors ();

		ApplySteering ();
		ApplyPower ();
	
		CheckPathNodeDistance ();

		Debug.Log (currentPathNode);


		Braking ();

		LerpToSteerAngle ();
	}

	private void CheckSensors() {
		RaycastHit hit;
		Vector3 sensorStartPos = transform.position + frontLineLoc;

		sensorStartPos += transform.forward * frontLineLoc.z;
		sensorStartPos += transform.up * frontLineLoc.y;

		float avoidMultiplier = 0;
		isAvoiding = false;

		//front right edge sensor
		sensorStartPos += transform.right * sideLinePos;
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				Debug.DrawLine (sensorStartPos, hit.point);
				isAvoiding = true;
				avoidMultiplier -= 1f;
			}
		}

		//front right angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				Debug.DrawLine (sensorStartPos, hit.point);
				isAvoiding = true;
				avoidMultiplier -= 0.5f;
			}
		}

		//front left edge sensor
		sensorStartPos -= transform.right * sideLinePos * 2;
		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				Debug.DrawLine (sensorStartPos, hit.point);
				isAvoiding = true;
				avoidMultiplier += 1f;
			}
		}

		//front left angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				Debug.DrawLine (sensorStartPos, hit.point);
				isAvoiding = true;
				avoidMultiplier += 0.5f;
			}
		}

		//front center sensor
		if(avoidMultiplier == 0) {
		
			if (Physics.Raycast (sensorStartPos, transform.forward * sensorLength, out hit, sensorLength)) {
				if (!hit.collider.CompareTag ("Terrain")) {
					Debug.DrawLine (sensorStartPos, hit.point);
					isAvoiding = true;

					if (hit.normal.x < 0) {
						avoidMultiplier = -1;
					} else {
						avoidMultiplier = 1;
					}

				}
			}

		}


		if (isAvoiding) {
			targetSteerAngle = maxSteerAngle * avoidMultiplier;
		}

	}


	private void ApplyPower() {

		currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

		if (currentSpeed < maxSpeed && !isBreaking) {
			wheelFL.motorTorque = maxMotorTorque;
			wheelFR.motorTorque = maxMotorTorque;
		} else {
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
		}
	}

	private void ApplySteering() {

		if (isAvoiding) {
			return;
		}


		Vector3 relativeVector = transform.InverseTransformPoint (nodes [currentPathNode].position);
		//relativeVector = relativeVector / relativeVector.magnitude;

		float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
	
		targetSteerAngle = newSteer;
	}

	private void CheckPathNodeDistance() {
		Debug.Log ("distance " + Vector3.Distance (transform.position, nodes [currentPathNode].position));

		if (Vector3.Distance (transform.position, nodes [currentPathNode].position) < distCheckLimit) {
			if (currentPathNode == nodes.Count - 1) {
				currentPathNode = 0;

				//Debug.Log ("setting to 0");
			} else {
				currentPathNode++;
				//Debug.Log ("setting to new one");
			}

		}
	}

	private void Braking() {

		Debug.Log (isBreaking);

		if (isBreaking) {
			brakeLights.enabled = true;
			wheelRR.brakeTorque = maxBrakeTorque;
			wheelRL.brakeTorque = maxBrakeTorque;
		} else {
			brakeLights.enabled = false;

			wheelRR.brakeTorque = 0;
			wheelRL.brakeTorque = 0;
		}
	}

	private void LerpToSteerAngle() {
		wheelFL.steerAngle = Mathf.Lerp (wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
		wheelFR.steerAngle = Mathf.Lerp (wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
	}


}
