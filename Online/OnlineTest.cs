using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlineTest : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject crossCannon_prefab;
    public GameObject crossBomber_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 0);
        //386

        levelType = LevelType.NONE;

        corePos = new Vector3(1000, 0, 1000);

        //InstantiatePool(drone, drone_prefab, 20);
        //InstantiatePool(crossCannon, crossCannon_prefab, 15);
        //InstantiatePool(crossBomber, crossBomber_prefab, 3);

        //beatNum = 316;

        //background.ChangeColour(color[0]);





        //StartAt(380);
    }

    protected override void Spawn(int beat)
    {

        switch (beat)
        {
            default:
                break;

            case 0:
                
                break;

        }
    }

    public override void LevelComplete()
    {
        Achievements.Instance.Achievment("FIRST_CONTACT");
        base.LevelComplete();
    }
}

