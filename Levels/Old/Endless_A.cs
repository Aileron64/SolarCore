using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Endless_A : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject mini_prefab;
    public GameObject crossCannon_prefab;
    public GameObject hWing_prefab;
    public GameObject crossBeam_prefab;
    public GameObject crossBomber_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> hWing = new List<GameObject>();
    List<GameObject> crossBeam = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();


    int d = 0;


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        //SetTotalBeats(3, 30);
        //448

        levelType = LevelType.NONE;

        corePos = new Vector3(1000, 0, 1000);




        InstantiatePool(drone, drone_prefab, 20);
        InstantiatePool(mini, mini_prefab, 25);
        InstantiatePool(crossCannon, crossCannon_prefab, 12);
        InstantiatePool(hWing, hWing_prefab, 12);
        InstantiatePool(crossBeam, crossBeam_prefab, 8);
        InstantiatePool(crossBomber, crossBomber_prefab, 4);
    }

    protected override void Spawn(int waveNum)
    {


        switch (waveNum)
        {
            default:
                break;

            case 10:
                SpawnCircle(drone, 6, 600);

                SpawnCircle(mini, 5 * d, 1000);
                break;

            case 25:
                SpawnCircle(drone, 10, 800);

                SpawnCircle(mini, 10 * d, 1300);
                break;

            case 50:
                SpawnCoinCircle(8, 500);
                SpawnCircle(drone, 14, 1000);

                SpawnCircle(mini, 4 * d, 700);
                SpawnCircle(mini, 10 * d, 1300);

                break;

            case 80:
                SpawnCoinCircle(6, 550);
                SpawnCircle(crossCannon, 3, 800);

                SpawnCircle(mini, 5 * d, 1100);
                break;

            case 100:
                if (Random.Range(0, 2) == 0)
                    SpawnCircle(drone, 4, 600);
                else
                    SpawnCircle(mini, 8, 600);

                SpawnCircle(hWing, 3 * d, 1000);
                break;

            case 115:
                SpawnCoinCircle(6, 700);

                if (Random.Range(0, 2) == 0)
                    SpawnCircle(drone, 6, 1000);
                else
                    SpawnCircle(mini, 12, 1000);

                SpawnCircle(drone, 6 * d, 2500);
                break;

            case 140:
                if (Random.Range(0, 2) == 0)
                    SpawnCircle(drone, 8, 1200);
                else
                    SpawnCircle(mini, 16, 1200);
                
                SpawnCircle(crossCannon, 3 * d, 600);
                break;

            case 170:
                SpawnCoinCircle(10, 1200);
                SpawnCircle(crossCannon, 4, 800);

                SpawnCircle(drone, 4 * d, 500);
                break;

            case 190:
                SpawnCircle(drone, 6, 600);

                SpawnCircle(crossCannon, 4 * d, 1100);
                break;

            case 205:
                SpawnCircle(drone, 8, 900);

                SpawnCircle(crossCannon, 3 * d, 600);
                break;

            case 240:

                SpawnEnemy(crossBomber, 0, 0);

                SpawnCircle(hWing, 4* d, 700);
                break;

            case 245:
                SpawnCoinCircle(10, 450);
                break;

            case 280:
                SpawnCircle(drone, 6, 600);

                SpawnCircle(hWing, 3 * d, 1100);
                break;

            case 300:
                SpawnEnemy(crossBomber, 0, 800);
                SpawnEnemy(crossBomber, 0, -800);

                SpawnCircle(mini, 10 * d, 1200);
                break;

            case 305:
                SpawnCoinCircle(8, 450, new Vector3(1000, 0, 1800));
                SpawnCoinCircle(8, 450, new Vector3(1000, 0, 200));
                break;

            case 335:
                SpawnCircle(drone, 8, 500);
                SpawnCircle(mini, 6 * d, 1000);
                break;

            case 340:
                SpawnCircle(drone, 12, 1100);

                SpawnCircle(mini, 6 * d, 700);
                break;

            case 380:
                SpawnEnemy(crossBomber, 0, 0);

                SpawnCircle(drone, 6 * d, 1100);
                break;

            case 385:
                SpawnCoinCircle(14, 650);
                break;

            case 400:
                SpawnCircle(crossCannon, 5, 460);

                SpawnCircle(drone, 4 * d, 1200);
                break;

            case 440:
                SpawnCircle(drone, 5, 600);

                SpawnCircle(mini, 6 * d, 100);
                break;

            case 448:
                beatNum = 0;
                d++;
                break;


        }
    }
}


