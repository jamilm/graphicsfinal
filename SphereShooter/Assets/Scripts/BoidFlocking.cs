using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidFlocking : MonoBehaviour
{
	private GameObject Controller;
	private List<GameObject> allBoids;

	private bool inited = false;
	private float minVelocity;
	private float maxVelocity;
	private float randomness;
	private GameObject chasee;
	private Rigidbody rigidbody; 
	public int health;
	public Rigidbody earth; 
	public float radius; 
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody> ();
		health = 5;
//		StartCoroutine ("BoidSteering");
	}
		
	private Vector3 Calc ()
	{
		Vector3 randomize = new Vector3 ((Random.value *2) -1, (Random.value * 2) -1, (Random.value * 2) -1);

		randomize.Normalize();

		// get flock stats
		BoidController boidController = Controller.GetComponent<BoidController>();
		Vector3 flockCenter = boidController.flockCenter;
		Vector3 flockVelocity = boidController.flockVelocity;
		Vector3 follow = chasee.transform.position;

		// cohesion/separation
		flockCenter = flockCenter - transform.position;
		float flockScale = 1.0f / (flockCenter.magnitude * flockCenter.magnitude);
		flockCenter.Normalize ();

		// alignment
		flockVelocity = flockVelocity - rigidbody.velocity;
		flockVelocity.Normalize ();

		// follow
		follow = follow - transform.position;
		follow.Normalize ();

		// separation
//		separation = separation * -1f;
//		Vector3 separation = new Vector3(0,0,0);
//		GameObject[] boids = boidController.boids;
//		for (int i = 0; i < boids.Length; i++) {
//			float distance = Vector3.Distance (transform.position, boids[i].transform.position);
//			Debug.Log (distance);
//			if (distance < 1f) { 
//				separation -= boidController.boids [i].transform.position;
//			}
//		}
		return (flockCenter + flockVelocity + follow * 10.0f  + randomize * randomness);
	}

	public void SetController (GameObject theController)
	{
		Controller = theController;
		BoidController boidController = Controller.GetComponent<BoidController>();
		minVelocity = boidController.minVelocity;
		maxVelocity = boidController.maxVelocity;
		randomness = boidController.randomness;
		chasee = boidController.chasee;
		inited = true;

		allBoids = boidController.boids;
	}

	public void Update() {
		if (inited) {

			Vector3 normal = (transform.position - earth.position).normalized;

			Vector3 velocity = rigidbody.velocity + Calc () * Time.deltaTime;
			rigidbody.velocity = Vector3.ProjectOnPlane (velocity, normal);

//			Debug.DrawRay (rigidbody.position, rigidbody.velocity * 2.0f);

			// enforce minimum and maximum speeds for the boids
			float speed = rigidbody.velocity.magnitude;
			if (speed > maxVelocity) {
				rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
			} else if (speed < minVelocity) {
				rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
			}

			Vector3 dist = rigidbody.position - earth.position;
			if (dist.magnitude > .5f) {
				dist.Normalize ();
				dist *= 0.5f;
				rigidbody.position = dist;
			}
		}


	}
	void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "bullet")
        {
            Destroy(col.gameObject);
            if (this.health-- <= 0) {
            	Destroy(this.gameObject);
				allBoids.Remove (this.gameObject);
			}
        }
    }
}