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

	void AABB() {
		for (int i = 0; i < sprites.Count; i++) {
			for (int j = i + 1; j < sprites.Count; j++) {
				bool collided = true;
				Vector3 center1 = sprites [i].bounds.center;
				Vector3 center2 = sprites [j].bounds.center;
				Vector3 minA = sprites [i].bounds.min;
				Vector3 minB = sprites [j].bounds.min;
				Vector3 maxA = sprites [i].bounds.max;
				Vector3 maxB = sprites [j].bounds.max;
				collided = collided && (minB.x < maxA.x);
				collided = collided && (maxB.x > minA.x);
				collided = collided && (maxB.y > minA.y);
				collided = collided && (minB.y < maxA.y);
				if (collided) {
					if (sprites [i] != player) {
						sprites [i].color = Color.red;
					}

					if (sprites [j] != player) {
						sprites [j].color = Color.red;
					}
					if (sprites [i] == player || sprites [j] == player) {
						lifeScript.DecrementLife ();
					}
					break;	
				} else {
					sprites [i].color = Color.white;

				}
			}
		}
			
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
