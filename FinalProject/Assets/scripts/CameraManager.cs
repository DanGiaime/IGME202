using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    Camera[] cameras;
    int index;

	// Use this for initialization
	void Start () {
        cameras = gameObject.GetComponentsInChildren<Camera>();
        index = 0;
        DisableAllExceptCurrent();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("r")) {
            index++;
            index %= cameras.Length;
            DisableAllExceptCurrent();
        }
	}

    void DisableAllExceptCurrent() {

        cameras[index].enabled = true;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (i != index)
            {
                cameras[i].enabled = false;
            }
        }
    }
}
