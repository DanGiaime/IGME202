using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerScript : MonoBehaviour {

	bool isWalking;

	// Use this for initialization
	void Start () {
		isWalking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isWalking) {
			float x = Random.Range (-0.5f, 0.5f);
			float z = Random.Range (-0.5f, 0.5f);
			gameObject.transform.Translate (x, 0, z);
		}
		if (Input.GetKey(KeyCode.M)) {
			isWalking = !isWalking;
		}
	}

	void OnGUI () {
		GUI.Button(new Rect(10, 10, 200, 50), "Press m to move");
	}
}
