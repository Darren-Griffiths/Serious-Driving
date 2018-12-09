using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mmcontroller : MonoBehaviour {

    public GameObject roadSigns;
	public GameObject controls;
    public GameObject difficulty;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Loads level 1 on click
    public void playbuttonClick()
    {
        SceneManager.LoadScene("level1");
    }

    // Activates Road Signs on click
    public void signsbuttonClick()
    {
        roadSigns.SetActive(true);
    }

    // De-Activates Road Signs on click
    public void hidesignsbuttonClick()
    {
        roadSigns.SetActive(false);
    }

	public void showControls()
	{
		controls.SetActive (true);
	}

	public void hideControls()
	{
		controls.SetActive (false);
	}

    public void showDifficulty() {
        difficulty.SetActive(true);
    }

    public void hideDifficulty() {
        difficulty.SetActive(false);
    }

    // Exit Application on click
    public void exitbuttonClick()
    {
        Application.Quit();
    }
}
