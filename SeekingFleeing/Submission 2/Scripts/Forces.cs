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

	const float MAX_X = 100f;
	const float MIN_X = 0f;
	const float MAX_Y = 100f;
	const float MIN_Y = 0f;

	// Use this for initialization
	void Start () {
		zombies = new List<GameObject> ();
		humans = new List<GameObject> ();

		for (int i = 0; i < 5; i++) {
			GameObject newZombie = Instantiate (zombie, randomPosition (), Quaternion.identity, transform);
			newZombie.transform.Rotate (0, 0, Random.Range (0f, 6.28f));
			newZombie.GetComponentInChildren<Agent> ().Mass = 1f;
			zombies.Add(newZombie);

			GameObject newHuman = Instantiate (human, randomPosition(), Quaternion.identity, transform);
			newHuman.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
			newHuman.GetComponentInChildren<Agent>().Mass = 1f;
			humans.Add(newHuman);
			newHuman.GetComponent<Human> ().PSG = this.PSG;

			newZombie.GetComponent<Zombie> ().humanTarget = newHuman;
			newHuman.GetComponent<Human> ().zombieClosest = newZombie;
		}
			
        
	}
	
	// Update is called once per frame
	void Update () {

		//Find closest humans
		foreach (GameObject z in zombies) {
			float minDist = Vector3.Distance (z.transform.position, humans [0].transform.position);
			GameObject closestHuman = humans [0];
			foreach (GameObject human in humans) {
				float newDist = Vector3.Distance (z.transform.position, human.transform.position);
				if (newDist < minDist) {
					minDist = newDist;
					closestHuman = human;
				}
			}
			z.GetComponent<Zombie> ().humanTarget = closestHuman;
		}

		//Fund closest zombies
		foreach (GameObject h in humans) {
			float minDist = Vector3.Distance (h.transform.position, zombies [0].transform.position);
			GameObject closestZombie = zombies [0];
			foreach (GameObject zombie in humans) {
				float newDist = Vector3.Distance (h.transform.position, zombie.transform.position);
				if (newDist < minDist) {
					minDist = newDist;
					closestZombie = human;
				}
			}
			h.GetComponent<Human> ().zombieClosest = closestZombie;
		}

        
	}

	Vector3 randomPosition() {
		return new Vector3 (Random.Range(MIN_X, MAX_X), 0, Random.Range(MIN_Y, MAX_Y));
	}
}
