using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HWing : BaseEnemy  
{
    public GameObject gun;

    GameObject shootPoint;
    Vector3 orbit;

    float orbitDistance;
    float shootSpeed = 350;
    float shootDelay = 5;
    float shootTimer;
    //float rotSpeed = 3;

    float oribitRange;
    float yEuler;

    bool shootFlag;

    GameObject core;
    GameObject clone;

    override protected void Awake()
    {
        base.Awake();

        ObjectPool.Instance.laserCount += 30;

        rotationSpeed = 10;
        speed = 50;
      
        shootPoint = transform.Find("Shoot Point 1").gameObject;
        core = GameObject.FindWithTag("Core");      
    }

    public override void OnSpawn()
    {
        yEuler = (Mathf.Atan2(transform.position.x - 1000,
            transform.position.z - 1000) * Mathf.Rad2Deg) - 135;

        transform.rotation = Quaternion.Euler(0, yEuler, 0);
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        oribitRange = (new Vector3(1000, 0, 1000)
            - new Vector3(transform.position.x, 0, transform.position.z)).magnitude * 2;

        shootFlag = false;

        base.OnSpawn();
    }

    override protected void Normal()
    {
        Oribit(core.transform, oribitRange, speed, false);
    }

    protected override void OnBeat()
    { 
        if (shootFlag)
        {
            Shoot(0);
            gun.transform.DOLocalMove(new Vector3(2, 0, -2), 0.2f);
        }
        else
        {
            gun.transform.DOLocalMove(new Vector3(-1, 0, 1), 0.4f);
            yEuler = (Mathf.Atan2(transform.position.x - target.transform.position.x,
                transform.position.z - target.transform.position.z) * Mathf.Rad2Deg) - 135;
            targetRotation = Quaternion.Euler(0, yEuler, 0);
        }

        shootFlag = !shootFlag;
    }

    void Shoot(int num)
    {
        clone = ObjectPool.Instance.GetLaser(shootPoint.transform.position);
        clone.GetComponent<RedLaser>().SetSize(60);
        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint.transform.forward * shootSpeed);

        shootPoint.GetComponent<ParticleSystem>().Play();   
    }

    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        gun.GetComponent<Renderer>().material = mat;
        
    }
}
