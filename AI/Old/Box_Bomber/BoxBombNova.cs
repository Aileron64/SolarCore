using UnityEngine;
using System.Collections;

public class BoxBombNova : BaseAttack
{
    float expandSpeed;

    float size;
    float timer;

    LineRenderer circle;

    float lineWidth;

    protected override void Start()
    {
        lifeTime = 2;
        expandSpeed = 3000;

        lineWidth = 100;

        circle = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {

        size += Time.deltaTime * expandSpeed;

        transform.localScale = new Vector3(size, size, 0);
        DrawCircle((size / 2) - lineWidth, circle);

        base.Update();
    }

    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.1f;             //Set lower to add more points
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; //Total number of points in circle.

        line.startWidth = lineWidth;
        line.endWidth = line.startWidth;
        line.positionCount = size;

        float x = 0;
        float z = 0;

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
