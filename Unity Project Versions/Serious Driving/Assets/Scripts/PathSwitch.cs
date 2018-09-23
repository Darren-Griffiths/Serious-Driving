using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSwitch : MonoBehaviour {

	public Transform nextPath;
	public carAI AI;


	void OnTriggerEnter(Collider col) {
		AI = col.transform.root.GetComponent<carAI> ();

		AI.switchPath (nextPath);
	}
}
