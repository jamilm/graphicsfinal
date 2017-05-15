using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float radius = 0.55f;
	public float translateSpeed = 180.0f;
	public float rotateSpeed = 360.0f;

	float angle = 0.0f;
//	public Vector3 direction = Vector3.one;
	Quaternion rotation = Quaternion.identity;
	public LineRenderer lineRenderer; 
	private Vector3 direction; 
	// Use this for initialization

//	private GameObject earth; 
//	private BoidController controller;
//	private List<GameObject> boids; 
	void Start () {
//		earth = GameObject.FindGameObjectsWithTag ("Earth")[0];
//		controller = earth.GetComponent<BoidController> (); 
//		boids = controller.boids; 

		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		mousePosition.z = 25f;
		mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);
		//		Vector3 direction = Vector3.ProjectOnPlane (mousePosition, transform.position);
		direction = Vector3.Cross(transform.position, mousePosition);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.RotateAround(new Vector3(0.0f,0.0f,0.0f), direction, 50 * Time.deltaTime);
//		foreach (GameObject boid in boids) {
//
//			if (transform != null && boid != null) {
//				float dist = Vector3.Distance (transform.position, boid.transform.position);
//
//				if (dist < 1f) {
//					boid.GetComponent<BoidFlocking> ().chasee = gameObject;
//				}
//			}
//		}
//		Debug.Log (direction);
//		Debug.Log (mousePosition);
//		lineRenderer.SetPosition (0, transform.position);
//		lineRenderer.SetPosition (1, mousePosition);
//		GL.Begin(GL.LINES);
//		lineMat.SetPass (0);
//		GL.Color (Color.green);
//		GL.Vertex (transform.position);
//		GL.Vertex (perpendicular);
//		GL.End ();
	}

//	void OnDrawGizmos() {
//		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
//		Gizmos.DrawRay (transform.position, mousePosition);
//	}
		
	void Rotate(float amount)
	{
		angle += amount * Mathf.Deg2Rad * Time.deltaTime;
	}

	void Translate(float x, float y)
	{
		var perpendicular = new Vector3(-direction.y, direction.x);
		var verticalRotation = Quaternion.AngleAxis(y * Time.deltaTime, perpendicular);
		var horizontalRotation = Quaternion.AngleAxis(x * Time.deltaTime, direction);
		rotation *= horizontalRotation * verticalRotation;
	}

	void UpdatePositionRotation()
	{
		transform.localPosition = rotation * Vector3.forward * radius;
		transform.rotation = rotation * Quaternion.LookRotation(direction, Vector3.forward);
	}

//
//	void OnTriggerEnter (Collider col)
//    {
//        if(col.gameObject.name == "Cube(Clone)")
//        {
//            Destroy(col.gameObject);
//            Destroy(this.gameObject);
//        }
//    }


}
