using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_RedSun : BaseLevel
{
    public GameObject _drone;
    public GameObject _crossCannon;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();

    RedCore redCore;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        //totalBeats = 300;

        levelType = LevelType.BOSS;

        corePos = new Vector3(1000, 0, 1000);

        //InstantiatePool(drone, _drone, 12);

        redCore = Object.FindObjectOfType<RedCore>();
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;

            case 1:
                //redCore.TurnRed();

                //SpawnCircle(drone, 8, 500);
                break;
        }
    }
}
