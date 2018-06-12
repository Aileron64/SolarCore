using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDrone : BaseEnemy
{
    float yEuler;

    const float SLIDE_FORCE = 30000;

    override protected void Start()
    {
        rotationSpeed = 8;
        speed = 100;
        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;

        base.OnSpawn();
    }

    protected override void OnBeat()
    {
        yEuler += 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        if(target)
            rB.AddForce(Vector3.Normalize(target.transform.position - transform.position) * SLIDE_FORCE);
    }

}
