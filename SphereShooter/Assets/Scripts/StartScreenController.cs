using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {


	public Text startText;
	public Image wasd;
	public Image space;
	public Image mouse; 

	private bool started = false; 
	// Use this for initialization
	void Start () {
		Enable ();

	}

	public void Enable() {

		startText.enabled = true;
		wasd.enabled = true; 
		space.enabled = true;
		mouse.enabled = true;

	}

	public void Disable() {
		startText.enabled = false;
		wasd.enabled = false; 
		space.enabled = false;
		mouse.enabled = false;
	}

	
	// Update is called once per frame
	void Update () {
		if (Time.time > 3f && !started) {
//			print ("Disabled");
			Disable ();
			started = true; 
		}
	}
}
