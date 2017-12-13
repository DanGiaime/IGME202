using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float time;
    float t;

	// Use this for initialization
	void Start () {
        t = time;
	}
	
	// Update is called once per frame
	void Update () {
		if(t > 0) {
            t -= Time.deltaTime;
            if(t <= 0) {

                Destroy(gameObject);
            }
        }
	}

    public void SetTime(float t) { this.t = t; }
}
