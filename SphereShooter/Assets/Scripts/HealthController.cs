using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	private AudioSource hitSound; 

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;
		hitSound = GetComponents<AudioSource> () [1];

		Canvas parentCanvas = healthSlider.GetComponentInParent<Canvas> ();
		Rect canvasRect = parentCanvas.pixelRect;
		RectTransform rt = healthSlider.GetComponent<RectTransform> ();
		//rt.sizeDelta = new Vector2( canvasRect.width*0.1f, canvasRect.height);
	}
	
	// Update is called once per frame
	void Update () {
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			//damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
		if (currentHealth <= 0) {
			ShipController.alive = false;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			hitSound.Play ();
			Destroy(col.gameObject);
			currentHealth -= 10;
			healthSlider.value = currentHealth;
		}
	}
}