using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_5 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject triShot_prefab;
    public GameObject hWing_prefab;
    public GameObject gigaCannon_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> triShot = new List<GameObject>();
    List<GameObject> hWing = new List<GameObject>();
    List<GameObject> gigaCannon = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(4, 2); 

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 16);
        InstantiatePool(triShot, triShot_prefab, 8);
        InstantiatePool(hWing, hWing_prefab, 10);
        InstantiatePool(gigaCannon, gigaCannon_prefab, 4);

        background.ChangeColour(color[0]);

        //StartAt(370);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            case 0:
                SpawnCircle(drone, 4, 800, PI / 4);
                break;

            case 10:
                SpawnCircle(hWing, 4, 1150, PI / 4);
                break;

            case 58:
                SpawnEnemy(gigaCannon, 0, 0);
                break;

            case 64:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(10, 600);
                SpawnCircle(drone, 8, 800, PI / 8);
                break;

            case 90:
                SpawnCircle(hWing, 4, 1150);
                break;


            case 121:
                SpawnCircle(drone, 8, 1000, PI / 8);
                break;

            case 127:
                background.ChangeColour(color[0]);
                break;

            case 131:
                SpawnCircle(drone, 8, 1200, PI / 8);
                break;

            case 141:
                SpawnCircle(drone, 8, 1400, PI / 8);
                break;

            case 185:
                SpawnEnemy(gigaCannon, 0, 450);
                SpawnEnemy(gigaCannon, 0, -450);
                SpawnCircle(triShot, 4, 1200, PI / 4);              
                break;

            case 191:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(8, 600);
                SpawnCircle(drone, 8, 1000, PI / 8);
                break;

            case 220:
                //SpawnCircle(hWing, 4, 1350);
                break;


            case 255:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                SpawnCoinCircle(8, 1000, PI / 8);
                break;


            case 282:
                SpawnCircle(hWing, 4, 1300);
                break;


            case 288:
                background.ToggleStars(true);
                break;


            case 318:

                SpawnCircle(drone, 8, 1400, PI / 8);
                break;


            case 377:
                SpawnCircle(triShot, 8, 1200, PI / 8);
                break;

            case 383:
                background.ChangeColour(color[1]);
                break;

            case 410:
                SpawnCircle(hWing, 4, 1300);
                break;


            case 442:
                SpawnEnemy(gigaCannon, 450, 450);
                SpawnEnemy(gigaCannon, 450, -450);
                SpawnEnemy(gigaCannon, -450, 450);
                SpawnEnemy(gigaCannon, -450, -450);
                break;

            case 448:
                background.ChangeColour(color[2]);
                break;

        }
    }
}
