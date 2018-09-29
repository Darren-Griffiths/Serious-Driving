using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSwitch : MonoBehaviour {

	public bool checkExitID;
	public string thisExitIndexID;
	public string desiredExitID;
	public Transform nextPath;
	public int specificPathNode;
	private carAI AI;


	void OnTriggerEnter(Collider col) {
		AI = col.transform.root.GetComponent<carAI> ();

		Debug.Log ("This is the " + thisExitIndexID + " exit");

		//so if the current pathswitch we are at is not the one the car has saved as the destination
		//do nothin and exit
		if (checkExitID && thisExitIndexID != AI.pathDestinationID) {
			return;
		}

		//if the destination exit ID is not empty use this to set on AI 
		if (desiredExitID != "") {
			AI.pathDestinationID = desiredExitID;
		}

		Debug.Log ("Specified path node of the new path is : " + specificPathNode);



		//if the specified path node is empty call the fuction without it 
		//otherwise use the specifiedPathNode
		if (specificPathNode == 0) {
			Debug.Log ("switvching path to : " + nextPath + " finding the closest node");
			AI.switchPath (nextPath);
		} else {
			Debug.Log ("switching to path : " + nextPath + " using specified node: " + specificPathNode);
			AI.switchPath (nextPath, specificPathNode);
		}


	}

	// on roundabouts

	//ai approach
	//check for no cars
	//drive

	//before drive trigger exit node (can be randomised)

	//check each node on passing if its the desired exit node

	//once the exit node is hit (exit node refers to current path)

	//on the exit node, grab the next path (exit nodes always refer to a specified path no randoms)


}
