using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Wasp : BaseEnemy
{
    float oribitRange;
    float yEuler;

    public GameObject smallestBlob;
    HiveMind boss;

    const float SLIDE_FORCE = 80000;
    const float CHASE_DISTANCE = 1000;
    bool slideFlag;

    Transform core;

    float coreDistance;
    float playerDistance;

    override protected void Start()
    {
        speed = 250;
        rotationSpeed = 5;

        boss = Object.FindObjectOfType<HiveMind>();
        core = GameObject.FindWithTag("Core").transform;

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


        coreDistance = (new Vector3(1000, 0, 1000)
            - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;

        playerDistance = (new Vector3(1000, 0, 1000)
            - new Vector3(target.transform.position.x, 0, target.transform.position.z)).magnitude;


        rB.AddForce(GetAngle(coreDistance >= playerDistance + 100) * SLIDE_FORCE);


    }

    protected Vector3 GetAngle(bool flag)
    {
        Vector3 orbit = core.position - transform.position;
        orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
                                    orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));



        if(flag)
            orbit = Vector3.Normalize(orbit) * (coreDistance * 2 - CHASE_DISTANCE);
        else
            orbit = Vector3.Normalize(orbit) * (coreDistance * 2 + CHASE_DISTANCE);

        orbit = core.position + orbit;

        return Vector3.Normalize(orbit - transform.position);
    }
}
