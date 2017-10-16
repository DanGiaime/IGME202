using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public SpriteRenderer player;
	private float currinvincibilityTime;
	public float invincibilityTime;
	public int lives;
	public int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currinvincibilityTime > 0) {
			currinvincibilityTime -= Time.deltaTime;
			player.color = Color.green;
		} else {
			currinvincibilityTime = 0;
			player.color = Color.white;
		}
	}

	public void DecrementLife() {
		if (currinvincibilityTime == 0) {
			lives--;
			currinvincibilityTime = invincibilityTime;
		}
		if (lives == 0) {
			lives = 3;
			score = 0;
		}
	}

	void OnGUI() {
		GUI.Label (new Rect(10, 10, 100, 20), "You have " + lives + " lives.");
		GUI.Label (new Rect(10, 20, 100, 20), "Score: " + score);
	}
}
