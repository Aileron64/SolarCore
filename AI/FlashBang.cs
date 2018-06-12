using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FlashBang : BaseEnemy
{
    //const float JUMP_DISTANCE = 120;
    const float JUMP_ANGLE = 3;
    float yEuler;

    float xDistance;
    float zDistance;

    public GameObject flashBlast;

    float oribitRange;

    Vector3 targetPos;

    enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    Direction direction;

    int count = 0;

    override protected void Start()
    {
        rotationSpeed = 12;

        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
        transform.rotation = Quaternion.identity;

        oribitRange = (new Vector3(1000, 0, 1000)
            - new Vector3(transform.position.x, 0, transform.position.z)).magnitude;

        base.OnSpawn();

        targetPos = new Vector3(transform.position.x, restHeight, transform.position.z);
    }


    protected override void OnBeat()
    {
        rB.isKinematic = true;
        transform.DOMove(targetPos, 0.1f).OnComplete(JumpEnd);

        yEuler += 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
        transform.rotation = Quaternion.Euler(0, yEuler, 0);


        //yEuler = (Mathf.Atan2(transform.position.x - targetPos.x,
        //    transform.position.z - targetPos.z) * Mathf.Rad2Deg) - 135;

        //targetRotation = Quaternion.Euler(0, yEuler, 0);


        targetPos = GetTargetPos();



        GameObject clone = Instantiate(flashBlast, targetPos, Quaternion.identity) as GameObject;
    }

    void JumpEnd()
    {
        rB.isKinematic = false;
    }

    Vector3 GetTargetPos()
    {
        Vector3 orbit = new Vector3(1000, 0, 1000) - targetPos;


        orbit = new Vector3(orbit.x * Mathf.Cos(JUMP_ANGLE) - orbit.z * Mathf.Sin(JUMP_ANGLE), 0,
            orbit.x * Mathf.Sin(JUMP_ANGLE) + orbit.z * Mathf.Cos(JUMP_ANGLE));

        orbit = Vector3.Normalize(orbit) * oribitRange;
        orbit = new Vector3(1000, 0, 1000) + orbit;

        return orbit;
    }

    //protected void Oribit(Transform oribitTar, float distance, float speed, bool clockWise)
    //{
    //    Vector3 orbit = oribitTar.position - transform.position;

    //    if (clockWise)
    //        orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
    //                                  orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));
    //    else
    //        orbit = new Vector3(orbit.x * Mathf.Cos(-90) - orbit.z * Mathf.Sin(-90), 0,
    //                      orbit.x * Mathf.Sin(-90) + orbit.z * Mathf.Cos(-90));

    //    orbit = Vector3.Normalize(orbit) * distance;
    //    orbit = oribitTar.position + orbit;

    //    velocity = Vector3.Normalize(orbit - transform.position) * speed;
    //}


}