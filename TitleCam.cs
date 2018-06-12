using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCam : MonoBehaviour
{

    public GameObject back1;
    public GameObject back2;
    Material background1;
    Material background2;

    Vector2 backgroundOffset1;
    Vector2 backgroundOffset2;

    float x;
    float y;

    void Start ()
    {

        background1 = back1.GetComponent<Renderer>().material;
        background2 = back2.GetComponent<Renderer>().material;
    }

    void Update ()
    {
        x += Time.deltaTime;
        y += Time.deltaTime;

        backgroundOffset1.Set(x * -0.016f, y * 0.009f);
        backgroundOffset2.Set(x * -0.008f + 0.1f, y * 0.0045f - 0.1f);

        background1.SetTextureOffset("_MainTex", backgroundOffset1);
        background2.SetTextureOffset("_MainTex", backgroundOffset2);
    }
}
