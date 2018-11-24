using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSelector : MonoBehaviour {

	public int maxScenarios = 3;
	public int currentScenario = 1;
	public Text scenarioNumber;

	public void nextScenario() {
		currentScenario++;

		if (currentScenario > maxScenarios) {
			currentScenario = 1;
		}

		PlayerPrefs.SetInt ("missionID", currentScenario);

		displayNumber ();

	}

	public void prevScenario() {
		currentScenario--;



		if (currentScenario < 1) {
			currentScenario = maxScenarios;
		}

		PlayerPrefs.SetInt ("missionID", currentScenario);

		displayNumber ();
	}

	void displayNumber() {
		scenarioNumber.text = currentScenario + " of " + maxScenarios;
	}
}
