using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_5_4 : BaseLevel
{
    public GameObject dronePrefab;
    public GameObject flash_Prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> flash = new List<GameObject>();



    protected override void Awake()
    {
        base.Awake();

        //beatNum = 275;

        SetBeatTime(128);
        SetTotalBeats(3, 12);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, dronePrefab, 42);





    }

    protected override void Spawn(int waveNum)
    {
        SpawnRandom(drone, waveNum / 100 + 1, 2000);



        switch (waveNum)
        {
            default:
                break;

        }
    }
}