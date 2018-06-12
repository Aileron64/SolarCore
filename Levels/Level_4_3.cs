using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_3 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject prop_prefab;
    public GameObject flash_prefab;
    //public GameObject trishot_prefab;
    //public GameObject crossBomber_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> prop = new List<GameObject>();
    List<GameObject> flash = new List<GameObject>();
    //List<GameObject> trishot = new List<GameObject>();
    //List<GameObject> crossBomber = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(126);
        SetTotalBeats(2, 33); //4:15 - 544

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 22);
        InstantiatePool(prop, prop_prefab, 12);
        InstantiatePool(flash, flash_prefab, 12);
        //InstantiatePool(trishot, trishot_prefab, 5);
        //InstantiatePool(crossBomber, crossBomber_prefab, 4);

        background.ChangeColour(color[0]);
 
        
       //StartAt(210);
    }

    protected override void Spawn(int num)
    {
        switch (num)
        {
            default:
                break;

            case 7:
                SpawnCoinCircle(4, 700);
                break;

            case 11:
                SpawnCircle(drone, 4, 1050, PI / 4);
                break;

            case 15:
                SpawnCircle(drone, 4, 1100);
                break;

            case 19:
                SpawnCircle(drone, 4, 1150, PI / 4);
                break;

            case 24:
                SpawnEnemy(prop, 0, 0);
                break;

            case 57:
                SpawnCircle(prop, 4, 1250, PI / 4);
                break;

            case 63:
                background.ChangeColour(color[1]);
                break;

            case 72:
                SpawnCircle(drone, 4, 800);
                SpawnCircle(drone, 4, 1100);
                SpawnCircle(drone, 4, 1400);
                break;

            case 89:
                SpawnCircle(flash, 4, 700);
                SpawnCircle(flash, 4, 900, PI / 4);
                break;

            case 105:
                SpawnEnemy(prop, 0, 0);
                break;

            case 122:
                SpawnCircle(drone, 4, 800);
                //
                break;

            case 126:
                SpawnCircle(drone, 4, 800, PI / 4);
                break;

            case 128:
                background.ChangeColour(color[0]);
                break;

            case 130:
                SpawnCircle(drone, 4, 1000);
                break;

            case 134:
                SpawnCircle(drone, 4, 1000, PI / 4);
                break;

            case 138:
                SpawnCircle(drone, 4, 1200);
                break;

            case 142:
                SpawnCircle(drone, 4, 1200, PI / 4);
                break;

            case 146:
                SpawnCircle(drone, 4, 1400);
                break;

            case 150:
                SpawnCircle(drone, 4, 1200, PI / 4);
                break;

            case 159:
                background.ToggleStars(false);
                break;

            case 169:
                SpawnCircle(prop, 4, 1450, PI / 4);
                break;


            case 217:
                SpawnEnemy(prop, 0, 0);
                break;





            case 218:
                SpawnCircle(drone, 3, 1050, PI / 3);
                break;

            case 223:
                background.ToggleStars(true);
                break;

            case 222:
                SpawnCircle(drone, 3, 1100);
                break;

            case 226:
                SpawnCircle(drone, 3, 1150, PI / 3);
                break;

            case 230:
                SpawnCircle(drone, 3, 1200);
                break;

            case 234:
                SpawnCircle(drone, 3, 1250, PI / 3);
                break;

            case 238:
                SpawnCircle(drone, 3, 1300);
                break;

            case 242:
                SpawnCircle(drone, 3, 1350, PI / 3);
                break;

            case 246:
                SpawnCircle(drone, 3, 1400);
                break;



            case 249:
                SpawnCircle(flash, 4, 950, PI / 4);
                SpawnCircle(flash, 4, 1100);
                SpawnCircle(flash, 4, 1250, PI / 4);
                SpawnCircle(flash, 4, 1400);
                break;

            case 256:
                background.ChangeColour(color[1]);
                break;


            case 266:
                SpawnEnemy(prop, 0, 0);
                break;

            case 282:
                SpawnCircle(drone, 6, 600);
                break;

        }
    }
}

