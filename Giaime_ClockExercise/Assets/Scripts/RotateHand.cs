using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{

    public bool shouldRotate;

    // Use this for initialization
    void Start()
    {
        shouldRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            RotateToMouse();
        }
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouseWorldPos.x, -mouseWorldPos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
