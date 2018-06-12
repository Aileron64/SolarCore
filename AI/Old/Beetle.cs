using UnityEngine;
using System.Collections;

public class Beetle : BaseEnemy
{
    float orbitDistance = 1000;

    enum State
    {
        MOVE, SHOOT
    }

    State state = State.MOVE;

    const float SLIDE_FORCE = 25000;
    float rotSpeed = 3;

    public GameObject laser;

    float shootSpeed = 300;
    float shootDelay = 0.5f;
    float shootTimer;

    public GameObject shootPoint;
    public GameObject[] forcePoint;

    bool slideLeft = true;

    float maxSpeed = 500;
    bool shootFlag;

    override protected void Start()
    {
        base.Start();
    }

    override protected void Normal()
    {
        switch (state)
        {
            default:
            case State.MOVE:
                FaceTarget(Vector3.zero, rotSpeed);
                break;

            case State.SHOOT:
                break;

        }
    }

    protected override void OnBeat()
    {
        if (shootFlag)
        {
            Shoot();
            state = State.SHOOT;
        }
        else
        {
            Move();
            state = State.MOVE;
        }

        shootFlag = !shootFlag;
    }


    void Shoot()
    {
        GameObject clone = Instantiate(laser, shootPoint.transform.position, transform.rotation) as GameObject;
        clone.GetComponent<Laser>().SetVelocity(shootPoint.transform.forward * shootSpeed);     
    }

    void Move()
    {
        if (slideLeft)
        {
            rB.AddForce(forcePoint[0].transform.forward * SLIDE_FORCE);
        }
        else
        {
            rB.AddForce(forcePoint[1].transform.forward * SLIDE_FORCE);
        }

        slideLeft = !slideLeft;
    }

}