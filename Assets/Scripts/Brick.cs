using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//	This class will help us control the behavior of several types of bricks
public class Brick : MonoBehaviour {
	//	Public properties
	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;

	//	Private properties
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;

	// Use this for initialization
	void Start () {
		//	We identify if the brick is breakable or not (using tags) and set the
		//	isBreakable flag accordingly
		isBreakable = (this.tag == "breakable");

		//	We keep track of how many breakable bricks we haver created, increasing
		//	the static property breakableCount.
		if (isBreakable) {
			breakableCount++;
		}

		//	We set the number ot times the brick has been hit to 0
		timesHit = 0;

		//	We link the LevelManager object to this script
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	//	We use the OnCollisionEnter2D message to play the cracking sound whe the ball
	//	hits the brick, and to decide if we are going to update the brick's sprite.
	void OnCollisionEnter2D (Collision2D collision) {
		//	We use the PlayClipAtPoint method on the AudioSource object to play the
		//	cracking sound, even if the brick is destroyed.
		AudioSource.PlayClipAtPoint(crack,transform.position, 0.8f);

		//	If the brick is a breakable one, we call the HandleHits() method
		if (isBreakable) {
			HandleHits ();
		}
	}

	//	We use this method to react to a brick being hit
	void HandleHits () {
		//	We increase the number of hits the brick has received.
		timesHit++;

		//	Using this new number of hits received, we display the next, and the number
		//	of sprites assigned to this particular type of brick, we compute the max
		//	number of hits the brick can take.
		int maxHits = hitSprites.Length + 1;

		//	If the times the brick has been hit is greater than or equals to the max
		//	number of hits it can take, we proceed to destroy it.  To do that,we need
		//	to decrease the number of breakable bricks in our scene, and call the
		//	BrickDestroyed() method within the level manager instance in our script.
		//	Then, we call the PuffSmoke() method to simulate the bricks destruction and
		//	then we call the Destroy() method for this game object.
		//	However, if the times the brick has been hit is less than the max number of
		//	hits it can take, then we call the LoadSprites() method.
		if (timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed ();
			PuffSmoke ();
			Destroy (gameObject);
		} else {
			LoadSprites ();
		}
	}

	//	We use this method to produce the explosion of the brick destruction.  We make use
	//	of the particle system.
	void PuffSmoke() {
		GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity);
        /*
		 * How would we do this in the newest version of Unity?
		smokePuff.GetComponent<ParticleSystem> ().main.startColor = gameObject.GetComponent<SpriteRenderer> ().color;
		*/
        ParticleSystem sp = smokePuff.GetComponent<ParticleSystem>();
        var main = sp.main;
        main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}

	//	We use this method to change the sprite of the brick, depending on how menay times
	//	it has been hit
	void LoadSprites() {
		//	We compute the array index based on the number of hits the bricks has taken
		int spriteIndex = timesHit - 1;

		//	If the array is not null at the given index, then we change the brick's sprite;
		//	otherwise, we log an error message.
		if (hitSprites [spriteIndex] != null) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		} else {
			Debug.LogError ("Brick sprite missing");
		}
	}
}
