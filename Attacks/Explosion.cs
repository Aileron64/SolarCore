using UnityEngine;
using System.Collections;

public class Explosion : BaseAttack 
{
    public float maxSize;
    public float expandSpeed;
    public float stunDuration;

    float size;
    float timer;

    SphereCollider col;



    protected override void Awake()
    {
        col = GetComponent<SphereCollider>();
        base.Awake();
    }

    protected override void Update()
    {
        timer += Time.deltaTime;

        if (size + Time.deltaTime * expandSpeed < maxSize)
        {
            size += Time.deltaTime * expandSpeed;
            //transform.localScale = new Vector3(size, size, size);

            col.radius = size / 2;
        }
        else
        {
            size = maxSize;
            col.radius = size / 2;
        }

        base.Update();
    }

}
