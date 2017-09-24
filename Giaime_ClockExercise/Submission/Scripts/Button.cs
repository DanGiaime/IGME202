using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject hand;
    private RotateHand handScript;

    // Use this for initialization
    void Start()
    {
        handScript = hand.GetComponent<RotateHand>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        handScript.shouldRotate = !handScript.shouldRotate;
    }
}
