using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficlights : MonoBehaviour
{

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
        resetlightTimer();
    }
	
	// Update is called once per frame
	void Update ()
    {

        {
            lightTimer -= Time.deltaTime;
            
            if (lightTimer < 0)
            {
                //Debug.Log("over");
                nextLight();
                //Debug.Log(currentLight);
            }

            if (currentLight == 1)
            {
                rlLight.enabled = true;
                alLight.enabled = false;
                glLight.enabled = false;
            }
            if (currentLight == 2)
            {
                rlLight.enabled = true;
                alLight.enabled = true;
                // start a new timer
                glLight.enabled = false;
            }
            if (currentLight == 3)
            {
                rlLight.enabled = false;
                alLight.enabled = false;
                // start a new timer
                glLight.enabled = true;
            }
            if (currentLight == 4)
            {
                rlLight.enabled = false;
                alLight.enabled = true;
                // start a new timer
                glLight.enabled = false;
            }
        }

    }

    public void resetlightTimer()
    {
        lightTimer = 10;
    }

    public void nextLight()
    {
        if (currentLight == 1)
        {
            currentLight = 2;
            lightTimer = 3;
        }
        else if (currentLight == 2)
        {
            currentLight = 3;
            resetlightTimer();
        }
        else if (currentLight == 3)
        {
            currentLight = 4;
            lightTimer = 5;
        }
        else if (currentLight == 4)
        {
            currentLight = 1;
            resetlightTimer();
        }
    }

}
