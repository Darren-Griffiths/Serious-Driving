using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

	public GameObject Player;
	public GameObject StartPoint;

	public GameObject MissionDescription;

	public List<GameObject> missionTips = new List<GameObject> ();
	public List<GameObject> missionLocations = new List<GameObject> ();

	public int missionState = 0;


	//states
	//0 not started
	//1 at the start position
	//2 after moving for the first time display location where to drive to 
	//3 player approached the roundabout show necessery info
	//4 player started moving after roundabout (or 5s wait) show location where to drive around the roundabout
	//5 show exit when distance is within a range
	//6show finish location



	//for the mission we need the start point
	//description of whats going on
	//Approach the mission objective (roundabout)
	//perform checks
	//when safe move onto the roundabout
	//proceed the roundabout
	//exit at the required exit
	//drive to finish



	// Use this for initialization
	void Start () {
		
	}

	public void StartMission() {
		Player.transform.position = StartPoint.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (Input.GetKey (KeyCode.I)) {
			DisplayMissionInfo ();
		}

		checkMissionStatus ();
	}

	void checkMissionStatus() {
		//status 1 
		//mission just started
		//show mission info
		if (missionState == 1) {
			//show first tip
			missionTips[0].SetActive(true);

			//check when the player startts moiving
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				missionState = 2;
			}
		}
			
		//status 2
		if (missionState == 2) {
			missionTips [0].SetActive (false);
			missionTips [1].SetActive (true);

			//show the location
			missionLocations[0].SetActive(true);

		}

		if (missionState == 3) {

		}



		//status 3


		// status 4

		//status 5

		// status 6
	}

	void DisplayMissionInfo() {
		MissionDescription.SetActive (!MissionDescription.active);
	}
}
