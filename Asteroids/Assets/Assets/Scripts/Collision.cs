using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public List<GameObject> asteroids;
	public List<GameObject> bullets;
	public List<GameObject> asteroidsToAdd;
	public List<GameObject> bulletsToAdd;
	public List<GameObject> asteroidsToSplit;
	public List<GameObject> bulletsToRemove;
	public GameObject player;
	private Life lifeScript;

	// Use this for initialization
	void Start () {
		asteroids = new List<GameObject>();
		bullets = new List<GameObject>();
		asteroidsToAdd = new List<GameObject>();
		bulletsToAdd = new List<GameObject>();
		bulletsToRemove = new List<GameObject>();
		asteroidsToSplit = new List<GameObject>();
		lifeScript = gameObject.GetComponent<Life> ();
	}
	
	// Update is called once per frame
	void Update () {
		BulletCollisions ();
		PlayerCollisions ();
		UpdateObjectLists ();
	}

	void BulletCollisions() {

		foreach (GameObject bullet in bullets) {
			foreach (GameObject asteroid in asteroids) { 

				bool collided = areCollided(bullet, asteroid);
				if (collided) {
					//If a bullet has collided with an asteroid...

					//Remove the bullet from the bullets list
					bulletsToRemove.Add(bullet);

					//Remove the asteroid
					asteroidsToSplit.Add(asteroid);

				}

			}
		}
			
	}

	void PlayerCollisions() {
		foreach (GameObject asteroid in asteroids) { 
			bool collided = areCollided(player, asteroid);
			if (collided) {
				lifeScript.DecrementLife ();
				break;
			}
		}
	}

	private bool areCollided(GameObject a, GameObject b) {
		
		//Get needed info for collision detection
		Bounds aBounds = a.GetComponent<SpriteRenderer>().bounds;
		Bounds bBounds = b.GetComponent<SpriteRenderer>().bounds;
		Vector3 aCenter = aBounds.center;
		Vector3 bCenter = bBounds.center;
		float aRadius = aBounds.extents.y;
		float bRadius = bBounds.extents.y;

		//Check if collided
		bool collided = (aRadius + bRadius > Vector3.Distance (aCenter, bCenter));
		return collided;
	}

	public void AddAsteroid(GameObject asteroid) {
		asteroidsToAdd.Add (asteroid);
	}

	public void AddBullet(GameObject bullet) {
		bulletsToAdd.Add (bullet);
	}

	private void UpdateObjectLists() {

		//Split all asteroids hit this frame
		foreach (GameObject asteroid in asteroidsToSplit) {

			//Remove the asteroid
			asteroids.Remove(asteroid);

			//Split the asteroid
			List<GameObject> splitResults = asteroid.GetComponent<Asteroid>().Split();
			if (splitResults != null) {
				asteroids.AddRange (splitResults);
			}
				
		}

		//Remove all bullets hit this frame
		foreach (GameObject bullet in bulletsToRemove) {
			bullets.Remove (bullet);
			Destroy (bullet);
		}

		asteroids.AddRange (asteroidsToAdd);
		bullets.AddRange (bulletsToAdd);

		asteroidsToSplit.Clear ();
		bulletsToRemove.Clear ();
		asteroidsToAdd.Clear();
		bulletsToAdd.Clear ();
	}

}
