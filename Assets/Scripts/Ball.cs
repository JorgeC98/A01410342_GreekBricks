using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	This class will help us controll the Ball's behavior
public class Ball : MonoBehaviour {

	//	Private properties
	private bool hasStarted = false;	//	Flag to now if the ball has left the paddle, starting our game
	private Paddle paddle;				//	Reference to the Paddle object
	private Vector3 paddleToBallVector;	//	Relative distance between the Ball and the Paddle objects.

	// Use this for initialization
	void Start () {
		//	We link the Paddle objecto to this script.
		paddle = GameObject.FindObjectOfType<Paddle> ();

		//	Now we compute the relative distance between both objects, subtracting vectors.
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//	If the game has not yet started (ball has not left the paddle) then we move the ball
		//	following the paddle if this one has moved.
		if (!hasStarted) {
			//	We change the ball's position, adding the relative distance computed in start to the
			//	paddle's new position.
			this.transform.position = paddle.transform.position + paddleToBallVector;

			//	If the user presses the mouse's main button, then the game starts.  The ball will not
			//	longer follow the paddle and will have a force of its own.
			if (Input.GetMouseButtonDown (0)) {
				//	We set the hasStarted flag to true.
				hasStarted = true;

				//	Then we change the ball's rigid body velocity, creating a new Vector2 force;
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (2f, 10f);
			}
		}
	}

	//	We will use the OnCollisionEnter2D message to randomly change the ball's velocity everytime
	//	it hits something
	void OnCollisionEnter2D (Collision2D collision){
		//	We create a new Vector2 object , called tweak, with random generated values for the
		//	x and y parameters (forces).  The float values will range from 0 to 0.2.
		Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));

		//	If the game has already started, then we sound the audioclip attached to the ball
		//	and then we tweak the ball's velocity
		if (hasStarted) {
			this.GetComponent<AudioSource> ().Play ();
			this.GetComponent<Rigidbody2D> ().velocity += tweak;
		}
	}
}
