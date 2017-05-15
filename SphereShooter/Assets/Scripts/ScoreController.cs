using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour {
	public Text scoreText;
	public static int score;

	void Start () {
		score = 0;
		scoreText.text = "0000000";
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString().PadRight(7,'0');
	}

}