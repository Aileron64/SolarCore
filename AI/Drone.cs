using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drone : BaseEnemy
{
    const float SLIDE_FORCE = 50000;
    float yEuler;

    float xDistance;
    float zDistance;

    override protected void Start()
    {
        rotationSpeed = 8;
        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;

        base.OnSpawn();
    }

    protected override void OnBeat()
    {
        if(target)
        {
            xDistance = transform.position.x - target.transform.position.x;
            zDistance = transform.position.z - target.transform.position.z;

            if (Mathf.Abs(xDistance) >= Mathf.Abs(zDistance))
            {
                if (xDistance >= 0)
                {
                    SlideDown();
                }
                else
                {
                    SlideUp();
                }
            }
            else
            {
                if (zDistance >= 0)
                {
                    SlideLeft();
                }
                else
                {
                    SlideRight();
                }
            }
        }

        yEuler += 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
    }

    void SlideUp()
    {
        if (RaycastCheck(new Vector3(SLIDE_FORCE, 0, 0)))
            rB.AddForce(new Vector3(SLIDE_FORCE, 0, 0));
        else
            SlideLeftOrRight();
    }

    void SlideDown()
    {
        if (RaycastCheck(new Vector3(-SLIDE_FORCE, 0, 0)))
            rB.AddForce(new Vector3(-SLIDE_FORCE, 0, 0));
        else
            SlideLeftOrRight();
    }

    void SlideRight()
    {
        if (RaycastCheck(new Vector3(0, 0, SLIDE_FORCE)))
            rB.AddForce(new Vector3(0, 0, SLIDE_FORCE));
        else
            SlideUpOrDown();
    }

    void SlideLeft()
    {
        if (RaycastCheck(new Vector3(0, 0, -SLIDE_FORCE)))
            rB.AddForce(new Vector3(0, 0, -SLIDE_FORCE));
        else
            SlideUpOrDown();
    }

    void SlideUpOrDown()
    {
        if (xDistance >= 0)
        {
            rB.AddForce(new Vector3(-SLIDE_FORCE, 0, 0));
        }
        else
        {
            rB.AddForce(new Vector3(SLIDE_FORCE, 0, 0));
        }
    }

    void SlideLeftOrRight()
    {
        if (zDistance >= 0)
        {
            rB.AddForce(new Vector3(0, 0, -SLIDE_FORCE));
        }
        else
        {
            rB.AddForce(new Vector3(0, 0, SLIDE_FORCE));
        }
    }

    bool RaycastCheck(Vector3 raycast)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, raycast, out hit, 100.0f))
        {
            //TextManager.Instance.DebugText("" + hit.collider.tag);

            if (hit.collider.tag == "Block")
                return false;
        }

        return true;
    }
}