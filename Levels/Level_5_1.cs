using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_5_1 : BaseLevel
{
    public GameObject dronePrefab;
    public GameObject miniPrefab;
    public GameObject hWingPrefab;
    public GameObject crossCannonPrefab;
    public GameObject turtlePrefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> hWing = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        //beatNum = 275;

        SetBeatTime(128);
        SetTotalBeats(4, 45);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, dronePrefab, 12);
        InstantiatePool(mini, miniPrefab, 20);
        InstantiatePool(hWing, hWingPrefab, 10);
        InstantiatePool(crossCannon, crossCannonPrefab, 10);
        InstantiatePool(turtle, turtlePrefab, 3);



        sRings.jumpHeight = 2;
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;


            case 8:
                SpawnCircle(mini, 8, 800);       
                break;

            case 14:
                background.ChangeColour(color[1]);
                sRings.jumpHeight = 6;
                break;


            case 25:
                SpawnCircle(hWing, 4, 1100);
                break;

            case 57:
                SpawnCircle(mini, 8, 800);
                break;

            case 65:
                SpawnCoinCircle(8, 600, PI / 8);
                break;

            case 74:
                SpawnCircle(hWing, 4, 800);
                break;

            case 80:
                background.ChangeColour(color[0]);              
                break;

            case 90:
                SpawnCircle(mini, 6, 500);
                break;

            case 127:
                background.ToggleStars(false);
                sRings.jumpHeight = 2;
                break;

            case 140:
                SpawnCoinCircle(12, 800);
                break;

            case 160:
                background.ToggleStars(true);
                sRings.jumpHeight = 5;
                SpawnEnemy(turtle, 0, 0);
                break;

            case 165:
                SpawnCoinCircle(10, 450);
                SpawnCircle(mini, 9, 700);
                break;

            case 187:
                SpawnCircle(crossCannon, 6, 900);
                break;

            case 191:
                background.ChangeColour(color[2]);
                sRings.jumpHeight = 10;
                break;

            //case 233:
            //    background.ChangeColour(color[4]);
            //    break;

            case 255:
                background.ChangeColour(color[0]);
                sRings.jumpHeight = 2;
                break;

            case 260:
                SpawnCircle(mini, 5, 600);
                break;

            case 280:
                SpawnCircle(mini, 5, 600);
                break;

            case 281:
                SpawnCircle(mini, 5, 700, PI / 5 * 1);
                break;

            case 300:
                SpawnCircle(mini, 5, 600);
                break;

            case 301:
                SpawnCircle(mini, 5, 700, PI / 5 * 1);
                break;

            case 302:
                SpawnCircle(mini, 5, 800);
                break;

            case 317:
                background.ChangeColour(color[1]);
                break;

            case 322:
                SpawnCircle(hWing, 5, 700);
                break;

            case 370:
                SpawnCoinCircle(8, 600);
                SpawnCircle(hWing, 3, 300);
                break;

            case 380:
                SpawnCircle(mini, 12, 1200);
                break;

            case 385:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                sRings.jumpHeight = 2;
                break;

            case 400:
                SpawnCoinCircle(12, 800);
                break;

            case 415:
                background.ToggleStars(true);
                sRings.jumpHeight = 5;
                break;

            case 430:
                SpawnCircle(drone, 12, 1000);
                break;

            case 443:
                SpawnCircle(turtle, 4, 1400, PI / 4);
                break;

            case 448:
                background.ChangeColour(color[2]);
                sRings.jumpHeight = 10;
                break;

            case 450:
                //SpawnCoinCircle(8, 450, new Vector3(392, 0, 1348));
                //SpawnCoinCircle(8, 450, new Vector3(1606, 0, 1350));
                //SpawnCoinCircle(8, 450, new Vector3(1000, 0, 300));
                break;


            case 476:
                SpawnCircle(hWing, 6, 705);
                break;

            case 510:
                SpawnCircle(mini, 5, 600);
                break;

            case 511:
                SpawnCircle(mini, 5, 700, PI / 5 * 1);
                break;

            case 512:
                SpawnCircle(mini, 5, 800);
                break;

            case 515:
                SpawnCoinCircle(8, 500);
                break;

            case 540:
                SpawnCircle(hWing, 4, 800);
                break;

            //case 560:
            //    SpawnCircle(hWing, 3, 300);
            //    break;

            case 575:
                background.ChangeColour(color[0]);
                sRings.jumpHeight = 4;
                break;
        }
    }
}