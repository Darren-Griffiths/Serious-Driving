using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSwitch : MonoBehaviour {

	public bool checkExitID;
	public bool randomiseExit;
	public int numberOfExits;

	public string thisExitIndexID;
	public string desiredExitID;
	public Transform nextPath;
	public int specificPathNode;
	private carAI AI;
	//private giveWayCheck giveWay;
	private List<giveWayCheck> giveWay = new List<giveWayCheck> ();


	public bool isGiveWay = false;
	public List<Collider> giveWayLocations = new List<Collider>();

	public bool isTrafficControlled = false;
	public CurrentLight trafficweLights;

	void Start() {
		if (isGiveWay) {

			foreach (Collider col in giveWayLocations) {
				giveWay.Add (col.GetComponent<giveWayCheck> ());
			}


			//giveWay = giveWayCheckLocation.GetComponent<giveWayCheck> ();
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Player") {
			return;
		}

		AI = col.transform.root.GetComponent<carAI> ();

		bool canMoveOff = true;

		if (isTrafficControlled) {
			if (trafficweLights.getCurrentLight() == 1 || trafficweLights.getCurrentLight() == 4) {
				canMoveOff = false;
			}
		}
			
		//loop over the give ways and see if they are free
		foreach (giveWayCheck check in giveWay) {
			//if not free just skip this iteration
			if (!check.isFree) {
				canMoveOff = false;
			}

		}

		if (canMoveOff) {
			AI.isBreaking = false;
			AI.waitForGiveWay = false;
		}
	}
	//apply braking to the cars behind the one that is breaking

	void OnTriggerEnter(Collider col) {
		//Debug.Log (col.gameObject.tag);
		if (col.gameObject.tag == "Player") {
			return;
		}

		AI = col.transform.root.GetComponent<carAI> ();

		bool applyBrakes = false;

		if (isTrafficControlled) {
			if (trafficweLights.getCurrentLight() == 1 || trafficweLights.getCurrentLight() == 4) {
				applyBrakes = true;
			}
		}

		//loop over all the give way checks and see if they arent free, apply brakes
		//this doesnt work right
		foreach (giveWayCheck check in giveWay) {
			//check give way and if something is there brake
			if ( check != null && !check.isFree) {
				//Debug.Log ("ahhh we breaking because of this right");

				AI.waitForGiveWay = true;
				AI.isBreaking = true;

				applyBrakes = true;
			}
		}

		if (applyBrakes) {
			AI.waitForGiveWay = true;
			AI.isBreaking = true;
		}
			
		//so if the current pathswitch we are at is not the one the car has saved as the destination
		//do nothin and exit
		if (checkExitID && thisExitIndexID != AI.pathDestinationID) {
		//	Debug.Log ("exiting the shit");
			return;
		}

		if (randomiseExit) {
			desiredExitID = "exit" + Random.Range (1, numberOfExits + 1);
		}

		//if the destination exit ID is not empty use this to set on AI 
		if (desiredExitID != "") {
			AI.pathDestinationID = desiredExitID;
		}

		//if the specified path node is empty call the fuction without it 
		//otherwise use the specifiedPathNode
		if (specificPathNode == -1) {
			AI.switchPath (nextPath);
		} else {
			AI.switchPath (nextPath, specificPathNode);
		}
	}
}
