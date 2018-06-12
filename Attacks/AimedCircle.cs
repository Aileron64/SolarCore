using UnityEngine;
using System.Collections;

public class AimedCircle : BaseAttack
{
    float size = 25;
    float maxSize = 600;

    bool targeting = true;

    public GameObject explosion;

    LineRenderer circle;

    public Yellow owner;

    protected override void Awake()
    {
        base.Awake();

        circle = GetComponent<LineRenderer>();
    }

    protected override void FixedUpdate()
    {
        if (targeting)
        {
            if (size <= maxSize)
            {
                size += Time.deltaTime * 500;
            }
            else
            {
                targeting = false;
                owner.AimedBlast();
            }
                

            transform.localScale = new Vector3(size, size, 0);

            DrawCircle(size / 2, circle);

            base.FixedUpdate();
        }
    }

    public void EndTargeting()
    {
        targeting = false;
    }

    public void Detonate()
    {
        GameObject clone = Instantiate(explosion, transform.position, transform.rotation) as GameObject;

        clone.GetComponent<Explosion>().maxSize = size;
        clone.GetComponent<ParticleSystem>().startSize = size * 0.7f;
        //        explosion.startSize = size * 80;

        OnEnd();
    }

    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.1f;             //Set lower to add more points
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; //Total number of points in circle.

        //line.startWidth = lineWidth;
        //line.endWidth = line.startWidth;
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
