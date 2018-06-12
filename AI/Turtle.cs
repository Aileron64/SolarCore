using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Turtle : BaseEnemy
{
    //public GameObject laser;

    public GameObject[] shell;
    public Transform[] shootPoint;

    Vector3[] shellPoint1 = new Vector3[4];

    const int EXPAND = 13;

    Vector3[] shellPoint2 = {
        new Vector3(EXPAND, -5, EXPAND),
        new Vector3(EXPAND, -5, -EXPAND),
        new Vector3(-EXPAND, -5, -EXPAND),
        new Vector3(-EXPAND, -5, EXPAND), };

    float yEuler;
    bool shootFlag;
    float shootSpeed = 300;

    GameObject clone;

    override protected void Awake()
    {
        base.Awake();

        rotationSpeed = 5;

        for (int i = 0; i < 4; i++)
        {
            shellPoint1[i] = shell[i].transform.localPosition;
        }

        ObjectPool.Instance.laserCount += 50;
    }

    public override void OnSpawn()
    {
        yEuler = 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
        transform.rotation = targetRotation;

        shootFlag = false;


        base.OnSpawn();
    }

    override protected void OnBeat()
    {
        if (shootFlag)
        {
            ShootLasers();
        }
        else
        {
            yEuler += 45;
            targetRotation = Quaternion.Euler(0, yEuler, 0);

            for (int i = 0; i < shell.Length; i++)
            {
                shell[i].transform.DOLocalMove(shellPoint1[i], 0.2f);
            }
        }

        shootFlag = !shootFlag;
    }

    void ShootLasers()
    {
        for (int i = 0; i < shootPoint.Length; i++)
        {
            shell[i].transform.DOLocalMove(shellPoint2[i], 0.2f);

            clone = ObjectPool.Instance.GetLaser(shootPoint[i].transform.position);
            clone.GetComponent<RedLaser>().SetSize(80);
            clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootSpeed);

            shootPoint[i].GetComponent<ParticleSystem>().Play();
        }
    }

    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        for (int i = 0; i < shell.Length; i++)
        {
            shell[i].GetComponent<Renderer>().material = mat;
        }
    }
}
