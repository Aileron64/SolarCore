using UnityEngine;
using System.Collections;

public class AoECircle : BaseAttack 
{
    float size;
    float timer;

    public float mainSize;
    public float expandSpeed;


    protected override void Update()
    {
        if(size <= mainSize)
        {
            size += Time.deltaTime * expandSpeed;
        }
        
        transform.localScale = new Vector3(size, size, 0);

        base.Update();
    }
}
