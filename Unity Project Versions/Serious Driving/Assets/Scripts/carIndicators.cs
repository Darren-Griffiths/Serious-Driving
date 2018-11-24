using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carIndicators : MonoBehaviour {

	public bool isIndLeft = false;
	public bool isIndRight = false;

	public GameObject leftInd;
	public GameObject rightInd;

	private Renderer left_Renderer;
	private Renderer right_Renderer;

	private float lightTimer = 0.5f;

	void Start() {
		left_Renderer = leftInd.GetComponent<Renderer> ();
		right_Renderer = rightInd.GetComponent<Renderer> ();
	
		left_Renderer.enabled = false;
		right_Renderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.LeftBracket)) {
			isIndLeft = !isIndLeft;
		}

		if (Input.GetKeyUp (KeyCode.RightBracket)) {
			isIndRight = !isIndRight;
		}


		if (isIndRight) {

			lightTimer -= Time.deltaTime;

			if (lightTimer < 0) {
				if (right_Renderer.enabled) {
					right_Renderer.enabled = false;
				} else {
					right_Renderer.enabled = true;
				}

				lightTimer = 0.5f;
			}

		} else {
			right_Renderer.enabled = false;
		}


		if (isIndLeft) {

			lightTimer -= Time.deltaTime;

			if (lightTimer < 0) {
				if (left_Renderer.enabled) {
					left_Renderer.enabled = false;
				} else {
					left_Renderer.enabled = true;
				}

				lightTimer = 0.5f;
			}

		} else {
			left_Renderer.enabled = false;
		}

	}



}