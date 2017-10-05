using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	private SpriteRenderer[] sprites;
	public SpriteRenderer player;
	private Life lifeScript;

	bool aabb;

	// Use this for initialization
	void Start () {
		sprites = gameObject.GetComponentsInChildren<SpriteRenderer> ();
		lifeScript = gameObject.GetComponent<Life> ();
		aabb = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Alpha1)) {
			aabb = true;
		}
		else if (Input.GetKey (KeyCode.Alpha2)) {
			aabb = false;
		}

		if (aabb) {
			AABB ();
		} else {
			BoundingCircle ();
		}
	}

	void AABB() {
		for (int i = 0; i < sprites.Length; i++) {
			for (int j = i + 1; j < sprites.Length; j++) {
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
					if (sprites[i] == player || sprites[j] == player) {
						lifeScript.DecrementLife ();
					}
						
				} else {
					if (sprites [i] != player) {
						sprites [i].color = Color.white;
					}
					if (sprites [j] != player) {
						sprites [j].color = Color.white;
					}
				}
			}
		}
	}

	void BoundingCircle() {
		for (int i = 0; i < sprites.Length; i++) {
			for (int j = i + 1; j < sprites.Length; j++) {
				bool collided = true;
				Vector3 center1 = sprites [i].bounds.center;
				Vector3 center2 = sprites [j].bounds.center;
				float radius1 = sprites [i].bounds.extents.x;
				float radius2 = sprites [j].bounds.extents.x;
				if (Vector3.Distance (center1, center2) < radius1 + radius2) {
					sprites [i].color = Color.red;
					sprites [j].color = Color.red;
				}
			}
		}
	}
}
