using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;

public class CrossCannon : BaseEnemy 
{
    public GameObject[] shootPoint;
    public GameObject[] gun;

    float shootSpeed = 200;
    float oribitRange;
    float yEuler;

    bool shootFlag;

    Transform core;

    GameObject clone;

    override protected void Awake() 
    {
        base.Awake();

        ObjectPool.Instance.laserCount += 30;

        speed = 100;
        rotationSpeed = 5;

        core = GameObject.FindWithTag("Core").transform;
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;

        oribitRange = (new Vector3(1000, 0, 1000)
            - new Vector3(transform.position.x, 0, transform.position.z)).magnitude * 2;

        shootFlag = false;

        base.OnSpawn();
    }


    override protected void Normal()
    {
        Oribit(core, oribitRange, speed, false);
    }

    protected override void OnBeat()
    {
        if(shootFlag)
        {
            ShootCannons();
        }
        else
        {
            yEuler += 45;
            targetRotation = Quaternion.Euler(0, yEuler, 0);

            for (int i = 0; i < 4; i++)
            {
                gun[i].transform.DOLocalMove(gun[i].transform.localPosition.normalized * 24, 0.3f);
            }
        }

        shootFlag = !shootFlag;
    }


    public void ShootCannons()
    {
        //GetComponent<AudioSource>().Play();
        for (int i = 0; i < 4; i++)
        {
            clone = ObjectPool.Instance.GetLaser(shootPoint[i].transform.position);
            clone.GetComponent<RedLaser>().SetSize(50);
            clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootSpeed);

            gun[i].transform.DOLocalMove(gun[i].transform.localPosition.normalized * 22, 0.15f);

            shootPoint[i].GetComponent<ParticleSystem>().Play();
        }
    }

    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        for (int i = 0; i < gun.Length; i++)
        {
            gun[i].GetComponent<Renderer>().material = mat;
        }
    }

}
