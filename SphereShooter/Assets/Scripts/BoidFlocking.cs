using UnityEngine;
using System.Collections;

public class BoidFlocking : MonoBehaviour
{
	private GameObject Controller;
	private bool inited = false;
	private float minVelocity;
	private float maxVelocity;
	private float randomness;
	private GameObject chasee;
	private Rigidbody rigidbody; 

	public Rigidbody earth; 
	public float radius; 
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody> ();
//		StartCoroutine ("BoidSteering");
	}

	IEnumerator BoidSteering ()
	{
		while (true)
		{
			if (inited)
			{
//				rigidbody.velocity = rigidbody.velocity + Calc () * Time.deltaTime;
				// velocity should be tangent to position on sphere
				// normal is from positoin to middle
				Vector3 normal = (earth.position - (transform.position)).normalized;

				Vector3 velocity = rigidbody.velocity + Calc () * Time.deltaTime;
				rigidbody.velocity = Vector3.ProjectOnPlane(velocity, normal);
//				Vector3 perp = Vector3.Cross (velocity, normal);
//				transform.RotateAround (new Vector3 (0.0f, 0.0f, 0.0f), perp, velocity.magnitude * 100 * Time.deltaTime);
				//rigidbody.velocity = Vector3.ProjectOnPlane (velocity, normal);
				Debug.DrawRay (rigidbody.position, rigidbody.velocity * 2.0f);
				// enforce minimum and maximum speeds for the boids
				float speed = rigidbody.velocity.magnitude;
				if (speed > maxVelocity)
				{
					rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
				}
				else if (speed < minVelocity)
				{
					rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
				}
			}

			float waitTime = 0.01f;
			yield return new WaitForSeconds (waitTime);
		}
	}

	private Vector3 Calc ()
	{
		Vector3 randomize = new Vector3 ((Random.value *2) -1, (Random.value * 2) -1, (Random.value * 2) -1);

		randomize.Normalize();
		BoidController boidController = Controller.GetComponent<BoidController>();
		Vector3 flockCenter = boidController.flockCenter;
		Vector3 flockVelocity = boidController.flockVelocity;
		Vector3 follow = chasee.transform.position;
		// cohesion/separation
		flockCenter = flockCenter - transform.position;

		// alignment
		flockVelocity = flockVelocity - rigidbody.velocity;

		// follow
		follow = follow - transform.position;

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
		return (flockCenter *-2 + flockVelocity + follow * 2  + randomize * randomness);
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
	}

	public void Update() {
		if (inited) {

			Vector3 normal = (transform.position - earth.position).normalized;

			Vector3 velocity = rigidbody.velocity + Calc () * Time.deltaTime;
			rigidbody.velocity = Vector3.ProjectOnPlane (velocity, normal);
			//				Vector3 perp = Vector3.Cross (velocity, normal);
			//				transform.RotateAround (new Vector3 (0.0f, 0.0f, 0.0f), perp, velocity.magnitude * 100 * Time.deltaTime);
			//rigidbody.velocity = Vector3.ProjectOnPlane (velocity, normal);
			Debug.DrawRay (rigidbody.position, rigidbody.velocity * 2.0f);
			// enforce minimum and maximum speeds for the boids
			float speed = rigidbody.velocity.magnitude;
			if (speed > maxVelocity) {
				rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
			} else if (speed < minVelocity) {
				rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
			}
		}
	}
}