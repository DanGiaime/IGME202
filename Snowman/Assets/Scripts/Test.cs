using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject top;
    public GameObject middle;
    public GameObject bottom;
    public GameObject sceneManager;
    private Count countingScript;

    // Use this for initialization
    void Start() {
        sceneManager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown()
    {
        top.AddComponent<Rigidbody>();
        middle.AddComponent<Rigidbody>();
        bottom.AddComponent<Rigidbody>();
        Debug.Log("clicked");
        countingScript = sceneManager.GetComponent<Count>();
        countingScript.snowmanCounter++;
        Destroy(gameObject.GetComponent<Collider>());
    }
}
