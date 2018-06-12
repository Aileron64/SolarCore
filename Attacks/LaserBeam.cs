using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour 
{
    public Transform endPoint;
    LineRenderer laserLine;

    float minWidth = 1;
    public float maxWidth = 15;
    float width;

    float timeTillActive = 1;
    float timer;

    public bool isActive = false;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(minWidth, minWidth);
        width = minWidth;
    }

    void Update()
    {
        if (width <= maxWidth)
        {
            width += Time.deltaTime * 10;
            laserLine.SetWidth(width, width);
        }


        if (!isActive && timer >= timeTillActive)
        {
            isActive = true;
            gameObject.tag = "Enemy Laser Beam";
        }
        else
            timer += Time.deltaTime;

        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, endPoint.position);
    }
}
