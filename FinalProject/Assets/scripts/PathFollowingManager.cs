using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowingManager : MonoBehaviour {

    public Material lineMaterial;

    public Transform[] path;

    // Waypoints are children of this object
    void Awake () {
        path = gameObject.GetComponentsInChildren<Transform>();
	}

	// Update is called once per frame
	void Update () {
    }

    private void OnRenderObject()
    {
        for (int i = 0; i < path.Length - 1; i++)
        {
            LineManager.DrawLine(path[i].position, path[i + 1].position);
        }
        LineManager.DrawLine(path[0].position, path[path.Length - 1].position);
    }

   
}
