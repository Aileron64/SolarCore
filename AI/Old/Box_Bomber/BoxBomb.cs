using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBomb : BaseEnemy
{
    const float SLIDE_FORCE = 20000;
    float yEuler;

    public GameObject nova;

    override protected void Start()
    {
        rotationSpeed = 8;
        base.Start();
    }


    protected override void OnBeat()
    {
        if(target)
        {
            if ((target.transform.position - transform.position).magnitude >= 300)
            {
                rB.AddForce((target.transform.position - transform.position).normalized * SLIDE_FORCE);

                yEuler += 45;
                targetRotation = Quaternion.Euler(0, yEuler, 0);
            }
            else
            {
                GameObject clone = Instantiate(nova, transform.position, transform.rotation) as GameObject;
                size = 2;
                Explode(false);
            }
        }
    }

    protected override void FindTarget()
    {
        if(Object.FindObjectOfType<BoxBomber>().gameObject)
            target = Object.FindObjectOfType<BoxBomber>().gameObject;
    }
}
