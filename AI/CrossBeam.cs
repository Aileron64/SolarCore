using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CrossBeam : BaseEnemy
{
    //LineRenderer[] line = new LineRenderer[2];
    GameObject[] laser = new GameObject[2];
    BoxCollider[] col = new BoxCollider[2];
    Vector3[] laserEnd = new Vector3[2];
    float[] laserLength = new float[2];
    Transform[] laserStart = new Transform[2];

    enum State
    {
        LASER, ROTATE,
    }
    State state = State.LASER;

    float maxLength = 5000;
    float maxWidth = 80;
    float laserWidth;

    bool laserActive = false;
    bool deativateLaser = false;

    int beatCount;
    float yEuler;

    float rotateTimer;
    float rotateTime = 1.5f;

    override protected void Start()
    {
        rotationSpeed = 6;

        for (int i = 0; i < 2; i++)
        {
            laser[i] = transform.Find("Laser " + i).gameObject;
            laserStart[i] = transform.Find("LaserStart " + i).transform;
            col[i] = laser[i].GetComponent<BoxCollider>();
        }

        DeactivateColliders();

        base.Start();
    }

    public override void OnSpawn()
    {
        state = State.LASER;
        rotateTimer = 0;

        beatCount = 0;
        laserActive = false;
        deativateLaser = false;

        if (laser[0] && laser[1])
        {
            laser[0].transform.localScale = new Vector3(0, 0, 0);
            laser[1].transform.localScale = new Vector3(0, 0, 0);
        }

        laserWidth = 0;

        yEuler = 90;
        targetRotation = Quaternion.Euler(0, yEuler, 0);
        transform.rotation = targetRotation;

        base.OnSpawn();
    }


    protected override void Normal()
    {
        if (laserActive)
        {
            if (deativateLaser)
            {
                if (laserWidth >= 0)
                    laserWidth -= Time.deltaTime * 100;
                else
                {
                    laserWidth = 0;
                    laserActive = false;
                    deativateLaser = false;
                }
            }
            else
            {
                if (laserWidth <= maxWidth)
                    laserWidth += Time.deltaTime * 100;
                else
                    laserWidth = maxWidth;
            }

        }

        // Each Laser
        for (int i = 0; i < 2; i++)
        {
            laser[i].transform.localScale = new Vector3(laserLength[i] / 4, laserWidth / 12, laserWidth / 4);
        }


        if (state == State.ROTATE && gameState == EnemyState.NORMAL)
        {
            rotateTimer += Time.deltaTime;

            if(rotateTimer >= rotateTime)
            {
                rotateTimer = 0;

                transform.rotation = targetRotation;
                state = State.LASER;
            }
        }
    }

    protected override void Rotate()
    {
        if(!laserActive)
            rB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed));
    }

    void ActivateColliders()
    {
        for (int i = 0; i < 2; i++)
        {
            col[i].enabled = true;
        }
    }

    void DeactivateColliders()
    {
        for (int i = 0; i < 2; i++)
        {
            col[i].enabled = false;
        }
    }

    void SetLaserLength()
    {
        for (int i = 0; i < 2; i++)
        {
            laserLength[i] = maxLength;
            laserEnd[i] = laserStart[i].transform.position + (laserStart[i].transform.forward * maxLength);

            RaycastHit[] hitArray = Physics.RaycastAll(laserStart[i].transform.position, laserStart[i].transform.forward, maxLength);

            foreach (RaycastHit hit in hitArray)
            {
                if (hit.collider.tag == "Block")
                {
                    if((laserStart[i].transform.position - hit.point).magnitude < laserLength[i])
                    {
                        laserLength[i] = (laserStart[i].transform.position - hit.point).magnitude;
                        laserEnd[i] = hit.point;
                    }
                }
            }

            laser[i].transform.position = (laserStart[i].position + laserEnd[i]) * 0.5f;
        }   
    }

    protected override void OnBeat()
    {
        if (state == State.LASER)
        {
            if (beatCount == 0)
            {
                laserWidth = 0;
                laserActive = true;

                Invoke("ActivateColliders", 0.5f);

                SetLaserLength();
            }

            beatCount++;

            if (beatCount >= 6)
            {
                state = State.ROTATE;
                beatCount = 0;

                deativateLaser = true;

                Invoke("DeactivateColliders", 0.5f);

                yEuler += 90;
                targetRotation = Quaternion.Euler(0, yEuler, 0);
            }
        }
    }

}
