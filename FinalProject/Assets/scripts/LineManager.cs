using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

    static public Material lineMaterial;
    public Material defaultLineMaterial;
    private static bool shouldDraw = true;

	// Use this for initialization
	void Start () {
        lineMaterial = defaultLineMaterial;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("d")) {
            shouldDraw = !shouldDraw;
        }
	}

    // Draws a line in classic fashion, must be called in OnRenderObject
    public static void DrawLine(Vector3 a, Vector3 b) // Examples of drawing lines – yours might be more complex!
    {
        if (shouldDraw)
        {
            // Set the material to be used for the first line
            lineMaterial.SetPass(0);
            // Draws one line
            GL.Begin(GL.LINES); // Begin to draw lines
            GL.Vertex(a); // First endpoint of this line
            GL.Vertex(b); // Second endpoint of this line
            GL.End();
        }
    }
}
