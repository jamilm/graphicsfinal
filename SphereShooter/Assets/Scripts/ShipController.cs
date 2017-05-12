using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public float radius = 0.55f;
	public float translateSpeed = 180.0f;
	public float rotateSpeed = 360.0f;

	float angle = 0.0f;
	Vector3 direction = Vector3.one;
	Quaternion rotation = Quaternion.identity;

	// Update is called once per frame
	void Update () {
		direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

		// Rotate with left/right arrows
		if (Input.GetKey(KeyCode.LeftArrow))  Translate(translateSpeed, 0); //Rotate( rotateSpeed);
		if (Input.GetKey(KeyCode.RightArrow)) Translate(-translateSpeed, 0); //Rotate(-rotateSpeed);

		// Translate forward/backward with up/down arrows
		if (Input.GetKey(KeyCode.UpArrow))    Translate(0,  translateSpeed);
		if (Input.GetKey(KeyCode.DownArrow))  Translate(0, -translateSpeed);

		UpdatePositionRotation();
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
