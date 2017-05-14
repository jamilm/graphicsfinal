using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidController : MonoBehaviour
{
	public float minVelocity = 0;
	public float maxVelocity = 0.01f;
	public float randomness = 1;
	public int flockSize = 20;
	public float respawnRate = 5f; 
	public GameObject prefab;
	public GameObject chasee;

	public Vector3 flockCenter;
	public Vector3 flockVelocity;

	public List<GameObject> boids;

	public float radius = 0.8f;
	public float cx = 0;
	public float cy = 0;
	public float cz = 0;

	void Start()
	{
		boids = new List<GameObject> (); 
//		boids = new GameObject[flockSize];
		for (var i=0; i<flockSize; i++)
		{
			float z = Random.Range (-radius, radius); 
			float phi = Random.Range (0, 2 * Mathf.PI);
			float d = Mathf.Sqrt (radius * radius - z * z);

			float px = cx + d * Mathf.Cos (phi);
			float py = cy + d * Mathf.Sin (phi);
			float pz = cz + z; 

			Vector3 randomPosition = new Vector3 (px, py, pz); 
			Quaternion randomRotation = Random.rotation;

			GameObject boid = Instantiate(prefab, randomPosition, randomRotation) as GameObject;
			boid.transform.parent = transform;
			boid.transform.localPosition = randomPosition;
			boid.GetComponent<BoidFlocking>().SetController (gameObject);
//			Debug.Log ("Set Controller");
			boids.Add(boid);
		}
	}

	void Respawn() {
		for (var i = boids.Count; i < flockSize; i++) {

			float z = Random.Range (-radius, radius); 
			float phi = Random.Range (0, 2 * Mathf.PI);
			float d = Mathf.Sqrt (radius * radius - z * z);

			float px = cx + d * Mathf.Cos (phi);
			float py = cy + d * Mathf.Sin (phi);
			float pz = cz + z; 

			Vector3 randomPosition = new Vector3 (px, py, pz); 
			Quaternion randomRotation = Random.rotation;

			GameObject boid = Instantiate(prefab, randomPosition, randomRotation) as GameObject;
			boid.transform.parent = transform.parent;
			boid.transform.localPosition = randomPosition;
			boid.GetComponent<BoidFlocking>().SetController (gameObject);
			//			Debug.Log ("Set Controller");
			boids.Add(boid);
		}
	}

	void Update ()
	{
		Vector3 theCenterSpherical = Vector3.zero;
		Vector3 theCenter = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;

		foreach (GameObject boid in boids)
		{
			if (boid) {
				Vector3 boidCenterEuclid = boid.transform.localPosition;
				Vector3 boidCenterSpherical = EuclideanToSpherical (boidCenterEuclid);
				theCenterSpherical = theCenterSpherical + boidCenterSpherical;

				//theCenter = theCenter + boid.transform.localPosition;
				theVelocity = theVelocity + boid.GetComponent<Rigidbody>().velocity;
			}
		}

		// convert center spherical to euclidean
		theCenterSpherical = theCenterSpherical/(flockSize);
		Vector3 centerEuclid = SphericalToEuclidean (theCenterSpherical);

		// update values
		flockCenter = centerEuclid;
		//flockCenter = theCenter/(flockSize);
		flockVelocity = theVelocity/(flockSize);

		Debug.DrawRay(flockCenter, flockCenter*100.0f, Color.red);

		if (Time.fixedTime % respawnRate == 1) {
			Respawn ();
		}
	}

	// returns spherical vector as r, theta, phi
	private Vector3 EuclideanToSpherical(Vector3 euclid)
	{
		float x = euclid.x;
		float y = euclid.y;
		float z = euclid.z;

		float r = Mathf.Sqrt (x * x + y * y + z * z);
		float theta = Mathf.Acos (z / r);
		float phi = Mathf.Atan (y / x);

		//print (new Vector3 (r, theta, phi));
		return new Vector3 (r, theta, phi);
	}

	// assumes spherical vector is r, theta, phi
	private Vector3 SphericalToEuclidean(Vector3 spherical)
	{
		float r = spherical.x;
		float theta = spherical.y;
		float phi = spherical.z;

		float x = r * Mathf.Sin (theta) * Mathf.Cos (phi);
		float y = r * Mathf.Sin (theta) * Mathf.Sin (phi);
		float z = r * Mathf.Cos (theta);

		return new Vector3 (x, y, z);
	}
}