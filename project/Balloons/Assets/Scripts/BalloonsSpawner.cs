using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsSpawner : MonoBehaviour {
	
	[SerializeField] private GameObject balloonPrefab;
	[SerializeField] private float spawnTime;
	[Range(0.1f, 5f)][SerializeField] private float minSize;
	[Range(0.1f, 5f)][SerializeField] private float maxSize;
	[SerializeField] private float normalSpeed; // Скорость шарика размером 1 в начальный момент времени
	[Range(1f, 20f)][SerializeField] private float speedMultiplier; // Коэфициент увеличения скорости со временем (Во сколько раз скорость шариков будет больше в конечный момент времени)
	[SerializeField] private float pointsCof; // Очки за шарик размером 1


	private GameManager gm;
	private List<GameObject> balloons;
	private float spawnTimer;
	private float cameraWidth;
	private float cameraHeight;
	private float acceleration; // Увеличение скорости со временем
	private float destructiveHeight; // Высота на которой шарики разрушаются

	void Awake(){
		gm = GetComponent<GameManager> ();
	}

	void Start () {
		balloons = new List<GameObject> ();
		spawnTimer = spawnTime;
		Camera camera = Camera.main;
		cameraHeight = 2 * camera.orthographicSize;
		cameraWidth = cameraHeight * camera.aspect;
		acceleration = (speedMultiplier * normalSpeed - normalSpeed) / gm.gameTime; // a = (Vкон - Vнач) / t
		destructiveHeight = camera.orthographicSize + maxSize / 2;
	}

	void Update () {
		if (spawnTimer >= spawnTime) {
			SpawnRandomBalloon ();
			spawnTimer = 0;
		} else {
			spawnTimer += Time.deltaTime;
		}
	}

	void SpawnRandomBalloon(){
		float size = Random.Range (minSize, maxSize);
		float xMax = (cameraWidth - size) / 2;
		float yMin = -(cameraHeight - size) / 2;
		float yMax = -size / 2;
		float posX = Random.Range (-xMax, xMax);
		float posY = Random.Range (yMin, yMax);
		
		GameObject newBalloon = Instantiate (balloonPrefab, new Vector2(posX, posY), Quaternion.identity);
		newBalloon.transform.localScale = new Vector2 (size,size);
		newBalloon.GetComponent<SpriteRenderer> ().color = RandomColor ();
		balloons.Add (newBalloon);

		Balloon balloonComp = newBalloon.GetComponent<Balloon> ();
		balloonComp.speed = (normalSpeed + acceleration * (gm.gameTime - gm.timeLeft)) / size;  // Vтек = Vнач + at
		balloonComp.points = (int)(pointsCof / size);
		balloonComp.destructiveHeight = destructiveHeight;
	}

	Color RandomColor(){
		float red = 0, green = 0, blue = 0;
		int a = Random.Range (0, 3);
		switch (a) {
		case 0:
			red = 1f;
			green = 1f;
			blue = Random.Range(0, 1f);
			break;
		case 1:
			red = 1f;
			green = Random.Range(0, 1f);
			blue = 1f;
			break;
		case 2:
			red = Random.Range(0, 1f);
			green = 1f;
			blue = 1f;
			break;
		}
		Color col = new Color(red, green, blue, 1f);
		return col;
	}

	public void DestroyAllBalloons(){
		foreach (GameObject b in balloons) {
			Destroy (b);
		}
		balloons = new List<GameObject> ();
	}
}
