using UnityEngine;
using System.Collections;

public class SolarDebris : BaseEnemy
{
    //GameObject redSun;

    Vector3 redSunPosition;
    Vector3 orbit;

    float orbitDistance;

    override protected void Start () 
    {
        base.Start();

        redSunPosition = new Vector3(1100, 0, 1000);

        //health = 400;
        //impactDamage = 20;
        //expAmount = 15;
        //expSpeed = 2000;

        orbitDistance = Random.Range(2000, 5000);
        speed = Random.Range(200, 400);

        transform.rotation = Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
    }

    override protected void Normal()
    {
        orbit = redSunPosition - transform.position;
        orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
                                        orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));
        orbit = Vector3.Normalize(orbit) * orbitDistance;
        orbit = redSunPosition + orbit;

        velocity = Vector3.Normalize(orbit - transform.position) * speed;
    }
}
