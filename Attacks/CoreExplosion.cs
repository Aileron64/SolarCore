using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreExplosion : MonoBehaviour
{
    public float setStun;


    float size;

    LineRenderer circle;

    float lineWidth = 500;

    float novaCharge;
    public void SetCharger(float _charge) { novaCharge = _charge; }

    void Start()
    {
        size = 0;
        circle = GetComponent<LineRenderer>();
    }

    void Update()
    {

        size += Time.deltaTime * 8000;
        transform.localScale = new Vector3(size, size, size);

        DrawCircle((size / 2), circle);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log(col.gameObject.name);

            col.gameObject.GetComponent<BaseEnemy>().Explode(false);
        }     
    }


    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.1f;             
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; 

        line.startWidth = lineWidth;
        line.endWidth = line.startWidth;

        //line.SetVertexCount(size);

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
