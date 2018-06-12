using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioVisualizer;
using DG.Tweening;

public class SolarRings : MonoBehaviour
{
    public float[] ring_Radius;

    private List<LineRenderer> lines = new List<LineRenderer>(); // list of lineRenderers that make up the pad

    public float jumpHeight = 5;

    public Material lineMat;
    public Color[] onColor;
    public Color offColor;

    public Color2[] test1;
    public Color2[] test2;

    float width = 10;
    //float defaultWidth = 10;

    public int lineSegments = 36;

    Transform core;
    Player player;
    float playerDistance;
    public int solarValue;

    int count = 0;

    float beatTime;
    float colorTime = 0.2f;

    DebugText debugText;

    void Start()
    {
        player = Object.FindObjectOfType<Player>();
        debugText = Object.FindObjectOfType<DebugText>();
        core = GameObject.FindWithTag("Core").transform;

        CreateRings();
        BaseLevel.OnBeat += BeatEvent;
        beatTime = BaseLevel.Instance.GetBeatTime();
        solarValue = 3;
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
    }

    void Update()
    {
        if(player)
        {
            playerDistance = Vector3.Magnitude(player.transform.position - core.position);

            switch(solarValue)
            {
                default:
                case 3:
                    if (playerDistance > ring_Radius[0])
                    {
                        lines[0].DOColor(new Color2(onColor[0], onColor[0]), 
                            new Color2(offColor, offColor), colorTime);             

                        solarValue = 2;
                    }
                    break;

                case 2:
                    if (playerDistance < ring_Radius[0])
                    {
                        lines[0].DOColor(new Color2(offColor, offColor),
                            new Color2(onColor[0], onColor[0]), colorTime);
                        solarValue = 3;
                    }
                    else if (playerDistance > ring_Radius[1])
                    {
                        lines[1].DOColor(new Color2(onColor[1], onColor[1]),
                            new Color2(offColor, offColor), colorTime);
                        solarValue = 1;
                    }
                    break;

                case 1:
                    if (playerDistance < ring_Radius[1])
                    {
                        lines[1].DOColor(new Color2(offColor, offColor),
                            new Color2(onColor[1], onColor[1]), colorTime);
                        solarValue = 2;
                    }
                    else if (playerDistance > ring_Radius[2])
                    {
                        lines[2].DOColor(new Color2(onColor[2], onColor[2]),
                            new Color2(offColor, offColor), colorTime);
                        solarValue = 0;
                    }
                    break;

                case 0:
                    if (playerDistance < ring_Radius[2])
                    {
                        lines[2].DOColor(new Color2(offColor, offColor),
                            new Color2(onColor[2], onColor[2]), colorTime);
                        solarValue = 1;
                    }
                    break;
            }
        }
    }



    public int GetSolarValue(Vector3 pos)
    {
        float distance = Vector3.Magnitude(pos - core.position);

        if (distance < ring_Radius[0])
            return 3;
        else if (distance < ring_Radius[1])
            return 2;
        else if (distance < ring_Radius[2])
            return 1;
        else
            return 0;
    }

    void BeatEvent()
    {
        float heightOverride = AudioSampler.instance.GetRMS(0) * 50;

        for (int i = ring_Radius.Length - 1; i >= 0; i--)
        {
            if (playerDistance <= ring_Radius[i])
            {
                //iTween.MoveFrom(lines[i].gameObject, iTween.Hash(
                //    "y", heightOverride, "islocal", true, "time", beatTime * 0.8f));

                lines[i].transform.DOLocalMoveY(heightOverride, beatTime * 0.8f).From();

            }
        }

    }

    //create "numlines" circles, from a radius of 0 to "radius".
    void CreateRings()
    {
        //float radiusStep = radius / (numLines + 1);
        float angleStep = 360f / (lineSegments - 1);

        for (int i = 0; i < ring_Radius.Length; i++)
        {
            lines.Add(NewLine(onColor[i]));

            lines[i].gameObject.AddComponent<Rigidbody>().isKinematic = true;

            float angle = 0; // 
            float thisRadius = ring_Radius[i]; //radiusStep * (i + 2); // the radius of this ring

            for (int j = 0; j < lineSegments; j++)
            {
                float rad = Mathf.Deg2Rad * angle;
                float x = Mathf.Cos(rad) * thisRadius;
                float y = Mathf.Sin(rad) * thisRadius;

                Vector3 pos = this.transform.right * x + this.transform.forward * y;
                lines[i].SetPosition(j, pos);
                angle += angleStep;
            }
        }
    }


    LineRenderer NewLine(Color c)
    {
        GameObject line = new GameObject();
        line.transform.position = this.transform.position;
        line.transform.rotation = this.transform.rotation;
        line.transform.parent = gameObject.transform;
        //line.hideFlags = HideFlags.HideInHierarchy;

        LineRenderer lr = line.AddComponent<LineRenderer>();

        lr.useWorldSpace = false;

        if (lineMat == null)
        {
            lr.material = new Material(Shader.Find("Particles/Additive"));
        }
        else
        {
            lr.material = lineMat;
        }

        lr.startColor = c;
        lr.endColor = c;

        lr.startWidth = width;
        lr.endWidth = width;

        lr.positionCount = lineSegments;

        return lr;
    }

}
