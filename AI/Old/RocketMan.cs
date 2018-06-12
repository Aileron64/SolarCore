using UnityEngine;
using System.Collections;

public class RocketMan : BaseEnemy
{
    public Transform shootPoint;
    public GameObject laser;

    float yEuler;
    bool shootFlag;

    override protected void Start()
    {
        rotationSpeed = 8;

        base.Start();
    }

    protected override void OnBeat()
    {
        if (shootFlag)
        {
            Shoot();
        }
        else
        {
            yEuler = (Mathf.Atan2(transform.position.x - target.transform.position.x,
                transform.position.z - target.transform.position.z) * Mathf.Rad2Deg) - 135;

            targetRotation = Quaternion.Euler(0, yEuler, 0);
        }

        shootFlag = !shootFlag;
    }

    void Shoot()
    {
        for (int i = 0; i < 5; i++)
        {               
            GameObject clone = Instantiate(laser, shootPoint.transform.position, transform.rotation) as GameObject;
            shootPoint.transform.localRotation = Quaternion.Euler(0, Random.Range(85, 185), 0);
            clone.GetComponent<Laser>().SetVelocity(shootPoint.transform.forward * Random.Range(300, 500));
        }

        Vector3 dir = (transform.position - shootPoint.position).normalized;
        dir.y = 0;

        shootPoint.GetComponent<ParticleSystem>().Play();

        rB.AddForce(dir * 60000);
    }


}
