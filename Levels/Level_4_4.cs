using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_4 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject prop_prefab;
    public GameObject flash_prefab;
    public GameObject trishot_prefab;
    public GameObject crossBeam_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> prop = new List<GameObject>();
    List<GameObject> flash = new List<GameObject>();
    List<GameObject> trishot = new List<GameObject>();
    List<GameObject> crossBeam = new List<GameObject>();

    float x;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(4, 30); //4:15 - 544

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 22);
        InstantiatePool(prop, prop_prefab, 12);
        InstantiatePool(flash, flash_prefab, 12);
        InstantiatePool(trishot, trishot_prefab, 5);
        InstantiatePool(crossBeam, crossBeam_prefab, 12);

        background.ChangeColour(color[0]);

        //
        //StartAt(438);
    }

    protected override void Spawn(int num)
    {
        switch (num)
        {
            default:
                break;

            case 0:
                SpawnEnemy(crossBeam, 720, 720);
                SpawnEnemy(crossBeam, 720, -720);
                SpawnEnemy(crossBeam, -720, 720);
                SpawnEnemy(crossBeam, -720, -720);
                break;

            case 26:
                CrackDrones();
                break;

            case 61:
                SpawnEnemy(prop, 0, 0);
                CrackDrones();
                break;

            case 68:
                background.ChangeColour(color[1]);
                break;


            case 95:
                SpawnCircle(flash, 4, 450, PI / 4);
                SpawnCoinCircle(4, 450);
                break;

            case 96:
                SpawnCircle(flash, 4, 650);
                SpawnCoinCircle(4, 650, PI / 4);
                break;

            case 97:
                SpawnCircle(flash, 4, 850, PI / 4);
                SpawnCoinCircle(4, 850);
                break;



            case 131:
                SpawnEnemy(crossBeam, 720, 0);
                SpawnEnemy(crossBeam, -720, 0);
                break;

            case 136:
                background.ChangeColour(color[0]);
                break;

            case 147:
                SpawnEnemy(crossBeam, 0, 720);
                SpawnEnemy(crossBeam, 0, -720);
                break;

            case 164:          
                SpawnCircle(trishot, 4, 950, PI / 4);
                break;

            case 170:
                background.ChangeColour(color[1]);
                break;

            case 174:
                
                break;

            case 199:
                SpawnEnemy(prop, 0, 0);
                CrackDrones();
                SpawnCircle(trishot, 4, 950);
                break;

            case 205:
                background.ChangeColour(color[2]);
                break;

            case 235:
                SpawnCircle(flash, 4, 450, PI / 4);
                SpawnCoinCircle(4, 450);
                break;

            case 236:
                SpawnCircle(flash, 4, 650);
                SpawnCoinCircle(4, 650, PI / 4);
                break;

            case 237:
                SpawnCircle(flash, 4, 850, PI / 4);
                SpawnCoinCircle(4, 850);
                break;

            case 267:
                SpawnEnemy(crossBeam, 720, 720);
                SpawnEnemy(crossBeam, 720, -720);
                SpawnEnemy(crossBeam, -720, 720);
                SpawnEnemy(crossBeam, -720, -720);
                break;

            case 275:
                background.ChangeColour(color[0]);
                break;

            case 299:
                SpawnEnemy(prop, 0, 0);
                CrackDrones();
                break;

            case 334:             
                CrackDrones();
                break;

            case 343:
                CrackDrones();
                break;

            case 352:
                CrackDrones();
                break;

            case 361:
                CrackDrones();
                break;

            case 369:
                CrackDrones();
                break;


            case 402:
                SpawnEnemy(prop, 0, 0);
                CrackDrones();
                //SpawnCircle(trishot, 4, 900);
                break;

            case 409:
                background.ChangeColour(color[1]);
                break;

            case 420:
                CrackDrones();
                break;


            case 438:
                SpawnEnemy(crossBeam, -1440, 480);
                SpawnEnemy(crossBeam, -1440, -480);
                SpawnEnemy(crossBeam, 1440, 480);
                SpawnEnemy(crossBeam, 1440, -480);

                SpawnEnemy(crossBeam, -480, 1440);
                SpawnEnemy(crossBeam, 480, 1440);
                SpawnEnemy(crossBeam, 480, -1440);
                SpawnEnemy(crossBeam, -480, -1440);
                break;

            case 448:
                CrackDrones();
                //SpawnCircle(drone, 8, 850);
                break;

            case 471:
                SpawnCircle(flash, 4, 350, PI / 4);
                SpawnCoinCircle(4, 350);
                break;

            case 472:
                SpawnCircle(flash, 4, 550);
                SpawnCoinCircle(4, 550, PI / 4);
                break;

            case 473:
                SpawnCircle(flash, 4, 750, PI / 4);
                SpawnCoinCircle(4, 750);
                break;

            case 488:
                CrackDrones();
                break;

            case 503:
                SpawnCircle(trishot, 4, 900);
                break;

            case 518:
                CrackDrones();
                break;

            case 533:
                SpawnCircle(trishot, 4, 900, PI / 4);
                break;

            case 546:
                background.ChangeColour(color[2]);
                break;
        }
    }


    void CrackDrones()
    {
        SpawnEnemy(drone, -1200, 480);
        SpawnEnemy(drone, -1200, -480);
        SpawnEnemy(drone, 1200, 480);
        SpawnEnemy(drone, 1200, -480);

        SpawnEnemy(drone, -480, 1200);
        SpawnEnemy(drone, 480, 1200);
        SpawnEnemy(drone, 480, -1200);
        SpawnEnemy(drone, -480, -1200);

        //SpawnCircle(drone, 8, 1100, PI / 8);
    }
}

