using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlackHole : BaseAttack 
{
    float startSize = 300;
    float size = 1;
    //float timer;

    float sizeIncrease = 400;
    float hawkingRad = 40;
    float maxSize = 500;   
    LineRenderer line;

    bool active = false;
    float activeTimer;
    float activeTime = 1.4f;

    protected override void Awake()
    {
        base.Awake();
        line = GetComponent<LineRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        DrawCircle((size / 2) + 1, line);
        size = 50;
        active = false;
    }




    protected override void Update()
    {
        

        if(active)
        {

        }
        else
        {
            activeTimer += Time.deltaTime;

            if (activeTimer >= activeTime)
            {
                Activate();
            }
        }

        //if (active)
        //{
        //    if(!test)
        //        size -= (hawkingRad + (size * 0.1f)) * Time.deltaTime * timeDilation;

        //    if (size <= 10)
        //        OnEnd();
        //}
        //else
        //{
        //    if (size < startSize)
        //    {
        //        size += Time.deltaTime * 500;
        //    }
        //    else
        //        active = true;
        //}

        if (size >= maxSize)
            size = maxSize;

        transform.localScale = new Vector3(size, 10, size);
        DrawCircle((size / 2) + 1, line);
    }

    void Activate()
    {
        active = true;
        rB.velocity *= 0.2f;
        size += 500;
    }

    public void HawkingRad(float amount)
    {
        size -= amount;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Laser" && active)
        {
            if (col.GetComponent<BaseAttack>().atkTag == "pLaser")
            {
                col.gameObject.SetActive(false);
                size += sizeIncrease * 3 * Time.deltaTime;
            }
        }

        if (col.gameObject.tag == "Enemy")
        {
            if(col.GetComponent<BaseEnemy>())
                size += sizeIncrease * Time.deltaTime * col.GetComponent<BaseEnemy>().value;

            if (!active)
                Activate();
        }

        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
        }
    }

    void OnTriggerStay(Collider col)
    {   // Called every frame
        if (col.gameObject.tag == "Laser Beam")
        {
            if (col.GetComponent<BaseAttack>().atkTag == "SuperNova")
            {
                size -= sizeIncrease * Time.deltaTime;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 1;
        }
    }

    protected override void OnEnd()
    {
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
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
