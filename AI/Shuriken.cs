using UnityEngine;
using System.Collections;

public class Shuriken : BaseEnemy
{
    LineRenderer[] line = new LineRenderer[4];
    public GameObject[] laser = new GameObject[4];
    BoxCollider[] col = new BoxCollider[4];
    Vector3[] laserEnd = new Vector3[4];
    float[] laserLength = new float[4];

    float maxLength = 500;
    float laserWidth = 2;

    float dashTimer;
    float dashTime = 1.2f;

    int laserCount = 4;


    bool laserFlag;
    const float SLIDE_FORCE = 80000;
    float yEuler;

    override protected void Start()
    {
        rotationSpeed = 8;
        
        for (int i = 0; i < 4; i++)
        {
            line[i] = laser[i].GetComponent<LineRenderer>();        
            col[i] = laser[i].GetComponent<BoxCollider>();
        }

        base.Start();
    }

    public override void OnSpawn()
    {
        yEuler = 0;
        targetRotation = Quaternion.Euler(0, yEuler, 0);

        transform.rotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            if (line[i])
            {
                line[i].startWidth = 0;
                line[i].endWidth = 0;
            }
        }

        laserFlag = true;

        base.OnSpawn();
    }

    protected override void OnDisable()
    {
        transform.rotation = Quaternion.identity;
        targetRotation = Quaternion.identity;

        yEuler = 0;

        base.OnDisable();
    }

    protected override void Normal()
    {
        // Laser Pulses
        if (laserWidth > 2)
            laserWidth -= Time.deltaTime * 100;

        else if(col[0].enabled)
        {
            for (int i = 0; i < 4; i++)
            {
                col[i].enabled = false;
            }
        }

        // Each Laser
        for (int i = 0; i < 4; i++)
        {
            bool blockDetected = false;

            RaycastHit[] hitArray = Physics.RaycastAll(laser[i].transform.position, laser[i].transform.forward, maxLength);

            foreach (RaycastHit hit in hitArray)
            {
                if (hit.collider.tag == "Block")
                {
                    blockDetected = true;
                    laserEnd[i] = hit.point;

                    laserLength[i] = (laser[i].transform.position - hit.point).magnitude;

                    break;
                }
            }

            if (!blockDetected)
            {
                laserEnd[i] = laser[i].transform.position + (laser[i].transform.forward * maxLength);
                laserLength[i] = maxLength;
            }


            laserEnd[i] = laser[i].transform.position + (laser[i].transform.forward * laserLength[i]);

            col[i].center = new Vector3(0, 0, laserLength[i] * 0.5f / 4);
            col[i].size = new Vector3(laserWidth / 4, laserWidth / 4, laserLength[i] / 4);

            line[i].SetWidth(laserWidth, laserWidth);
            line[i].SetPosition(0, laser[i].transform.position);
            line[i].SetPosition(1, laserEnd[i]);
        }
    }

    protected override void OnBeat()
    {

        if(laserFlag)
        {

            //Lasers
            laserWidth = 25;

            for (int i = 0; i < 4; i++)
            {
                col[i].enabled = true;
            }

            laserCount--;

            if (laserCount <= 0)
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    line[i].SetWidth(0, 0);
                //}

            }



        }
        else
        {

            // Moving

            if (target)
            {
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
            }

            // Rotate

            yEuler += 45;
            targetRotation = Quaternion.Euler(0, yEuler, 0);
        }


        laserFlag = !laserFlag;
    }
}
