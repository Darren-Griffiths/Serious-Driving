using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r_check : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter()
    {
        if (gameObject.tag == "checkpoint")
            Debug.Log("Check other checker on roundabout!");
    }
}
