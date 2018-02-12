using UnityEngine;
using UnityEngine.SceneManagement;		// Requiered to switch scenes
using System.Collections;

//	This class will help us control how our game's scenes flow
public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
        //  Reset the number of breakable bricks in the scene to 0
		Brick.breakableCount = 0;

        //  Load the scene requested
		//  Debug.Log ("New Level load: " + name);
		//	Application.LoadLevel (name);    -- This method was deprecated a long time ago
		SceneManager.LoadScene (name);
	}

	public void EndGame(){
		//  Debug.Log ("Quit requested");
		Application.Quit ();
	}

    //  We added these functions to our previous LevelManager script.
    public void LoadNextLevel(){
        //  Reset the number of breakable bricks in the scene to 0
		Brick.breakableCount = 0;

        //  Load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BrickDestroyed(){
        //  If the player has destroyed all of the breakable bricks in the scene
        //  then we move to the next scene
		if (Brick.breakableCount <= 0) {
			LoadNextLevel ();
		}
    }
}
