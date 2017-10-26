using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Forces : MonoBehaviour {

	Vehicle[] children;
	public GameObject smallPrefab;
	public GameObject mediumPrefab;
	public GameObject largePrefab;

    private GameObject moving;
    private GameObject still;

	const float MAX_X = 5f;
	const float MIN_X = -5f;
	const float MAX_Y = 5f;
	const float MIN_Y = -5f;
	bool flee = true;

	// Use this for initialization
	void Start () {
		this.moving = Instantiate (smallPrefab, randomPosition(), Quaternion.identity, transform);
        this.moving.transform.Rotate(0,0,Random.Range(0f, 6.28f));
        this.moving.GetComponentInChildren<Vehicle>().Mass = 1f;
        this.moving.transform.localScale = new Vector3(1, 1, 1);
        this.still = Instantiate (mediumPrefab, randomPosition(), Quaternion.identity, transform);
        this.still.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
        this.still.GetComponentInChildren<Vehicle>().Mass = 5f;
        this.still.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

		children = this.GetComponentsInChildren<Vehicle> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flee = true;
            Debug.Log("Fleeing");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            flee = false;
            Debug.Log("Steering");
        }

        if(flee)
        {
            this.moving.GetComponent<Agent>().Flee(still);
        }
        else
        {
            this.moving.GetComponent<Agent>().Seek(still);
        }

        foreach (Vehicle v in children) {
			v.Bounce ();
		}

        if(Vector2.Distance(moving.transform.position, still.transform.position) < 2f)
        {
            still.GetComponent < Vehicle>().position = randomPosition();
        }
        
	}

	Vector2 randomPosition() {
		return new Vector2 (Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y));
	}
}
