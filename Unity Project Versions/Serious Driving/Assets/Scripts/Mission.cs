using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mission : MonoBehaviour {

	public GameObject Player;
	public GameObject StartPoint;

	public GameObject missionMarker;

	public GameObject MissionDescription;
	public GameObject FinishDescription;

	public List<GameObject> missionTips = new List<GameObject> ();
	public List<GameObject> missionLocations = new List<GameObject> ();

	public int finishingMissionState;

	public int missionState = 0;

	private List<GameObject> missionCurrentEnabledLocations = new List<GameObject> ();
	private bool isMissionActive = false;

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
		missionState = 1;
		isMissionActive = true;
	}

	//checks the distance to next location and if below 5 trifgger next mission status
	void checkDistanceToMissionMarker(GameObject marker) {

		if (marker == null) {
			return;
		}
			
		float dist = Vector3.Distance (marker.transform.position, Player.transform.position);

		//Debug.Log ("Mission distance: " + dist);

		if (dist < 5) {
			missionState++;
		}

	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (Input.GetKey (KeyCode.I)) {
			DisplayMissionInfo ();
		}


		checkMissionFinish ();
		checkMissionStatus ();
	}

	void checkMissionFinish() {

		Debug.Log ("Current status" + missionState + " End state: " + finishingMissionState);

		if (missionState == finishingMissionState) {
			//show finished tip

			isMissionActive = false;

			FinishDescription.SetActive (true);
		}
	}

	//takes an index to enable to marker, markers are tied into states
	//state 2 mission marker 0
	//state 3 mission marker 1
	//state 4 mission marker 2
	//etc.. each mission marker(missionLocations) is 1 per state
	void enableMissionMarker(int markerIndex) {
		//check iff there is a mission marker
		if (missionLocations.ElementAtOrDefault(markerIndex) == null) {
			return;
		}


		GameObject missionLoc = Instantiate (missionMarker, missionLocations[markerIndex].transform.position, Quaternion.identity);
		missionLoc.SetActive (true);

		missionCurrentEnabledLocations.Add (missionLoc);
	}

	//clears any mission markers that are in the world to avoid confusion
	void ClearMissionMarkers() {
		foreach(GameObject loc in missionCurrentEnabledLocations) {
			if (loc != null) {
				loc.SetActive (false);
			}
		}

		missionCurrentEnabledLocations.Clear ();

	}

	void checkMissionStatus() {

		if (!isMissionActive) {
			return;
		}

		//status 1 
		//mission just started
		//show mission info
		if (missionState == 1) {
			//show first tip
			//missionTips[0].SetActive(true);


			//check when the player startts moiving
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				missionState = 2;
			}
		}
			
		//status 2
		if (missionState == 2) {
			//show mission tips
			//missionTips [0].SetActive (true);

			enableNextMissionTip (0);

			//turn off main mission info
			MissionDescription.SetActive (false);

			enableMissionMarker(0);

			//check the distance towards the next location and adds to the mission status
			checkDistanceToMissionMarker (missionLocations [0]);

		}

		//status 3
		if (missionState == 3) {
			//missionTips [0].SetActive (false);
			//missionTips [1].SetActive (true);

			enableNextMissionTip (1);

			//clear the marker list
			ClearMissionMarkers();

			enableMissionMarker (1);

			checkDistanceToMissionMarker (missionLocations [1]);
		}

		if (missionState == 4) {
			//missionTips [1].SetActive (false);
			//missionTips [2].SetActive (true);

			enableNextMissionTip (2);

			ClearMissionMarkers ();

			enableMissionMarker (2);

			checkDistanceToMissionMarker (missionLocations [2]);
		}

		if (missionState == 5) {

			//missionTips [2].SetActive (false);
			//missionTips [3].SetActive (true);
			enableNextMissionTip (3);


			ClearMissionMarkers ();
			enableMissionMarker (3);

			checkDistanceToMissionMarker (missionLocations [3]);
		}

		if (missionState == 6) {
			enableNextMissionTip (4);

			ClearMissionMarkers ();
			enableMissionMarker (4);

			checkDistanceToMissionMarker (missionLocations [4]);
		}

	}

	void enableNextMissionTip(int missionTipIndex) {

		if (missionTips.ElementAtOrDefault(missionTipIndex - 1) != null) {
			//disable previous
			missionTips[missionTipIndex - 1].SetActive(false);
		}


		if (missionTips.ElementAtOrDefault(missionTipIndex) != null) {
			//enable current
			missionTips[missionTipIndex].SetActive(true);
		}

	}

	void DisplayMissionInfo() {
		MissionDescription.SetActive (!MissionDescription.active);
	}
}
