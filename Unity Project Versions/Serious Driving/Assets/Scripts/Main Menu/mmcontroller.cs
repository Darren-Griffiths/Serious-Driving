using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mmcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Loads level 1 on click
    public void playbuttonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }
}
