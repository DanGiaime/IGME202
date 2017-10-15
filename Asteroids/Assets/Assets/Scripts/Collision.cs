using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public List<SpriteRenderer> sprites;
	public SpriteRenderer player;
	private Life lifeScript;

	// Use this for initialization
	void Start () {
		sprites = new List<SpriteRenderer>(gameObject.GetComponentsInChildren<SpriteRenderer> ());
		lifeScript = gameObject.GetComponent<Life> ();
	}
	
	// Update is called once per frame
	void Update () {
		BoundingCircle ();
	}

	void BoundingCircle() {
		for (int i = 0; i < sprites.Count; i++) {
			for (int j = 0; j < sprites.Count; j++) {
				if (i == j)
					continue;
				bool collided = true;
				Vector3 center1 = sprites [i].bounds.center;
				Vector3 center2 = sprites [j].bounds.center;
				float radius1 = sprites [i].bounds.extents.x;
				float radius2 = sprites [j].bounds.extents.x;
				if (Vector3.Distance (center1, center2) < radius1 + radius2) {
					sprites [i].color = Color.red;
					break;
				} else {
					sprites [i].color = Color.white;
				}
			}
		}
	}
}
