using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {


	public static Image startScreen;

	public static bool started = false; 
	// Use this for initialization
	void Start () {
		Enable ();

	}

	public void Enable() {

		startScreen.enabled = true;

	}

	public void Disable() {
		startScreen.enabled = false;
	}

	
	// Update is called once per frame
	void Update () {
		if (Time.time > 5f && !started) {
//			print ("Disabled");
			Disable ();
			started = true; 
		}
	}
}
