using UnityEngine;
using System.Collections;

public class AimedBlast : BaseAttack
{
    public AimedCircle target;

    float speed = 2500;

    protected override void FixedUpdate()
    {
        velocity = (target.transform.position - transform.position).normalized * speed;

        if ((transform.position - target.transform.position).magnitude <= 25)
            OnEnd();

        base.FixedUpdate();
    }

    protected override void OnEnd()
    {
        target.Detonate();
        base.OnEnd();
    }
}
