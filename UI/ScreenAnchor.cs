using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnchor : MonoBehaviour
{
    public enum Anchor
    {
        LEFT, RIGHT
    }

    public Anchor side;

    public float offset;
    float x;
    float ratio;


    void Awake()
    {
        ratio = (float)Screen.width / (float)Screen.height;

        switch (side)
        {
            default:
            case Anchor.LEFT:
                x = offset + ratio * -570;
                break;

            case Anchor.RIGHT:
                x = offset + ratio * 570;
                break;

        }

        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    //void Update()
    //{
    //    ratio = (float)Screen.width / (float)Screen.height;

    //    switch (side)
    //    {
    //        default:
    //        case Anchor.LEFT:
    //            x = offset + ratio * -570;
    //            break;

    //        case Anchor.RIGHT:
    //            x = offset + ratio * 570;
    //            break;

    //    }


    //    //DebugText.Instance.SetText("Ratio = " + ratio); // + "\b X = " + x);
    //    transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    //}

}
