using UnityEngine;
using System.Collections;

public class PropellerNova : BaseAttack
{
    float expandSpeed = 1500;
    LineRenderer circle;
    float lineWidth;

    public float size;
    public float maxSize;

    public Propeller owner;

    float beatTime;

    protected override void Start()
    {
        lineWidth = 10;
        circle = GetComponent<LineRenderer>();
        //beatTime = 
    }

    protected override void OnEnable()
    {
        size = 0;
        transform.localScale = new Vector3(0, 0, 0);

        if(circle)
            DrawCircle(1, circle);

        base.OnEnable();
    }

    protected override void Update()
    {
        if (size < maxSize)
            size += Time.deltaTime * expandSpeed;
        else
        {
            size = maxSize;
            Invoke("OnEnd", 0.4f);
        }
            
    
        transform.localScale = new Vector3(size, size, 0);
        DrawCircle((size / 2) - lineWidth, circle);

        if (owner)
        {
            if (size > owner.sizeLength)
            {
                owner.sizeLength = size;
                //TextManager.Instance.DebugText("" + size);
            }
                
        }

        base.Update();
    }

    //protected override void OnEnd()
    //{
    //    Destroy(this.gameObject);
    //}

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
