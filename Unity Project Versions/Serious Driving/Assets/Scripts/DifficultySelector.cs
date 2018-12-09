using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//set to easy
		PlayerPrefs.SetInt("difficulty", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHardDifficulty() {
		PlayerPrefs.SetInt("difficulty", 1);

	}

	public void SetEasyDifficulty() {

		PlayerPrefs.SetInt("difficulty", 0);
	}
}
