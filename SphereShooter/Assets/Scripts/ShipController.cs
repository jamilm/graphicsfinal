using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

	public GameObject bulletPrefab;
	public GameObject superBulletPrefab; 
	public Transform bulletSpawn;
	public Slider bulletSlider;
	int kills;

	public float radius = 0.55f;
	public float translateSpeed = 90.0f;
	public float rotateSpeed = 360.0f;
	public float superBulletReq = 10f;
	private int lastKillVal = 0;

	private float bulletsPerSecond = 5f;
 	private bool shooting = false;
	public static bool superShooting = false; 
	private bool superBulletUsed = true;
 	public static bool alive = true;
	float angle = 0.0f;
	Vector3 direction = Vector3.one;
	Quaternion rotation = Quaternion.identity;

	private AudioSource laser; 

	public Text gameOver; 

	private AudioSource superBulletSound;

	void Start () {
		InvokeRepeating("Fire", 0.0f, 1.0f / bulletsPerSecond);
		laser = GetComponents<AudioSource> ()[0];
		superBulletSound = GetComponents<AudioSource> () [2];
	}

	// Update is called once per frame
	void Update () {

		kills = ScoreController.score;
		if(bulletSlider.value != 10) {
			kills -= lastKillVal;
			bulletSlider.value = (kills % 11);
		}
		// else if(ShipController.superShooting) {
		// 	bulletSlider.value = 0;
		// }

		// movement
		float currScore = ScoreController.score;
		if (superBulletUsed && currScore > 0 && currScore % superBulletReq == 0) {
			superBulletUsed = false; 
		}

		direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

		// Rotate with left/right arrows
		if (alive) {
			if (Input.GetKey (KeyCode.A))
				Rotate( rotateSpeed); //Translate (translateSpeed, 0);
			if (Input.GetKey (KeyCode.D))
				Rotate(-rotateSpeed);  //Translate (-translateSpeed, 0);

			// Translate forward/backward with up/down arrows
			if (Input.GetKey (KeyCode.W))
				Translate (0, translateSpeed);
			if (Input.GetKey (KeyCode.S))
				Translate (0, -translateSpeed);

			UpdatePositionRotation ();

			// shooting
			shooting = false;
			superShooting = false;
			if (Input.GetKey (KeyCode.Space)) {
				shooting = true;
			}
			if (Input.GetKey (KeyCode.E) && !superBulletUsed) {
				superShooting = true; 
			}
		} 
		else {
			gameOver.enabled = true;
			shooting = false;
			superShooting = false;
		}
	}

	void Fire()
	{
		if (!shooting && !superShooting) {
			return;
		}
		// Create the Bullet from the Bullet Prefab
		else if (shooting) {
			var bullet = (GameObject)Instantiate (
				             bulletPrefab,
				             transform.position,
				             transform.rotation);
			Destroy(bullet, 2.0f);  

		} else {
			lastKillVal = kills;
			bulletSlider.value = 0;
			superBulletSound.Play ();
			superBulletUsed = true; 
			var superBullet = (GameObject)Instantiate (
				                 superBulletPrefab,
				                 transform.position,
				                 transform.rotation);
			print ("Super bullet created");
			Destroy (superBullet, 5f);
		}
//		ProjectileController otherController = (ProjectileController) bullet.GetComponent ("Projectile Controller");
//		otherController.direction = direction;

		// Add velocity to the bullet
		// bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Destroy the bullet after 2 seconds
		laser.Play();
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
