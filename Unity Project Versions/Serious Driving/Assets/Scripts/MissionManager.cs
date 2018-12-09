using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

	public GameObject roundaboutScript;
	public GameObject roundaboutUI;

	public GameObject tJunScript;
	public GameObject tJunUI;

	public GameObject motorwayMissionUI;
	public GameObject motorwayMission;

	public int forceMissionID;

	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("missionID", forceMissionID);

		if (PlayerPrefs.GetInt ("missionID") == 1) {
			roundaboutScript.SetActive (true);
			roundaboutUI.SetActive (true);
		} else if (PlayerPrefs.GetInt ("missionID") == 2) {
			tJunScript.SetActive (true);
			tJunUI.SetActive (true);
		} else if(PlayerPrefs.GetInt("missionID") == 3) {
			motorwayMissionUI.SetActive (true);
			motorwayMission.SetActive(true);
		}

	}

	void Update() {

		if (PlayerPrefs.GetInt ("missionID") == 1) {
			roundaboutScript.SetActive (true);
			roundaboutUI.SetActive (true);
			tJunScript.SetActive (false);
			tJunUI.SetActive (false);
			motorwayMissionUI.SetActive (false);
			motorwayMission.SetActive(false);
		} else if (PlayerPrefs.GetInt ("missionID") == 2) {
			tJunScript.SetActive (true);
			tJunUI.SetActive (true);
			roundaboutScript.SetActive (false);
			roundaboutUI.SetActive (false);
			motorwayMissionUI.SetActive (false);
			motorwayMission.SetActive(false);
		} else if (PlayerPrefs.GetInt ("missionID") == 3) {
			motorwayMissionUI.SetActive (true);
			motorwayMission.SetActive(true);
			tJunScript.SetActive (false);
			tJunUI.SetActive (false);
			roundaboutScript.SetActive (false);
			roundaboutUI.SetActive (false);

		} else {
			tJunScript.SetActive (false);
			tJunUI.SetActive (false);
			roundaboutScript.SetActive (false);
			roundaboutUI.SetActive (false);
		}
	}

	public void loadMotorway() {
		SceneManager.LoadScene("motorway_level");

	}

	public void loadLevel1() {
		SceneManager.LoadScene("level1");
	}
	

}
