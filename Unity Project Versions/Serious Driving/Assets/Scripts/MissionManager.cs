using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

	public GameObject roundaboutScript;
	public GameObject roundaboutUI;

	public GameObject tJunScript;
	public GameObject tJunUI;

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("missionID", 1);

		if (PlayerPrefs.GetInt("missionID") == 1) {
			roundaboutScript.SetActive (true);
			roundaboutUI.SetActive (true);
		} else if(PlayerPrefs.GetInt("missionID") == 2) {
			tJunScript.SetActive (true);
			tJunUI.SetActive (true);
		}

	}

	void Update() {
		if (PlayerPrefs.GetInt ("missionID") == 1) {
			roundaboutScript.SetActive (true);
			roundaboutUI.SetActive (true);
		} else if (PlayerPrefs.GetInt ("missionID") == 2) {
			tJunScript.SetActive (true);
			tJunUI.SetActive (true);
		} else {
			tJunScript.SetActive (false);
			tJunUI.SetActive (false);
			roundaboutScript.SetActive (false);
			roundaboutUI.SetActive (false);
		}
	}
	

}
