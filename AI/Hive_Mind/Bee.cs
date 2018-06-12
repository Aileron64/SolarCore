using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Bee : BaseEnemy
{
    float oribitRange;
    float yEuler;

    HiveMind boss;

    const float SLIDE_FORCE = 20000;

    override protected void Start()
    {
        speed = 100;
        rotationSpeed = 5;

        boss = Object.FindObjectOfType<HiveMind>();

        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;

        base.OnSpawn();
    }

    protected override void OnEnable()
    {
        oribitRange = (new Vector3(1000, 0, 1000)
            - new Vector3(transform.position.x, 0, transform.position.z)).magnitude * 2;

        base.OnEnable();
    }

    override protected void Normal()
    {
        //Oribit(boss.transform, oribitRange, speed, true);
    }


    protected override void OnBeat()
    {
        yEuler -= 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);


        Vector3 orbit = boss.transform.position - transform.position;
        orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
                                      orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));

        orbit = Vector3.Normalize(orbit) * oribitRange;
        orbit = boss.transform.position + orbit;

        rB.AddForce(Vector3.Normalize(orbit - transform.position) * SLIDE_FORCE);
    }

}
