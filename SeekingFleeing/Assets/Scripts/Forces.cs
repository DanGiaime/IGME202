using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Forces : MonoBehaviour {

	Vehicle[] children;
	public GameObject zombie;
	public GameObject human;
	public GameObject PSG;

    private List<GameObject> zombies;
    private List<GameObject> humans;
	private World world;

	const float MAX_X = 100f;
	const float MIN_X = 0f;
	const float MAX_Y = 100f;
	const float MIN_Y = 0f;

	// Use this for initialization
	void Start () {
		zombies = new List<GameObject> ();
		humans = new List<GameObject> ();
		world = new World ();

		for (int i = 0; i < 5; i++) {
			GameObject newZombie = Instantiate (zombie, randomPosition (), Quaternion.identity, transform);
			newZombie.transform.Rotate (0, 0, Random.Range (0f, 6.28f));
			newZombie.GetComponentInChildren<Agent> ().Mass = 1f;
			zombies.Add(newZombie);
			newZombie.GetComponent<Zombie> ().world = world;

			GameObject newHuman = Instantiate (human, randomPosition(), Quaternion.identity, transform);
			newHuman.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
			newHuman.GetComponentInChildren<Agent>().Mass = 1f;
			humans.Add(newHuman);
			newHuman.GetComponent<Human> ().PSG = this.PSG;
			newHuman.GetComponent<Human> ().world = world;

			newZombie.GetComponent<Zombie> ().humanTarget = newHuman;
			newHuman.GetComponent<Human> ().zombieClosest = newZombie;

			world.AddHuman(newHuman);
			world.AddZombie(newZombie);
		}

		for (int i = 0; i < 500; i++) {

			GameObject newObject = Instantiate (PSG, randomPosition(), Quaternion.identity, transform);
			newObject.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
			world.AddObject(newObject);

		}
			
        
	}
	
	// Update is called once per frame
	void Update () {

        
	}

	Vector3 randomPosition() {
		return new Vector3 (Random.Range(MIN_X, MAX_X), 1, Random.Range(MIN_Y, MAX_Y));
	}
}
