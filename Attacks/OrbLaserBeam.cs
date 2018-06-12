using UnityEngine;
using System.Collections;

public class OrbLaserBeam : BaseAttack
{
    GameObject parent;
    LineRenderer laserLine;

    bool active = true;

    protected override void Start()
    {
        damage = 300;
        parent = transform.parent.gameObject.transform.parent.gameObject;
        laserLine = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, parent.transform.position);

        transform.rotation = Quaternion.LookRotation(parent.transform.position - transform.position);

        float length = (parent.transform.position - transform.position).magnitude;

        GetComponent<BoxCollider>().center = new Vector3(0, 0, length * 0.5f);
        GetComponent<BoxCollider>().size = new Vector3(3, 10, length);
    }

    public void ActivateLaser(bool x)
    {
        //laser = x;
    }



}
