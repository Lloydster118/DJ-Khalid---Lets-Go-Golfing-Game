using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;//allows you to call the level manager script without referencing it in unity

	[Header("References")]
	[SerializeField] private TextMeshProUGUI strokeUI;//stroke text UI
	[SerializeField] private GameObject levelCompleteUI;//level complete UI
	[SerializeField] private GameObject gameOverUI;//game over UI
	[SerializeField] private AudioSource Music;//audio source


	[Header("Attributes")]
	[SerializeField] private int maxStrokes;//amount of strokes per game

	private int strokes;
	public bool outOfStrokes;//out of strokes per game
	public bool levelCompleted;//finish level

	private void Awake() {
		main = this;//
	}
	//Game starting
	private void Start() {
		UpdateStrokeUI(); //calls the UpdateStrokeUI function
	}
	//
	public void IncreaseStroke () { //adds one stroke once it registers an input
		strokes++;
		UpdateStrokeUI();

		if (strokes >= maxStrokes) {
			outOfStrokes = true; // sets out of strokes to true once you reach max amount of strokes
		}
	}

	public void LevelComplete() {
		levelCompleted = true;
		Music.Play();
		levelCompleteUI.SetActive(true); //checks if level is completed, if so it plays music and displays next level menu
	}

	public void GameOver () {
		gameOverUI.SetActive(true);//displays end level screen
	}

	private void UpdateStrokeUI() {
		strokeUI.text = strokes + "/" + maxStrokes;//shows the text display order having current number of strokes / to max strokes taken
	}

	public void IncreaseMaxStroke()//increase the max stroke display when activating the powerup
	{
		maxStrokes++;
		UpdateStrokeUI();
	}
}
