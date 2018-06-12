using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_1 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject flashBang_prefab;
    public GameObject mini_prefab;
    public GameObject motherShip_prefab;
    public GameObject turtle_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> flashBang = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> motherShip = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 0); //4:15 - 544

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 32);
        InstantiatePool(flashBang, flashBang_prefab, 30);
        InstantiatePool(mini, mini_prefab, 12);
        InstantiatePool(motherShip, motherShip_prefab, 5);
        InstantiatePool(turtle, turtle_prefab, 4);

        background.ChangeColour(color[1]);

        //
        //StartAt(190);
    }

    protected override void Spawn(int num)
    {
        switch (num)
        {
            default:
                break;

            case 0:
                SpawnCircle(flashBang, 4, 800);
                //SpawnCoinCircle(12, 1100);
                break;

            case 10:
                SpawnCircle(flashBang, 4, 1000);
                //SpawnCoinCircle(12, 1100);
                break;

            case 20:
                SpawnCircle(flashBang, 4, 1180);
                //SpawnCoinCircle(12, 1100);
                break;

            case 58:
                SpawnCircle(drone, 14, 1600);
                SpawnEnemy(turtle, 0, 0);
                break;


            case 64:
                background.ChangeColour(color[0]);
                break;


            case 89:
                SpawnCircle(turtle, 4, 1400);
                break;

            case 95:
                background.ChangeColour(color[1]);
                break;

            case 120:
                SpawnCircle(flashBang, 4, 850, PI / 4);
                SpawnCircle(flashBang, 4, 1050);
                SpawnCircle(flashBang, 4, 1200, PI / 4);
                break;

            case 126:
                background.ChangeColour(color[2]);
                break;

            case 130:
                SpawnCircle(mini, 8, 600);
                break;

            case 151:
                SpawnCircle(mini, 8, 600);
                break;

            case 160:
                SpawnCircle(mini, 8, 600);
                break;

            case 168:
                SpawnCircle(mini, 8, 600);
                break;

            case 191:
                background.ChangeColour(color[0]);
                break;

            case 217:
                SpawnCircle(drone, 14, 1600);
                break;

            case 223:
                background.ChangeColour(color[1]);
                break;

            case 233:
                SpawnCircle(drone, 14, 1600);
                break;

            case 249:
                SpawnCircle(turtle, 4, 1400, PI / 4);
                break;

            case 255:
                background.ChangeColour(color[2]);
                break;

            case 271:
                SpawnCircle(turtle, 4, 1200);
                break;

            case 313:
                SpawnCircle(flashBang, 4, 350);
                SpawnCircle(flashBang, 4, 650);
                SpawnCircle(flashBang, 4, 950);
                break;

            case 329:
                SpawnCircle(drone, 18, 2100);
                break;

            case 352:
                background.ChangeColour(color[0]);
                break;
        }
    }
}

