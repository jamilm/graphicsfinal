using UnityEngine;
using System.Collections;

public class BoidController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public float randomness = 1;
	public int flockSize = 20;
	public GameObject prefab;
	public GameObject chasee;

	public Vector3 flockCenter;
	public Vector3 flockVelocity;

	private GameObject[] boids;

	public float radius = 10;
	public float cx = 0;
	public float cy = 0;
	public float cz = 0;

	void Start()
	{
		boids = new GameObject[flockSize];
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
			boid.transform.parent = transform.parent;
			boid.transform.localPosition = randomPosition;
			boid.GetComponent<BoidFlocking>().SetController (gameObject);
//			Debug.Log ("Set Controller");
			boids[i] = boid;
		}
	}

	void Update ()
	{
		Vector3 theCenter = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;

		foreach (GameObject boid in boids)
		{
			theCenter = theCenter + boid.transform.localPosition;
			theVelocity = theVelocity + boid.GetComponent<Rigidbody>().velocity;
		}

		flockCenter = theCenter/(flockSize);
		flockVelocity = theVelocity/(flockSize);
	}
}