using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperBulletController : MonoBehaviour {

	private GameObject earth; 
	private BoidController controller; 
	// private SuperController bulletController; 
	public Slider bulletSlider; 
	int kills;	

	List<GameObject> boids; 
	// Use this for initialization
	void Start () {
		earth = GameObject.FindGameObjectsWithTag ("Earth") [0];
		controller = earth.GetComponent<BoidController> ();
		boids = controller.boids;

		// bulletBar = GameObject.FindGameObjectsWithTag ("BulletBar") [0];
		// bulletController = bulletBar.GetComponent<bulletController> ();
		// boids = controller.boids; 

	}
	
	// Update is called once per frame
	void Update () {

		foreach (GameObject boid in boids) {
			if (transform && boid) {
				float dist = Vector3.Distance (transform.position, boid.transform.position);

				if (dist < 1f) {
					boid.GetComponent<BoidFlocking> ().chasee = gameObject;
					boid.GetComponent<BoidFlocking> ().scale = 10f;
				}
			}
		}
		
	}
}
