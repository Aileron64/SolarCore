using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleBomb : MonoBehaviour
{
    LineRenderer circle;

    public Material lineMat;
    public float lineWidth;

    public GameObject explosion;

    Vector3[] points = new Vector3[4];

    float radius = 1;
    public float maxSize = 120;
    private object lines;

    float beatTime;
    float timer;

    bool isArmed = false;

    void Awake()
    {
        circle = GetComponent<LineRenderer>();
        beatTime = Object.FindObjectOfType<BaseLevel>().GetBeatTime();
    }

    void OnEnable()
    {
        timer = 0;
        radius = 1;
        isArmed = false;
        DrawCircle(radius, circle);
    }

    void FixedUpdate()
    {
        if (timer <= beatTime)
        {
            timer += Time.deltaTime;

            if (!isArmed)
            {
                radius = (timer / beatTime) * maxSize;
                DrawCircle(radius, circle);
            }

        }
        else
        {
            if (isArmed)
            {
                GameObject clone = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                //Destroy(this.gameObject);

                this.gameObject.SetActive(false);
            }
            else
            {
                timer = 0;
                isArmed = true;
            }
        }
    }


    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.2f;             //Set lower to add more points
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; //Total number of points in circle.

        //circle.material = new Material(Shader.Find("Particles/Additive"));
        //circle.SetColors(c1, c2);

        line.startWidth = 5;
        line.endWidth = line.startWidth;

        line.positionCount = size;

        float x = 0;
        float z = 0;

        //int i = 0;
        for (int i = 0; i < size; i++)
        {
            x = _radius * Mathf.Cos(i * theta_scale);
            z = _radius * Mathf.Sin(i * theta_scale);

            Vector3 pos = new Vector3(x, 0, z) + transform.position;

            line.SetPosition(i, pos);
            //i += 1;
        }
    }

}
