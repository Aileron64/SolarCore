using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_2 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject crossCannon_prefab;
    public GameObject flash_prefab;
    public GameObject trishot_prefab;
    public GameObject crossBomber_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> flash = new List<GameObject>();
    List<GameObject> trishot = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 14); //4:15 - 544

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 22);
        InstantiatePool(crossCannon, crossCannon_prefab, 12);
        InstantiatePool(flash, flash_prefab, 12);
        InstantiatePool(trishot, trishot_prefab, 5);
        InstantiatePool(crossBomber, crossBomber_prefab, 4);

        background.ChangeColour(color[0]);

        //
        //77StartAt(310);
    }

    protected override void Spawn(int num)
    {
        switch (num)
        {
            default:
                break;

            case 0:
                SpawnCoinCircle(12, 1100);
                break;

            case 24:
                SpawnCircle(drone, 12, 900);
                break;

            case 31:
                background.ChangeColour(color[1]);
                break;

            case 58:
                SpawnCircle(flash, 4, 1150);
                SpawnCircle(flash, 4, 650, PI / 4);
                break;

            case 73:
                SpawnCircle(crossCannon, 4, 900, PI / 4);
                break;

            case 77:
                background.ChangeColour(color[2]);
                break;

            case 90:
                SpawnCircle(drone, 12, 1600);
                break;


            case 122:
                SpawnCircle(drone, 12, 1600);
                break;

            case 160:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                break;

            case 185:
                SpawnCircle(drone, 12, 1600);
                break;

            case 191:
                background.ToggleStars(true);
                break;

            case 218:
                SpawnCircle(crossCannon, 4, 900, PI / 4);
                break;


            case 254:
                SpawnEnemy(crossBomber, 0, 0);
                break;

            case 259:
                background.ChangeColour(color[2]);
                SpawnCircle(trishot, 4, 1300);
                break;

            case 282:
                SpawnCircle(drone, 12, 1600, PI / 12);
                break;

            case 317:
                SpawnCircle(crossBomber, 3, 500, PI / -6);
                break;

            case 345:
                SpawnCircle(trishot, 12, 1300);
                break;
        }
    }
}

