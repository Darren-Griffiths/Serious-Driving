using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carAI : MonoBehaviour {

	public Transform currentPath;
	public List<Transform> nodes;
	public int currentPathNode = 0;
	public bool isAvoiding = false;
	private float targetSteerAngle = 0;

	public Transform rightSensorPos;
	public Transform leftSensorPos;
	public List<Transform> forntSensors = new List<Transform> ();

	public Vector3 frontLineLoc = new Vector3(-3f, 2f, 0f);
	public float sideLinePos = 4f;
	public float frontSensorAngle = 30f;

	public string pathDestinationID = "";

	public bool brakingForCar = false;
	public float driverThrottleInput = 1;

	[Header("Car Setup")]
	public float maxSteerAngle = 40f;
	public float turnSpeed = 5f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public Vector3 COM;

	[Header("AI properties")]
	public float nodeDistCheckLimit = 2f;
	public float maxMotorTorque = 200f;
	public float currentSpeed;
	public float maxSpeed = 100f;
	public float maxBrakeTorque = 150f;
	public LayerMask rayCastLayerMask;

	[Header("Cur AI Behaviour")]
	public bool isBreaking = false;
	public MeshRenderer brakeLights;

	[Header("Sensors")]
	public float sensorLength = 5f;
	private float avoidMultiInt = 0.3f;
	public bool waitForGiveWay = false;

	// Use this for initialization
	void Start () {
		//frontLineLoc = sensorPos.position;//new Vector3(-3f, 2f, 0f);

		GetComponent<Rigidbody> ().centerOfMass = COM;
		//spawn car and find the nears node on the path
		currentPathNode = findNearestNode(currentPath);

		loadNewPath (currentPath);
	}

	/*switchPath Transform - next path for the AI to switch to
	 * 
	 * this function will attempt to find the closest available node of the path and use it as its current node
	 * 
	 * */
	public void switchPath(Transform path) {

		currentPathNode = findNearestNode(path);

		loadNewPath(path);
	}
		
	/*switchPath 
	 *  - path Transform - next path for the AI to switch to
	 *  - node Integer - the number of the node to set as current
	 * 
	 * this switches to the specified path and sets the current node by user specification
	 * 
	 * */
	public void switchPath(Transform path, int node) {
		loadNewPath (path);
		currentPathNode = node;
	}

	//Attempts to find the closest node based on current position
	int findNearestNode(Transform path) {

		Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();

		Vector3 currentPos = transform.position;
		float minDistance = Mathf.Infinity;
		int nodeKey = 0;

		//loop over the new path
		for (int i = 0; i < pathTransforms.Length - 1; i++) {
			//get the distance
			float distance = Vector3.Distance (pathTransforms [i].position, currentPos);
			//Debug.Log ("Distance to node" + distance);
			// check if this distance is closer than what we found earlier 
			if (distance < minDistance) {
			//	Debug.Log ("found the shortest");

				//if yes set the closest node to the index of the path Array
				nodeKey = i;
				//default the closest distance to current distance
				minDistance = distance;
			}

		}

		//Debug.Log ("Noide key " + nodeKey + " distance: " + minDistance);

		return nodeKey;
	}

	//sets up a full path for the car
	void loadNewPath(Transform travelPath) {
		currentPath = travelPath;
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

		Braking ();

		LerpToSteerAngle ();
	}

	public void CheckSensors() {
		RaycastHit hit;
		Vector3 sensorStartPos;

		float avoidMultiplier = 0;
		isAvoiding = false;

		bool breakingNoAvoid = false;
		driverThrottleInput = 1f;

		foreach (Transform pos in forntSensors) {

			Debug.DrawRay(pos.position, transform.forward * sensorLength, Color.red, 0.1f);

			if (Physics.Raycast (pos.position, transform.forward * sensorLength, out hit, sensorLength, -5, QueryTriggerInteraction.Ignore)) {
				if (!hit.collider.CompareTag ("Terrain")) {
					//get the root of the object we hit and attempt to get the AI script
					GameObject something = hit.collider.transform.root.gameObject;
					carAI ai = something.GetComponent<carAI> ();

					//if the car in front is AI and is braking
					//set throttle to 0 and apply brakes
					if (ai != null && ai.isBreaking) {
						this.isBreaking = true;

						driverThrottleInput = 0f;

						return;
					} else {

						//once car in front is a distance away, full throttle baby
						if (this.isBreaking && hit.distance > 3) {
							driverThrottleInput = 1f;
							StartCoroutine(moveAfterBrake());
						}

						if (hit.distance < 12) {
							driverThrottleInput = 0.5f;
						}


						//if we no braking but the car in front is too close, go half throttle
						if (hit.distance < 9) {
							driverThrottleInput = 0.2f;

							//if the car in front is getting too close, hit the brakes 
							if (hit.distance < 5) {
								driverThrottleInput = 0f;
								this.isBreaking = true;
							}

						} else {
							driverThrottleInput = 1f;
						}

					}
				}
			}
		}

		if (this.isBreaking) {
			return;
		}

		//front right edge sensor
		sensorStartPos = rightSensorPos.position;
		float sideSensorLength = sensorLength * 0.5f;

		Debug.DrawRay(sensorStartPos, transform.forward * sideSensorLength, Color.green, 0.5f);

		Debug.DrawRay(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward * sideSensorLength, Color.green, 0.5f);

		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sideSensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				isAvoiding = true;
				avoidMultiplier -= (0.5f * avoidMultiInt);
				driverThrottleInput = 0.4f;
			}
		}
			
		//front right angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sideSensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				isAvoiding = true;
				avoidMultiplier -= avoidMultiInt;
				driverThrottleInput = 0.4f;
			}
		}



		//front left edge sensor
		sensorStartPos = leftSensorPos.position;

		Debug.DrawRay(sensorStartPos, transform.forward * sideSensorLength, Color.red, 0.5f);

		Debug.DrawRay(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward * sideSensorLength, Color.red, 0.5f);

		if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sideSensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				isAvoiding = true;
				avoidMultiplier += (0.5f * avoidMultiInt);
				driverThrottleInput = 0.4f;
			}
		}

		//front left angle sensor
		else if (Physics.Raycast (sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sideSensorLength)) {
			if (!hit.collider.CompareTag ("Terrain")) {
				isAvoiding = true;
				avoidMultiplier += avoidMultiInt;
				driverThrottleInput = 0.4f;
			}
		}

		if (isAvoiding) {
			targetSteerAngle = maxSteerAngle * avoidMultiplier;
		}

	}

	IEnumerator moveAfterBrake() {
		yield return new WaitForSeconds (4);
		brakingForCar = false;
		isBreaking = false;
		driverThrottleInput = 5f;

		StopCoroutine (moveAfterBrake());
	}


	private void ApplyPower() {
		currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

		//Debug.Log ("THROTTLE POSIS " + driverThrottleInput);

		//get correct power for situation
		float enginePower = maxMotorTorque * driverThrottleInput;

		if (currentSpeed < maxSpeed && !isBreaking) {
			wheelFL.motorTorque = enginePower;
			wheelFR.motorTorque = enginePower;
		} else {
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
		}
	}

	private void ApplySteering() {

		if (isAvoiding) {
		//	Debug.Log ("is avoiding no steering");
			return;
		}

		//Debug.Log ("CURRENT NODE CHECK FOR DISTANCE " + currentPathNode);

		//Debug.Log ("heading to " + nodes [currentPathNode]);

		Vector3 relativeVector = transform.InverseTransformPoint (nodes [currentPathNode].position);
		//relativeVector = relativeVector / relativeVector.magnitude;

		float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
	
		targetSteerAngle = newSteer;
	}

	private void CheckPathNodeDistance() {
		//Debug.Log ("distance " + Vector3.Distance (transform.position, nodes [currentPathNode].position));

		if (Vector3.Distance (transform.position, nodes [currentPathNode].position) < nodeDistCheckLimit) {
			if (currentPathNode == nodes.Count - 1) {
				currentPathNode = 0;

				//Debug.Log ("setting to 0");
			} else {
				currentPathNode++;
				//Debug.Log ("setting to new one");
			}

		}

		//Debug.Log("Current Path Node is : " + currentPathNode);
	}



	private void Braking() {

		//Debug.Log (isBreaking);
		/*
		if (brakingForCar) {
			brakeLights.enabled = true;
			wheelRR.brakeTorque = maxBrakeTorque;
			wheelRL.brakeTorque = maxBrakeTorque;
		} else {
			brakeLights.enabled = false;

			wheelRR.brakeTorque = 0;
			wheelRL.brakeTorque = 0;
		}
*/

		if (isBreaking || waitForGiveWay || brakingForCar) {
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
