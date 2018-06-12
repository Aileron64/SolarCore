using UnityEngine;
using System.Collections;

public class Web : BaseAttack 
{
    const float PARENT_SCALE = 15;
    LineRenderer webLine;

    public Transform prevWeb;

    protected override void Start()
    {
        damage = 1400;
        //parent = transform.parent.gameObject.transform.parent.gameObject;
        webLine = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        webLine.SetPosition(0, transform.position);
        webLine.SetPosition(1, prevWeb.position);

        transform.rotation = Quaternion.LookRotation(prevWeb.position - transform.position);

        float length = (transform.position - prevWeb.position).magnitude;
        length /= PARENT_SCALE;

        GetComponent<BoxCollider>().center = new Vector3(0, 0, length * 0.5f);
        GetComponent<BoxCollider>().size = new Vector3(2, 5, length);
    }
}
