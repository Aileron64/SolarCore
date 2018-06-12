using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundControl : MonoBehaviour
{
    const int B_NUM = 2;

    public GameObject[] background;
    Material[] backgroundMat = new Material[B_NUM + 1];
    Vector2[] backgroundOffset = new Vector2[B_NUM];

    Camera cam;

    bool discoTime = false;
    Color discoColor1;
    Color discoColor2;
    bool discoFlag;


    void Awake()
    {
        transform.rotation = Quaternion.Euler(new Vector3(70, 90, 0));

        cam = GetComponent<Camera>();
        BaseLevel.OnBeat += BeatEvent;


        for (int i = 0; i < B_NUM; i++)
        {
            backgroundMat[i] = background[i].GetComponent<Renderer>().material;
        }

    }

    void FixedUpdate()
    {
        backgroundOffset[0].Set(transform.position.z * -0.000016f, transform.position.x * 0.00009f);
        backgroundOffset[1].Set(transform.position.z * -0.000008f + 0.1f, transform.position.x * 0.000045f - 0.1f);
        //backgroundOffset[2].Set(transform.position.z * -0.000016f, transform.position.x * 0.000025f);

        for (int i = 0; i < B_NUM; i++)
        {
            backgroundMat[i].SetTextureOffset("_MainTex", backgroundOffset[i]);
        }
    }

    public void ChangeColour(Color _color)
    {
        //backgroundMat[2].DOColor(_color, 0.45f);
        Camera.main.DOColor(_color, 0.45f);
    }

    public void ChangeColour(Color _color, float time)
    {
        //backgroundMat[2].DOColor(_color, time);
        Camera.main.DOColor(_color, time);
    }

    public void ToggleStars(bool x)
    {
        if (x)
        {
            backgroundMat[0].DOFade(1, 0.45f);
            backgroundMat[1].DOFade(1, 0.45f);
        }
        else
        {
            backgroundMat[0].DOFade(0, 0.45f);
            backgroundMat[1].DOFade(0, 0.45f);
        }
        

    }

    public void DiscoTime()
    {
        discoTime = !discoTime;
    }

    void BeatEvent()
    {
        if (discoTime)
        {
            if (discoFlag)
            {
                backgroundMat[0].DOFade(1, 0.45f);
                backgroundMat[1].DOFade(0.7f, 0.45f);
            }
            else
            {
                backgroundMat[0].DOFade(0.7f, 0.45f);
                backgroundMat[1].DOFade(1, 0.45f);
            }

            discoFlag = !discoFlag;
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
    }
}
