using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FalsePulsor : BaseEnemy
{
    float yEuler;

    public GameObject flashBlast;

    bool flag;

    override protected void Start()
    {
        rotationSpeed = 12;

        base.Start();
    }

    protected override void OnBeat()
    {


        GameObject clone;

        if (flag)
        {
            clone = Instantiate(flashBlast, transform.position, Quaternion.identity) as GameObject;
            yEuler += 45;
            targetRotation = Quaternion.Euler(0, yEuler, 0);
            //transform.rotation = Quaternion.Euler(0, yEuler, 0);
        }



        flag = !flag;
    }

}