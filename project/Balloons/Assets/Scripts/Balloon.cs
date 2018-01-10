using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {
	
	private GameManager gm;
	[HideInInspector] public float speed;
	[HideInInspector] public int points;
	[HideInInspector] public float destructiveHeight;

	void Awake(){
		gm = FindObjectOfType<GameManager> ();
	}

	void Update(){
		if (transform.position.y <= destructiveHeight) {
			transform.position += new Vector3 (0, speed * Time.deltaTime);
		} else {
			Destroy (gameObject);
		}
	}

	void OnMouseDown(){
		if(!gm.gameOver){
			Destroy (gameObject);
			gm.AddPoints (points);
		}
	}
}
