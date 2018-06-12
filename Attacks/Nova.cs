using UnityEngine;
using System.Collections;

public class Nova : BaseAttack 
{

    public bool scaleWithSize;
    public float expandSpeed;
    public float maxSize;

    float size;
    float timer;
    float maxDamage;

    bool followOwner;
    Transform owner;
    LineRenderer circle;

    public float lineWidth;

    float novaCharge;
    public void SetCharger(float _charge) { novaCharge = _charge; }

    protected override void Awake() 
    {
        base.Awake();

        maxDamage = damage;
        circle = GetComponent<LineRenderer>();      
    }

    protected override void OnEnable()
    {
        size = 0;
        DrawCircle((size / 2) - lineWidth, circle);
        transform.localScale = new Vector3(size, size, 0);
        base.OnEnable();
    }


    public void FollowOwner(Transform _owner)
    {
        followOwner = true;
        owner = _owner;
    }

    protected override void Update() 
    {
        timer += Time.deltaTime;

        if(scaleWithSize)
        {
            damage = maxDamage * ((lifeTime - timer) / lifeTime);
        }

        if(followOwner)
        {
            transform.position = owner.position;
        }

        if (maxSize == 0 || size < maxSize)
            size += Time.deltaTime * expandSpeed;
        else
            size = maxSize;

        transform.localScale = new Vector3(size, size, 0);
        DrawCircle((size / 2) - lineWidth, circle);

        base.Update();
	}


    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.1f;             //Set lower to add more points
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; //Total number of points in circle.

        //circle.material = new Material(Shader.Find("Particles/Additive"));
        //circle.SetColors(c1, c2);


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
