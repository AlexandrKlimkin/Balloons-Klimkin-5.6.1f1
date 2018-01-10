using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	[SerializeField] private Text scoreText;
	[SerializeField] private Text timeText;
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private Text totalScoreText;
	public float gameTime;

	[HideInInspector] public float timeLeft;
	[HideInInspector] public bool gameOver;
	private int points;
	private BalloonsSpawner balloonsSpawner;

	void Awake(){
		balloonsSpawner = GetComponent<BalloonsSpawner> ();
	}

	void Start () {
		scoreText.text = "0";
		timeText.text = gameTime.ToString ();
		timeLeft = gameTime;
	}

	void Update () {
		if (timeLeft <= 0) {
			GameOver ();
		} else {
			timeLeft -= Time.deltaTime;
			timeText.text = ((int)(timeLeft) + 1).ToString ();
		}
	}

	public void AddPoints(int points){
		this.points += points;
		scoreText.text = this.points.ToString ();
	}

	void GameOver(){
		Time.timeScale = 0;
		gameOver = true;
		timeText.text = "0";
		gameOverMenu.SetActive (true);
		totalScoreText.text = points.ToString ();
	}

	public void StartNewRound(){
		Time.timeScale = 1;
		balloonsSpawner.DestroyAllBalloons ();
		gameOver = false;
		timeLeft = gameTime;
		timeText.text = gameTime.ToString ();
		points = 0;
		scoreText.text = "0";
		gameOverMenu.SetActive (false);
	}
}
