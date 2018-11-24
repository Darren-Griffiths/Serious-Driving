using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Mission : MonoBehaviour {

	public GameObject Player;
	public GameObject StartPoint;
	public Vector3 StartRotation;

	public GameObject missionMarker;

	public GameObject MissionDescription;
	public GameObject FinishDescription;

	public List<GameObject> missionTips = new List<GameObject> ();
	public List<GameObject> missionLocations = new List<GameObject> ();

	public int finishingMissionState;

	public int missionState = 0;

	private List<GameObject> missionCurrentEnabledLocations = new List<GameObject> ();
	private bool isMissionActive = false;

	public MissionConditions finishConditions;
	public GameObject finishConditionsUI;

	private CollisionChecker collCheck;

	private int missionPoints = 100;
	private bool isProcessingResults = false;

	private lvl1controller controller;

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
		collCheck = Player.GetComponent<CollisionChecker> ();
		isProcessingResults = false;
		controller = GameObject.Find ("Controller").GetComponent<lvl1controller> ();
	}

	public void StartMission() {
		Player.transform.position = StartPoint.transform.position;
		Player.transform.rotation = Quaternion.Euler(StartRotation);
		missionState = 1;
		isMissionActive = true;

		Player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Player.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;


		//hide the UI
		controller.pauseMenu.SetActive(false);
		controller.scenarioSelector.SetActive (false);






		Time.timeScale = 1;
		//missionState = 2;
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

	//check if the mission is finished
	void checkMissionFinish() {

		//Debug.Log ("Current status" + missionState + " End state: " + finishingMissionState);

		//look at the defined stop status int
		if (missionState == finishingMissionState) {
			//show finished tip

			isMissionActive = false;

			FinishDescription.SetActive (true);

			Invoke ("ProcessMissionConditions", 1f);

			//ProcessMissionConditions ();
		}
	}

	void addConditionText(string GONAME, string TEXT, int YPOS) {
		//clear all other texts with the same name
		foreach (Transform test in finishConditionsUI.transform) {
			if (test.gameObject.name == GONAME) {
				Destroy (test.gameObject);
			}
		}
			
		GameObject UIText = new GameObject (GONAME);
		UIText.transform.SetParent (finishConditionsUI.transform);

		RectTransform trans = UIText.AddComponent<RectTransform> ();
		trans.anchoredPosition = new Vector2 (0,YPOS);
		trans.sizeDelta = new Vector2(390, 30);

		Text speedText = UIText.AddComponent<Text>();
		speedText.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		speedText.text = TEXT;
		speedText.fontSize = 20;


	}

	//displays all the conditions set for the mission
	void ProcessMissionConditions() {

		if (isProcessingResults) {
			return;
		}

		isProcessingResults = true;

		//print speed
		addConditionText (
			"SPEED TEXT",
			"Speed limit: " + finishConditions.maxSpeed + " Your max speed: " + finishConditions.playerMaxSpeed,
			140
		);

		//check speed
		if (finishConditions.playerMaxSpeed > finishConditions.maxSpeed) {
			//take away points for this
			missionPoints -= 10;
		}

		foreach (MissionStopLocation test in finishConditions.stopLocs) {
			if (test.playerStopped) {
				addConditionText (
					"STOPTEXT",
					"You stopped in correct position",
					110
				);
			} else {
				missionPoints -= 10;

				addConditionText (
					"STOPTEXT",
					"You failed to stop in the desired location",
					110
				);
			}
		}

		//check hits
		if (collCheck.carAIhitAmounts > 0) {
			missionPoints -= 10;

			addConditionText (
				"CARAIHIT",
				"You collided with other cars " + collCheck.carAIhitAmounts + " times.",
				80
			);
		}

		if (collCheck.otherHits > 0) {
			missionPoints -= 10;

			addConditionText (
				"OTHERHIT",
				"You collided with things " + collCheck.carAIhitAmounts + " times.",
				50
			);
		}

		//check left side of the road

		//check indicators?

		switch (missionPoints) {
		case 100: 

			addConditionText (
				"MissionScore",
				"Well done, perfect score",
				-100
			);

			break;
		case 90:
			addConditionText (
				"MissionScore",
				"Good, almost a perfect score just need more practice",
				-100
			);

			break;

		case 80: 
			addConditionText (
				"MissionScore",
				"OK, More work required",
				-100
			);
			break;
		}

		Debug.Log ("MISSION POINTS" + missionPoints);

		if (missionPoints <= 70) {
			addConditionText (
				"MissionScore",
				"STOP, just buy a bike or get a bus pass",
				-100
			);
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

			ClearMissionMarkers();

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
