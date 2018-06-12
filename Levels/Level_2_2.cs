using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_2_2 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject shuriken_prefab;
    public GameObject mini_prefab;
    public GameObject crossBeam_prefab;
    public GameObject turtle_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> shuriken = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> crossBeam = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        //

        SetTotalBeats(4, 14);
        //totalBeats = 350;


        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 25);
        InstantiatePool(shuriken, shuriken_prefab, 14);
        InstantiatePool(mini, mini_prefab, 50);
        InstantiatePool(crossBeam, crossBeam_prefab, 4);
        InstantiatePool(turtle, turtle_prefab, 5);
   
        //StartAt(440);

        background.ChangeColour(color[2]);
    }

    protected override void Spawn(int beat)
    {

        switch (beat)
        {
            default:
                break;

            case 0:
                SpawnCircle(mini, 6, 1000);
                break;

            case 25:
                SpawnCircle(shuriken, 4, 600);
                break;

            case 57:
                SpawnCircle(shuriken, 6, 1100);
                break;

            case 64:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(6, 800, PI / 8);
                break;

            case 88:
                SpawnCircle(mini, 12, 1300);
                break;

            case 121:
                SpawnEnemy(turtle, 0, 0);
                break;

            case 127:
                background.ChangeColour(color[2]);
                break;

            case 138:
                SpawnCircle(turtle, 4, 800);
                break;


            case 152:
                SpawnCircle(mini, 12, 1300);
                break;


            case 185:
                SpawnCircle(shuriken, 8, 1300);
                break;

            case 191:
                background.ChangeColour(color[3]);
                SpawnCoinCircle(6, 900, PI / 8);
                break;


            case 218:
                SpawnCircle(mini, 8, 1300);
                break;


            case 235:
                SpawnCircle(mini, 16, 1300);
                break;


            case 250:
                SpawnCircle(shuriken, 4, 1600);
                break;



            case 282:
                SpawnCircle(turtle, 4, 1100);
                break;

            case 287:
                background.ChangeColour(color[1]);
                break;


            case 313:
                SpawnCircle(mini, 12, 1300, PI / 12);
                break;

            case 319:
                background.ChangeColour(color[2]);
                break;


            case 345:
                SpawnCircle(mini, 12, 1300, PI / 12);
                break;


            case 378:
                SpawnCircle(shuriken, 4, 1700);
                break;

            case 383:
                background.ChangeColour(color[1]);
                break;


            case 392:
                SpawnCircle(turtle, 4, 1300);
                break;

            case 398:
                background.ChangeColour(color[2]);
                break;

            case 409:
                SpawnCircle(shuriken, 4, 1100, PI / 4);
                break;

            case 415:
                background.ChangeColour(color[3]);
                break;

            case 417:
                SpawnCircle(shuriken, 4, 800);
                break;



            case 442:
                SpawnCircle(mini, 8, 1200, PI / 8);
                break;

            case 450:
                SpawnCircle(mini, 8, 1100);
                break;

            case 458:
                SpawnCircle(mini, 8, 1000, PI / 8);
                break;


            case 505:
                SpawnCircle(shuriken, 8, 1200);
                break;

            case 511:
                background.ChangeColour(color[1]);
                break;
        }
    }
}


