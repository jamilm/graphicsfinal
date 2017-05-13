using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public Rigidbody coin; 
	public float spawnTime = 5f;
	public int numParticles = 5;
	public float radius = 30; 
	public float cx = 0;
	public float cy = 0;
	public float cz = 0; 

	// Use this for initialization
	void Start () {
//		coin = GetComponent<Rigidbody> ();
		InvokeRepeating ("Spawn", spawnTime, spawnTime); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn() {

		// randomly spawn x particles on a sphere every 5 seconds
		for (int i = 0; i < numParticles; i++) {
			float z = Random.Range (-radius, radius); 
			float phi = Random.Range (0, 2 * Mathf.PI);
			float d = Mathf.Sqrt (radius * radius - z * z);

			float px = cx + d * Mathf.Cos (phi);
			float py = cy + d * Mathf.Sin (phi);
			float pz = cz + z; 

			Vector3 randomPosition = new Vector3 (px, py, pz); 
			Quaternion rotation = Random.rotation;
			Rigidbody newCoin = Instantiate (coin, randomPosition, rotation);

			newCoin.gameObject.SetActive (true);
		}

	}
}
