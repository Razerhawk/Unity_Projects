using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {


	/// <summary>
	/// Behavior and methods of Tie Fighter (my ship)
	/// </summary>

	// Set a Rigidbody2D to evade the retrieving of this component everytime we apply thrust
	public Rigidbody2D ship;

	// Set a CircleCollider2D to evade the retrieving of this component everytime the ship dissapears
	public CircleCollider2D circleCollider;

	// Direction of Thrust
	Vector3 thrustDirection;
	float zRotation = 0F;


	// The Force of the impulse
	const float ThrustForce = 2F;

	// Degrees of movement per second
	const float RotateDegreesPerSecond = 80F;

	void OnBecameInvisible() { 


		
		if (ship.position.x > ScreenUtils.ScreenRight) {

			Vector3 newpos = new Vector3 (ScreenUtils.ScreenLeft, ship.position.y, -Camera.main.transform.position.z);
			transform.position = newpos;


		} 
		if (ship.position.x < ScreenUtils.ScreenLeft) {

			Vector3 newpos = new Vector3 (ScreenUtils.ScreenRight, ship.position.y, -Camera.main.transform.position.z);
			transform.position = newpos; 

		}
		if (ship.position.y > ScreenUtils.ScreenTop) {

			Vector3 newpos = new Vector3 (ship.position.x, ScreenUtils.ScreenBottom, -Camera.main.transform.position.z);
			transform.position = newpos;

		}
		if (ship.position.y < ScreenUtils.ScreenBottom) {

			Vector3 newpos = new Vector3 (ship.position.x, ScreenUtils.ScreenTop, -Camera.main.transform.position.z);
			transform.position = newpos;

		}


	}

	// Use this for initialization
	void Start () {

		thrustDirection.x = 1;
		ship = gameObject.GetComponent<Rigidbody2D>();
		circleCollider = gameObject.GetComponent<CircleCollider2D> ();
	}


	void Update (){

		// calculate rotation amount and apply rotation
		float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
		float rotationInput = Input.GetAxis ("Rotate");



		if (rotationInput < 0) {

			rotationAmount *= -1;
			zRotation += rotationAmount;
			transform.Rotate(Vector3.forward, rotationAmount);
			ship.transform.eulerAngles = new Vector3 (0, 0, zRotation);


			thrustDirection.x = Mathf.Cos((Mathf.Deg2Rad)*zRotation);
			thrustDirection.y = Mathf.Sin((Mathf.Deg2Rad)*zRotation);
		}


		if (rotationInput > 0) {

			rotationAmount *= 1;
			zRotation += rotationAmount;
			transform.Rotate(Vector3.forward, rotationAmount);
			ship.transform.eulerAngles = new Vector3 (0, 0, zRotation);


			thrustDirection.x = Mathf.Cos((Mathf.Deg2Rad)*zRotation);
			thrustDirection.y = Mathf.Sin((Mathf.Deg2Rad)*zRotation);
		}

	}

		
	// It runs a twice frames per second compared to the update method, and it is applied to Rigidbodys
	void FixedUpdate ()
	{

		// Apply Impulse when we press the Space Bar
		if (Input.GetAxis ("Thrust") != 0) {
			

			ship.AddForce (ThrustForce * thrustDirection, ForceMode2D.Force);

		} 
		else 
		{
			// Stops when Space bar is not pressed
			ship.velocity = Vector3.zero;
		}

	}
}
