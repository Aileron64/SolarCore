using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Diamond : MonoBehaviour
{
    List<LineRenderer> lines = new List<LineRenderer>();

    public Material lineMat;
    public float lineWidth;

    Vector3[] points = new Vector3[4];

    float radius = 1;
    float maxSize = 150;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            lines.Add(AddLine());
        }



    }

    void FixedUpdate()
    {
        if (radius <= maxSize)
            radius += Time.deltaTime * 400;



        lines[0].SetPosition(0, new Vector3(0, 0, 1) * radius);
        lines[0].SetPosition(1, new Vector3(1, 0, 0) * radius);

        lines[1].SetPosition(0, new Vector3(1, 0, 0) * radius);
        lines[1].SetPosition(1, new Vector3(0, 0, -1) * radius);

        lines[2].SetPosition(0, new Vector3(0, 0, -1) * radius);
        lines[2].SetPosition(1, new Vector3(-1, 0, 0) * radius);

        lines[3].SetPosition(0, new Vector3(-1, 0, 0) * radius);
        lines[3].SetPosition(1, new Vector3(0, 0, 1) * radius);
    }


    LineRenderer AddLine()
    {
        GameObject line = new GameObject();

        line.transform.position = this.transform.position;
        line.transform.rotation = this.transform.rotation;
        line.hideFlags = HideFlags.HideInHierarchy;



        LineRenderer lr = line.AddComponent<LineRenderer>();

        if (lineMat == null)
        {
            lr.material = new Material(Shader.Find("Particles/Additive"));
        }
        else
        {
            lr.material = lineMat;
        }

        lr.useWorldSpace = false;
        lr.SetWidth(lineWidth, lineWidth);
        lr.SetVertexCount(2);

        return lr;
    }

}
