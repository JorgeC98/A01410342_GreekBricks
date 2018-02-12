using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	This class will help us control the Paddle's behavior.
public class Paddle : MonoBehaviour {
	// Public properties
	public bool autoplay = false;	//	This variable will help us automatically test our scenes when true.
	public float minX;				//	Left boundary of our gamespace
	public float maxX;				//	Right space of our gamespace

	//	Private properties
	private Ball ball;				//	Reference to the Ball object

	// Use this for initialization
	void Start () {
		//	We link the Ball object in our scene to this script
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		//	If the autoplay property is set to false, then the player must use the mouse to
		//	move the paddle; otherwise, the paddle will move automatically following the ball
		//	in the x axis.
		if (!autoplay) {
			MoveWithMouse ();
		} else {
			AutoPlay ();
		}
	}

	void AutoPlay() {
		//	We need to obtain the ball position in order to reflect its x position on the paddle

		//	We create a new Vector3 object to retrieve the paddle's curent position.  We also
		//	create another Vector3 object to retrieve the ball's position.
		Vector3 paddlePos = this.transform.position;
		Vector3 ballPos = ball.transform.position;

		//	We use the Mathf.Clamp() method to compute the paddle's new x position, so it remains
		//	within range.  The paddle's position should be half a unit to the left of the ball's
		//	position whenever this is possible, so the ball could always hit the center of the paddle.
		paddlePos.x = Mathf.Clamp(ballPos.x - 0.5f, minX, maxX);

		//	Now that we have the paddle's new x position, we assign the whole Vector3 object to
		//	the paddle's position.
		this.transform.position = paddlePos;
	}

	void MoveWithMouse (){
		// Here we need to obtain the mouse position and transform it to a
		// valid screen position in x.

		//	We create a new Vector3 object to retrieve the paddle's current position
		Vector3 paddlePos = new Vector3 (this.transform.position.x, this.transform.position.y,
			                    this.transform.position.z);

		//	We compute the mouse position realtively to the screen width, and then transform that
		//	proportion to our world units
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;

		//  We use the Mathf.Clamp() method to compute the paddle's new x position, so it remains
		//	within range.
		paddlePos.x = Mathf.Clamp (mousePosInBlocks, minX, maxX);

		//	Now that we have the paddle's new x position, we assign the whole Vector3 object to
		//	the paddle's position.
		this.transform.position = paddlePos;
	}
}
