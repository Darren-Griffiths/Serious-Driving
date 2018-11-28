using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    [RequireComponent (typeof(AudioSource))]

public class tjvideo : MonoBehaviour {
    public MovieTexture tjvid;
    private AudioSource tjaud;

	// Use this for initialization
	void Start () {

        GetComponent<RawImage>().texture = tjvid as MovieTexture;
        tjaud = GetComponent<AudioSource>();
        tjaud.clip = tjvid.audioClip;

		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space) && tjvid.isPlaying)
        {
            tjvid.Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !tjvid.isPlaying)
        {
            tjvid.Play();
        }
    }

    public void playVid()
    {
        tjvid.Stop();
        tjvid.Play();
        tjaud.Play();
    }
}
