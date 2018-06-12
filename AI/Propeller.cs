using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Propeller : BaseEnemy
{
    float oribitDistance;

    public GameObject nova_prefab;
    List<GameObject> nova = new List<GameObject>();

    public GameObject[] part = new GameObject[4];
    LineRenderer[] line = new LineRenderer[4];
    public Transform[] linePoint = new Transform[4];


    public float sizeLength;
    float targetLength;
    float laserWidth = 20;


    float novaSize;

    GameObject clone;


    protected override void Start()
    {
        Pooler.Instantiate(nova, nova_prefab, 10);
        //core = GameObject.FindWithTag("Core").transform;
        //speed = 100;

        for (int i = 0; i < 4; i++)
        {
            line[i] = part[i].GetComponent<LineRenderer>();
            //col[i] = laser[i].GetComponent<BoxCollider>();
        }

        rotationSpeed = 5;
        //targetLength = 250;

        base.Start();
    }


    public override void OnSpawn()
    {
        for (int i = 0; i < 4; i++)
        {
            part[i].transform.localPosition = new Vector3(0, 0, 0);        
        }

        novaSize = 200;

        transform.rotation = Quaternion.identity;
        base.OnSpawn();
    }

    protected override void EndWarp()
    { 
        base.EndWarp();
        sizeLength = 0;
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            line[i].SetPosition(0, linePoint[i].position);
            line[i].SetPosition(1, transform.position + new Vector3(0, 28, 0));

        }
    }

    protected override void Normal()
    {
        transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime * timeDilation);


        

        targetLength = sizeLength / 2 - 50;

        //DebugText.Instance.SetText("" + sizeLength);

        if (targetLength > 0)
        {
            part[0].transform.localPosition = new Vector3(0, 0, targetLength / 4);
            part[1].transform.localPosition = new Vector3(targetLength / 4, 0, 0);
            part[2].transform.localPosition = new Vector3(0, 0, -targetLength / 4);
            part[3].transform.localPosition = new Vector3(-targetLength / 4, 0, 0);
        }
    }

    protected override void OnBeat()
    {
        clone = Instantiate(nova_prefab, transform.position, Quaternion.identity) as GameObject;
        //clone = Pooler.GetObject(nova, transform.position, transform.rotation);

        clone.GetComponent<PropellerNova>().owner = this;
        clone.GetComponent<PropellerNova>().maxSize = novaSize;

        //transform.DOMoveY(-10, 0.1f).From();

        novaSize += 50;
    }

    //void Lasers()
    //{
    //    // Each Laser
    //    for (int i = 0; i < 4; i++)
    //    {
    //        targetLength = sizeLength / 2 - 120;

    //        laserEnd[i] = laser[i].transform.position + (laser[i].transform.forward * targetLength);
    //        laserLength[i] = targetLength;

    //        laserEnd[i] = laser[i].transform.position + (laser[i].transform.forward * laserLength[i]);

    //        col[i].center = new Vector3(0, 0, laserLength[i] * 0.5f / 4);
    //        col[i].size = new Vector3(laserWidth / 4, laserWidth / 4, laserLength[i] / 4);

    //        line[i].startWidth = laserWidth;
    //        line[i].endWidth = laserWidth;

    //        line[i].SetPosition(0, laser[i].transform.position);
    //        line[i].SetPosition(1, laserEnd[i]);

    //        endPart[i].transform.localPosition = new Vector3(0, -5, targetLength / 4);
    //    }
    //}


    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        for (int i = 0; i < part.Length; i++)
        {
            part[i].GetComponent<Renderer>().material = mat;
        }
    }
}

