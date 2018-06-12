using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BumbleBee : BaseEnemy
{
    GameObject[] shootPoint = new GameObject[4];


    float shootSpeed = 130;

    HiveMind boss;

    float timer;
    float oribitRange;


    float yEuler;

    bool shootFlag;

    GameObject clone;

    override protected void Awake()
    {
        base.Awake();
        ObjectPool.Instance.laserCount += 30;

        speed = 30;
        rotationSpeed = 5;

        for (int i = 0; i <= 3; i++)
        {
            shootPoint[i] = transform.Find("" + i).gameObject;
        }

        boss = Object.FindObjectOfType<HiveMind>();
        
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;
        shootFlag = false;

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
        OribitTarget(oribitRange, speed, true);
    }


    protected override void OnBeat()
    {

        if (shootFlag)
        {
            ShootCannons();
        }
        else
        {
            yEuler += 25;
            targetRotation = Quaternion.Euler(0, yEuler, 0);

            //Invoke("ShootCannons", 0.1f);
        }

        shootFlag = !shootFlag;
        

    }

    public void ShootCannons()
    {
        //GetComponent<AudioSource>().Play();
        for (int i = 0; i < 4; i++)
        {
            clone = ObjectPool.Instance.GetLaser(shootPoint[i].transform.position);
            clone.GetComponent<RedLaser>().SetSize(35);
            clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootSpeed);

            shootPoint[i].GetComponent<ParticleSystem>().Play();
        }
    }

    //public override void Explode()
    //{
    //    if (boss)
    //        boss.TakeDamage(maxHealth * 0.5f, boss.transform.position);

    //    base.Explode();
    //}

    protected override void FindTarget()
    {
        target = GameObject.FindWithTag("Core");
    }
}
