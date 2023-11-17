using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Loads Next Level After Finishing Hole
public class LevelHole : MonoBehaviour {
    
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ball") { //Checks if the ball enters the hole
			SceneManager.LoadSceneAsync(1); //loads the next scene in the background
		}
	}

}
