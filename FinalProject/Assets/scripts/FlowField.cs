using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour {

    public Terrain terrain;
    public float width;
    public float height;
    public Vector3[,] flowField;
    public int size = 20;
    public int unitWidth = 50;

	// Use this for initialization
    void Start () {
        flowField = new Vector3[size,size];
        this.width = unitWidth * size;
        this.height = unitWidth * size;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {

                // Rotate a unit vector to get a random unit vector
                flowField[i, j] = Quaternion.Euler(0, 360f * Mathf.PerlinNoise(i * .02f, j * .02f), 0) * Vector3.forward * 10f;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        
	}

    private void OnRenderObject()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 position = new Vector3(i * width / size, 0, j * height / size);
                LineManager.DrawLine(transform.position + position, transform.position + position + flowField[i, j]);
            }
        }
    }
}
