using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour 
{

    public GameObject gui;

    float z;


    Camera cam;

    void Start()
    {
        //gui.transform.parent = transform.parent;
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        z = 1000 - ((cam.fieldOfView - 64) * 12);

        gui.transform.localPosition = new Vector3(gui.transform.localPosition.x, gui.transform.localPosition.y, z);
    }
}
