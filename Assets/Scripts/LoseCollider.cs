using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	This class will detect when the ball has touched the floor and launch the Game Over screen.
public class LoseCollider : MonoBehaviour {

	//	This method is triggered when the ball touches the LoseCollider game object.
	//	It loads the "Lose" scene (Game Over)
	void OnTriggerEnter2D(Collider2D collider) {
		
		//	We need to instantiate a LevelManager object to call the LoadLevel method.
		LevelManager levelManager = new LevelManager ();
		levelManager.LoadLevel ("Lose");
	}
}
