using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public List<GameObject> asteroids;
	public List<GameObject> bullets;
	public List<GameObject> asteroidsToAdd;
	public List<GameObject> bulletsToAdd;
	public List<GameObject> asteroidsToSplit;
	public List<GameObject> asteroidsNowInCamera;
	public List<GameObject> asteroidsOutsideCamera;
	public GameObject player;
	private Life lifeScript;
	private QuadTreeNode quadTree;
	bool showLines = false;

	// Use this for initialization
	void Start () {
		asteroids = new List<GameObject>();
		bullets = new List<GameObject>();
		asteroidsToAdd = new List<GameObject>();
		bulletsToAdd = new List<GameObject>();
		asteroidsToSplit = new List<GameObject>();
		asteroidsOutsideCamera = new List<GameObject> ();
		asteroidsNowInCamera = new List<GameObject> ();
		lifeScript = gameObject.GetComponent<Life> ();


		float height = Camera.main.orthographicSize * 2;    
		float width = height * Screen.width / Screen.height;
		Vector3 cameraExtents = new Vector3(width, height, 0); 
		Bounds cameraBounds = new Bounds ((Vector2)Camera.main.transform.position, cameraExtents + new Vector3(5, 5, 0) );
		quadTree = new QuadTreeNode (cameraBounds);
	}
	
	// Update is called once per frame
	void Update () {
		BulletCollisions ();
		PlayerCollisions ();
		quadTree.Update (quadTree);
		UpdateObjectLists ();

		if(Input.GetKeyDown(KeyCode.D))
		{
			showLines = !showLines;
		}

		if (showLines) {
			quadTree.DrawTree ();
		}
	}

	private void BulletCollisions() {

		// Check all bullets to see if any have collided with an asteroid
		for (int i = 0; i < bullets.Count; i++) {
			GameObject asteroid = quadTree.Collides (bullets[i]);

			if (asteroid != null) {
				Debug.Log ("BULLET HIT!");
				//If a bullet has collided with an asteroid...

				//Remove the bullet from the bullets list
				Destroy(bullets[i]);
				bullets.Remove(bullets[i]);
				i--;

				//Remove the asteroid
				asteroidsToSplit.Add(asteroid);

			}

		}			
	}

	private void PlayerCollisions() {
		GameObject asteroid = quadTree.Collides (player);
		if (asteroid != null) {
			lifeScript.DecrementLife ();
		}
	}

	public static bool AreCollided(GameObject a, GameObject b) {
		
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
		asteroids.Add (asteroid);
		quadTree.Insert (asteroid);
	}

	public void AddBullet(GameObject bullet) {
		bullets.Add (bullet);
	}

	private void UpdateObjectLists() {


		//Split all asteroids hit this frame
		foreach (GameObject asteroid in asteroidsToSplit) {

			//Remove the asteroid
			asteroids.Remove(asteroid);
			quadTree.Remove (asteroid);
			lifeScript.score++;
			//Split the asteroid
			List<GameObject> splitResults = asteroid.GetComponent<Asteroid>().Split();
			if (splitResults != null) {
				asteroids.AddRange (splitResults);
				foreach (GameObject newAsteroid in splitResults) {
					quadTree.Insert(newAsteroid);
				}
			}
			Destroy (asteroid);

		}

		foreach (GameObject asteroid in asteroidsOutsideCamera) {
			if (quadTree.Insert (asteroid)) {
				asteroidsNowInCamera.Add (asteroid);
				Debug.Log ("Succesfully inserted");
			}
		}

		foreach (GameObject asteroid in asteroids) {
			if (!quadTree.Encapsulates (asteroid)) {
				asteroidsOutsideCamera.Add (asteroid);
			}
		}
			

		foreach (GameObject asteroid in asteroidsNowInCamera) {
			asteroidsOutsideCamera.Remove (asteroid);
		}

		// Clear all temp lists for next frame
		asteroidsNowInCamera.Clear();
		asteroidsToSplit.Clear ();
		asteroidsToAdd.Clear();
		bulletsToAdd.Clear ();
	}

}
