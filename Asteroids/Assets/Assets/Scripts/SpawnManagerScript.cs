using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

	public GameObject sceneManager;
	public GameObject asteroidSmall;
	public GameObject asteroidMedium;
	public GameObject asteroidLarge;
	private float percentSmall = 0.3f;
	private float percentMedium = 0.4f;
	private float percentLarge = 0.3f;
	private float spawnFreq = 1.0f/60.0f;
	private float height;
	private float width;

	// Use this for initialization
	void Start () {

		height = Camera.main.orthographicSize * 2;    
		width = height * Screen.width / Screen.height;
//		height += Camera.main.transform.position.y;
//		width += Camera.main.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Decide whether or not to spawn an asteroid this frame
		float shouldSpawn = Random.Range (0.0f, 1.0f);

		//Spawn on if we should
		if (shouldSpawn < spawnFreq) {
			sceneManager.GetComponent<Collision> ().AddAsteroid(SpawnRandom ());
		}
	}

	public GameObject SpawnRandom() {

		//Decide which asteroid to spawn
		float rand = Random.Range (0.0f, 1.0f);
		GameObject prefab = null;

		//Set prefab accordingly
		if (rand < percentSmall) {
			prefab = asteroidSmall;
		} else if (rand < percentSmall + percentMedium) {
			prefab = asteroidMedium;
		} else if (rand < percentSmall + percentMedium + percentLarge) {
			prefab = asteroidLarge;
		} 

		//Decide where to spawn asteroid
		int sideOrTop = Random.Range (0, 2);
		int whichSide = Random.Range (0, 2);

		float xVel = (whichSide == 1) ? 0 : width;
		float yVel = (whichSide == 1) ? 0 : height;
		if (sideOrTop == 1) {
			xVel = Random.Range (0, width);
		} else {
			yVel = Random.Range (0, height);
		}

		//Give asteroid a random position
		Vector3 position = new Vector3 (xVel,yVel,0);

		// Instantiate and return asteroid
		return Instantiate (prefab, position, Quaternion.identity, sceneManager.transform); 
	}
}
