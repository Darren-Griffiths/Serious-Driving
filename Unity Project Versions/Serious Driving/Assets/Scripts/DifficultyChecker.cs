using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int diff = PlayerPrefs.GetInt("difficulty");
		
		//if its hard
		if(diff == 1) {
			gameObject.SetActive(true);
		} else {
			gameObject.SetActive(false);
		}

	}
}
