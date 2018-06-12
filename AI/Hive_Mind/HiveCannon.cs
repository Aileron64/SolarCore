using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HiveCannon : BaseEnemy
{
    //LineRenderer laserBeam;
    BoxCollider laserCollider;
    Vector3 laserPoint;
    float laserLength;

    Vector3 laserStart;
    Vector3 laserEnd;

    // default position = + - 20, 2, + - 20

    float damage = 500;

    float laserWidth = 0;
    float maxWidth = 100;

    public GameObject laser;

    bool active = false;
    bool laserActive = false;

    Vector3 targetRot;
    Vector3 rotation;
    float rotSpeed = 10;

    const float EXPLOSION_FORCE = 5000;
    HiveMind boss;

    float beatTime;

    protected override void Start()
    {
        stunImmune = true;
        timeImmune = true;

        rotation = transform.rotation.eulerAngles;

        laserPoint = laser.transform.position;
        laserCollider = laser.GetComponent<BoxCollider>();
        laserStart = laser.transform.localPosition;


        //ActivateLaser();

        beatTime = BaseLevel.Instance.GetBeatTime();
        base.Start();
        boss = Object.FindObjectOfType<HiveMind>();
        explosion.GetComponent<EnemyExplosion>().meshSize = 6;
    }

    protected override void OnBeat()
    {
        if(active)
        {
            laser.transform.DOScaleX(28, 0.1f).SetDelay(beatTime - 0.1f);
            laser.transform.DOScaleX(25, 0.1f);
        }
    }

    public void ActivateLaser()
    {
        laser.SetActive(true);
        laser.transform.DOScaleX(25, 1.6f).OnComplete(LaserActive);
    }

    void LaserActive()
    {
        active = true;
        laser.GetComponent<BoxCollider>().enabled = true;
    }


    public override void Explode(bool reward)
    {
        if (boss && reward)
            boss.TakeDamage(maxHealth * 0.5f, boss.transform.position);

        base.Explode(reward);
    }
}
