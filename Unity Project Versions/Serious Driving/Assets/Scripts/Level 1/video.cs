using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video : MonoBehaviour {

    public GameObject video1;
    public VideoClip video2;
    public VideoPlayer video3;

	// Use this for initialization
	void Start () {

        video3.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
