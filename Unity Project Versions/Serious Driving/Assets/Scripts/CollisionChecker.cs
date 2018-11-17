using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

	public int carAIhitAmounts = 0;
	public int otherHits = 0;

	void Start() {
		Debug.Log ("COLLISION START");
	}

	void OnCollisionEnter (Collision obj) {
		if (obj.gameObject.tag == "CARAI") {
			carAIhitAmounts++;
			Debug.Log ("HIT AIII");
		} else {
			otherHits++;
		}

	}
}
