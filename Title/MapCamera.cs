using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MapCamera : MonoBehaviour
{

    float speed = 5;

    Vector3 velocity;
    Vector3 targetPosition;
    Vector3 offset;

    float offsetScale = 0.6f;
    public void SetOffSetScale(float _scale) { offsetScale = _scale; }

    public GameObject back1;
    Material background1;
    Vector2 backgroundOffset1;
    Vector2 backgroundScale1;

    Camera cam;

    public GameObject target;



    void Start()
    {
        cam = Camera.main;
        offset = new Vector3(-220, 600, 0);

        background1 = back1.GetComponent<Renderer>().material;

        backgroundScale1 = new Vector2(1, 1);
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;

        //Debug.Log(_target.name);
    }

    void FixedUpdate()
    {
        velocity = targetPosition - transform.position;
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;

    }

    void LateUpdate()
    {



        targetPosition = target.transform.position;
        targetPosition += offset * offsetScale;

        

        //backgroundOffset1.Set(transform.position.z * -0.000016f, transform.position.x * 0.00009f);


        background1.SetTextureOffset("_MainTex", backgroundOffset1);



        //Mousewheel test

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            backgroundScale1.x += 0.01f;
            backgroundScale1.y += 0.01f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            backgroundScale1.x -= 0.01f;
            backgroundScale1.y -= 0.01f;
        }

        background1.SetTextureScale("_MainTex", backgroundScale1);
        
    }



}
