using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushForce : MonoBehaviour
{
    float expandSpeed;
    float maxSize;

    float size;

    public float power;
    public Rigidbody GetRigidBody() { return GetComponent<Rigidbody>(); }

    void Start()
    {      
        maxSize = 1000;
        expandSpeed = 400;
    }

    void OnEnable()
    {
        size = 100;
        power = 200;
        transform.localScale = new Vector3(size, 1, size);
    }

    void Update()
    {
        power -= Time.deltaTime * 300;

        if (maxSize == 0 || size < maxSize)
            size += Time.deltaTime * expandSpeed;
        else
            size = maxSize;

        transform.localScale = new Vector3(size, 1, size);
    }

    //void OnDisable()
    //{
    //    Debug.Log(power);
    //}
}
