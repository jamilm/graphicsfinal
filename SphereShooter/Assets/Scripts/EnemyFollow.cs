using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {


	public Rigidbody player;
	public float moveSpeed = 4;
	public float maxDist = 10;
	public float minDist = 1;

	private Rigidbody enemy; 
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Rigidbody> (); 
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 localPosition = (player.position - enemy.position).normalized;
		transform.Translate (localPosition * Time.deltaTime * moveSpeed);

//		transform.LookAt (player.position);
//		Debug.Log (transform.rotation);
//		if (Vector3.Distance(transform.position, player.position) >= minDist) {
//			transform.position += transform.forward * moveSpeed * Time.deltaTime;
//		}
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Earth")) 
		{
			Vector3 planeNormal = (other.gameObject.transform.position - enemy.position).normalized;
			transform.position = Vector3.ProjectOnPlane (transform.position, planeNormal);
//			enemy.velocity = new Vector3(0,0,0);
//			minDist = 10000;
//			enemy.gameObject.SetActive (false);
//			other.gameObject.SetActive (false);
			//			Destroy (other.gameObject);
		}
	}


}


