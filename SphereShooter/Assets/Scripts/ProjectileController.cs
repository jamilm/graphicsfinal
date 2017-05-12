using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float radius = 0.55f;
	public float translateSpeed = 180.0f;
	public float rotateSpeed = 360.0f;

	float angle = 0.0f;
	Vector3 direction = Vector3.one;
	Quaternion rotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
//		float distance = 10.0f;
//		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
//		mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);
//		mousePosition.z = 0;
//
//		direction = mousePosition - transform.position;
//		direction.z = 0;
//		direction.Normalize ();
	}
	
	// Update is called once per frame
	void Update () {
//		Translate(translateSpeed, 0); 
//		Translate(0, translateSpeed); 
//		UpdatePositionRotation();
	}
		
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


}
