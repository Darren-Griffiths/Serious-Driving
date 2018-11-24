using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class lvl1controller : MonoBehaviour {

	public GameObject pauseMenu;
    public GameObject scenarios;
	public GameObject scenarioSelector;
    //public GameObject pauseMenu;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Checks for ESC press
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If ESC is pressed, pause function is called.
            pauseGame();
        }
	}


    //Pause Game Function
    public void pauseGame()
    {
        //Checks if the game is paused, if so resumes.
        if (Time.timeScale == 0)
        {
            //Resume game
            Time.timeScale = 1;
            //Hides pause menu
            pauseMenu.SetActive(false);
            //Set Cursor to not be visible
            Cursor.visible = false;
        }

        //Checks if the game is playing, if so pauses.
        else if (Time.timeScale == 1)
        {
            //Freezes game
            Time.timeScale = 0;
            //Shows pause menu
            pauseMenu.SetActive(true);
            //Set Cursor to be visible
            Cursor.visible = true;
        }
    }

    //Opens scenarios
    public void openScenarios()
    {
        scenarios.SetActive(true);
		scenarioSelector.SetActive (true);
    }

    //Returns to main menu
    public void mainmenuOnClick()
    {
        SceneManager.LoadScene("mainmenu");
    }

    //Exits game
    public void exitgameOnClick()
    {
        Application.Quit();
    }
}
