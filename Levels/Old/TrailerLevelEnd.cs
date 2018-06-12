using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailerLevelEnd : BaseLevel
{
    CameraControl cam;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(4, 30);

        beatTime = 0.5f;
        totalBeats = 540;

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);




        cam = Camera.main.GetComponent<CameraControl>();
    }

    void Start()
    {
        //cam.ClearTargets();
        //cam.AddTarget(GameObject.FindWithTag("Core"));
        //cam.offsetDistance = 0.3f; // 1.4;
    }

    void Update()
    {
         //cam.offsetDistance -= Time.deltaTime * 0.09f;       
    }

}

