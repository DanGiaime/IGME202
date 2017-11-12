using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	public List<GameObject> humans;
	public List<GameObject> zombies;
	public List<GameObject> objects;

	public World() {
		humans = new List<GameObject> ();
		zombies = new List<GameObject> ();
		objects = new List<GameObject> ();
	}

	public void AddHuman(GameObject human) {
		if (human.GetComponent<Human> () != null) {
			humans.Add (human);
		}
	}

	public void AddZombie(GameObject zombie) {
		if (zombie.GetComponent<Zombie> () != null) {
			zombies.Add (zombie);
		}
	}

	public void AddObject(GameObject obj) {
		objects.Add (obj);
	}
}
