using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockNumbers : MonoBehaviour {

	public GameObject prefab;
	private float radius = 2.2f;

	// Use this for initialization
	void Start () {
		Vector2 parentPos = transform.position;
		for (int i = 1; i <= 12; i++) {
			float angle = (3 - i) * (360.0f / 12.0f) * Mathf.Deg2Rad;
			Vector2 offset = new Vector2 (radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
			GameObject number = Instantiate (prefab, parentPos + offset, Quaternion.identity);
			TextMesh text = number.GetComponentInChildren<TextMesh> ();
			text.text = i + "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
