using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TriShot : BaseEnemy
{
    public GameObject[] shootPoint;

    float shootSpeed = 200;
    float yEuler;

    GameObject clone;

    override protected void Awake()
    {
        base.Awake();

        speed = 100;
        rotationSpeed = 5;

        ObjectPool.Instance.laserCount += 25;
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        yEuler = (Mathf.Atan2(transform.position.x - 1000,
            transform.position.z - 1000) * Mathf.Rad2Deg) - 135;

        transform.rotation = Quaternion.Euler(0, yEuler, 0);
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        base.OnSpawn();
    }

    protected override void OnBeat()
    {
        yEuler += 25;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        Invoke("Shoot", 0.05f);
    }


    void Shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            clone = ObjectPool.Instance.GetLaser(shootPoint[i].transform.position);
            clone.GetComponent<RedLaser>().SetSize(30);
            clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootSpeed);

            shootPoint[i].GetComponent<ParticleSystem>().Play();
        }

        //iTween.PunchPosition(gameObject, transform.forward * -10, 0.2f);
    }
}