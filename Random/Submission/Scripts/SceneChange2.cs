using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.C)) {
			SceneManager.LoadScene ("Scenes/Random Walker");
		}	
	}

	void OnGUI () {
		GUI.Button(new Rect(10, 100, 200, 50), "Press C to change scenes");
	}
}
