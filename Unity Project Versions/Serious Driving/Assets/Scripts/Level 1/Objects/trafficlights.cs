using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficlights : MonoBehaviour {

    public GameObject redLight;
    public GameObject amberLight;
    public GameObject greenLight;

    Light rlLight;
    Light alLight;
    Light glLight;

    public float lightTimer;

    public float currentLight;

	// Use this for initialization
	void Start ()
    {
        rlLight = redLight.GetComponentInChildren<Light>();
        alLight = amberLight.GetComponentInChildren<Light>();
        glLight = greenLight.GetComponentInChildren<Light>();
        rlLight.enabled = true;
        currentLight = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {

        {
            lightTimer -= Time.deltaTime;
            
            if (lightTimer < 0)
            {
                //debug.log("over");
                //nextLight();
                //debug.log(currentlight);
            }

            //if (currentlight == 1)
            //{
            //    rllight.enabled = true;
            //    allight.enabled = false;
            //    gllight.enabled = false;
            //}
            //if (currentlight == 2)
            //{
            //    rllight.enabled = false;
            //    allight.enabled = true;
            //    // start a new timer
            //    gllight.enabled = false;
            //}
            //if (currentlight == 3)
            //{
            //    rllight.enabled = false;
            //    allight.enabled = false;
            //    // start a new timer
            //    gllight.enabled = true;
            //}
        }

    }

    //public void resetlightTimer()
    //{
    //    //lightTimer = 10;
    //}

    //public void nextLight()
    //{
        //debug.log("working");
    //    if (currentlight == 1)
    //    {
    //        currentlight = 2;
    //    }
    //    else if (currentlight == 2)
    //    {
    //        currentlight = 3;
    //    }
    //    else if (currentlight == 3)
    //    {
    //        currentlight = 1;
    //    }
    //}

}
