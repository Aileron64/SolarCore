using UnityEngine;
using System.Collections;

public class Phycho : BaseEnemy
{
    const float SLIDE_FORCE = 1000000;
    float yEuler = 45;

    override protected void Start()
    {
        rotationSpeed = 15;

        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 45;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
        transform.rotation = targetRotation;

        base.OnSpawn();
    }

    protected override void OnBeat()
    {
        int rand = Random.Range(0, 8);

        switch (rand)
        {
            default:
                float xDistance = transform.position.x - target.transform.position.x;
                float zDistance = transform.position.z - target.transform.position.z;

                if (Mathf.Abs(xDistance) >= Mathf.Abs(zDistance))
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
                else
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
                break;

            case 0:
                rB.AddForce(new Vector3(-SLIDE_FORCE, 0, 0));
                break;

            case 1:
                rB.AddForce(new Vector3(SLIDE_FORCE, 0, 0));
                break;

            case 2:
                rB.AddForce(new Vector3(0, 0, SLIDE_FORCE));
                break;

            case 3:
                rB.AddForce(new Vector3(0, 0, -SLIDE_FORCE));
                break;
        }



        yEuler += 90;
        targetRotation = Quaternion.Euler(0, yEuler, 0);


    }
}