using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public Vector3 velocity;
	public float maxVel = 20f;
	public GameObject smallerPrefab = null;
	private float variation = 0.4f;

	void Awake() {
		velocity = new Vector3 (Random.Range(-maxVel, maxVel), Random.Range(-maxVel, maxVel), 0);
	}

	// Use this for initialization
	void Start () {
		Color color;
		int rand = Random.Range (0, 2);
		switch (rand) {
			case 0:
				color = Color.magenta;
				break;
			case 1:
				color = Color.blue;
				break;
			case 2:
				color = Color.gray;
				break;
			default:
				color = Color.black;
				break;
		}
		gameObject.GetComponent<SpriteRenderer> ().color = color;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}

	public List<GameObject> Split() {
		if (smallerPrefab != null) {
			List<GameObject> smallerAsteroids = new List<GameObject> ();
			smallerAsteroids.Add (Instantiate (smallerPrefab, transform.position, Quaternion.identity));
			smallerAsteroids.Add (Instantiate (smallerPrefab, transform.position, Quaternion.identity));
			foreach (GameObject asteroid in smallerAsteroids) {
				asteroid.GetComponent<Asteroid> ().SetVelocity (
					velocity + 
					new Vector3(
						Random.Range(-variation, variation), 
						Random.Range(-variation, variation), 
						0));
			}
			Destroy (gameObject);
			return smallerAsteroids;
		} else {
			Destroy (gameObject);
			return null;
		}
	}

	public void SetVelocity(Vector3 newVelocity) {
		velocity = newVelocity;
		Debug.Log (velocity);
	}

}
