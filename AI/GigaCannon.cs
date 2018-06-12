using UnityEngine;
using System.Collections;

public class GigaCannon : BaseEnemy 
{
    float targetDistance;
    float spinDistance = 300;

    public GameObject[] piston = new GameObject[4];

    // -11.5

    float[] pistonStartHeight = { -13, -1, -9, -5 };
    float[] pistonTweenHeight = { -5, -9, -1, -13};


    public Transform startPoint;
    public Transform endPoint;
    
    public bool clockWise = false;

    GameObject laser;

    LineRenderer laserLine;
    bool laserActive = false;
    Vector3 laserEnd;
    
    float laserLength;
    float maxLength = 4000;

    float laserWidth;
    float maxWidth = 100;

    BoxCollider laserCollider;

    float timeDelay = 1;
    float timer;

    bool spining = false;

    Vector3 targetRot;
    float rotSpeed = 24;

    enum State
    {
        ROTATE, SPINNING_UP
    }
    State state = State.ROTATE;

	protected override void Start () 
    {
        laser = transform.Find("Laser").gameObject;
        laserLine = laser.GetComponent<LineRenderer>();
        laserCollider = laser.GetComponent<BoxCollider>();

        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //transform.rotation = Quaternion.Euler(new Vector3(0, 50, 0));

        for (int i = 0; i < 4; i++)
        {
            piston[i].transform.localPosition = new Vector3(
                piston[i].transform.localPosition.x, pistonStartHeight[i], piston[i].transform.localPosition.z);

            //Debug.Log("NEEDS FIXING");

            //iTween.MoveTo(piston[i], iTween.Hash(
            //    "y", pistonTweenHeight[i], "islocal", true,
            //    "easeType", easeType,
            //    "loopType", "pingPong",
            //    "time", BaseLevel.Instance.GetBeatTime()));
        }
    }

    public override void OnSpawn()
    {
        if ((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(1000, 0, 1000)).magnitude > 50)
        {
            transform.rotation = Quaternion.Euler(0,
                (Mathf.Atan2(transform.position.x - 1000, transform.position.z - 1000) * Mathf.Rad2Deg) + 90,
                50);
        }
        else
        {          
            transform.rotation = Quaternion.Euler(0, 0, 50);
        }

        state = State.ROTATE;

        targetRot = transform.rotation.eulerAngles;

        if (laserLine)
        {
            laserLine.startWidth = 0;
            laserLine.endWidth = 0;
            laserLine.enabled = false;
        }

        timer = 0;
        base.OnSpawn();
    }


    override protected void Normal() 
    {
        targetDistance = Vector3.Magnitude(target.transform.position - transform.position);

        //DebugText.Instance.SetText("" + timer);

        switch (state)
        {
            default:
            case State.ROTATE:
                targetRot.z -= Time.deltaTime * 40;

                if (targetRot.z <= 0)
                {
                    targetRot.z = 0;
                    targetRot.x = 0;
                    //transform.rotation = Quaternion.Euler(targetRot);

                    timer += Time.deltaTime;
                }

                if (timer >= timeDelay)
                {
                    ActivateLaser();
                    timer = 0;
                    state = State.SPINNING_UP;

                    //Debug.Log("SPINNING UP");
                }

                //velocity = Vector3.Normalize(target.transform.position - transform.position) * speed * 0.75f;
                break;

            case State.SPINNING_UP:

                timer += Time.deltaTime;


                if (timer >= timeDelay && !spining)
                {
                    spining = true;

                    //if (clockWise)
                    //    rotSpeed = rotSpeed;
                    //else
                    //    rotSpeed = -rotSpeed;
                }
                velocity = Vector3.zero;
                break;
        }

        //Vector3 forward = laser.transform.TransformDirection(Vector3.forward) * 1000;
        //Debug.DrawRay(transform.position, forward, Color.green);


        bool blockDetected = false;   

        RaycastHit[] hitArray = Physics.RaycastAll(laser.transform.position, laser.transform.forward, maxLength);

        foreach(RaycastHit hit in hitArray)
        {
            if (hit.collider.tag == "Block")
            {
                blockDetected = true;
                laserEnd = hit.point;

                laserLength = (laser.transform.position - hit.point).magnitude;

                break;
            }             
        }

        if(!blockDetected)
        {
            laserEnd = laser.transform.position + (laser.transform.forward * maxLength);
            laserLength = maxLength;
        }

        if (laserActive)
        {
            laserLine.SetPosition(0, laser.transform.position);
            laserLine.SetPosition(1, laserEnd);

            laserCollider.center = new Vector3(0, 0, laserLength * 0.5f);
            laserCollider.size = new Vector3(100, 100, laserLength);

            laserLine.SetWidth(laserWidth, laserWidth);

            if (laserWidth <= maxWidth)
                laserWidth += Time.deltaTime * 50;
            else
                laserWidth = maxWidth;
        }
	}

    protected void ActivateLaser()
    {
        laserWidth = 0;
        laserActive = true;
        laserLine.enabled = true;
    }

    protected override void Rotate()
    {
        if(target)
        {
            if (state == State.SPINNING_UP && spining)
            {
                transform.Rotate(new Vector3(0, rotSpeed, 0) * Time.deltaTime * timeDilation);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    Quaternion.Euler(targetRot), rotSpeed * Time.deltaTime);
            }
        }
    }


    //protected override void ChangeMat(Material mat)
    //{
    //    GetComponent<Renderer>().material = mat;

    //    for (int i = 0; i <= 3; i++)
    //    {
    //        piston[i].GetComponent<Renderer>().material = mat;
    //    }
    //}
}
