using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Level_1_5 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject mini_prefab;
    //public GameObject crossCannon_prefab;
    public GameObject crossBomber_prefab;
    public GameObject hWing_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    //List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();
    List<GameObject> hWing = new List<GameObject>();

    float timer;
    float delay = 25;

    const int TOTAL_WAVES = 7;
    int wave = 0;

    float waveTimer;
    float waveTime = 10;


    int phase = 1;



    float levelTime = 255; // 4:15

    GameObject[] enemies;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(140);
        SetTotalBeats(3, 53);
        timer = delay;
        waveTimer = waveTime;

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 30);
        InstantiatePool(mini, mini_prefab, 20);
        //InstantiatePool(crossCannon, crossCannon_prefab, 12);
        InstantiatePool(crossBomber, crossBomber_prefab, 4);
        InstantiatePool(hWing, hWing_prefab, 15);

        //StartAt(405);
        
    }


    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            case 0:
                SpawnCircle(drone, 12, 1200);
                break;

            case 17:
                SpawnCircle(drone, 12, 1200, PI / 12);
                break;

            case 21:
                SpawnCoinCircle(10, 700);
                break;

            case 26:
                SpawnEnemy(crossBomber, 0, 0);
                break;

            case 31:
                background.ChangeColour(color[1]);
                break;

            case 63:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                break;

            case 70:
                SpawnCoinCircle(10, 700);
                break;

            case 90:
                SpawnCircle(hWing, 3, 850, PI / 6);
                break;

            case 95:
                background.ToggleStars(true);
                break;

            case 122:
                SpawnCircle(hWing, 6, 1100);
                break;

            case 126:
                SpawnCircle(mini, 12, 800);
                break;

            case 127:
                background.ChangeColour(color[2]);
                break;

            case 154:
                SpawnCircle(drone, 8, 1300);
                break;

            case 159:
                background.ChangeColour(color[1]);
                break;

            case 184:
                SpawnCircle(mini, 18, 1000);
                break;

            case 190:
                background.ChangeColour(color[2]);
                break;

            case 219:
                SpawnCircle(drone, 8, 800);
                break;

            case 225:
                background.ChangeColour(color[1]);
                break;

            case 251:
                SpawnEnemy(crossBomber, 0, 400);
                SpawnEnemy(crossBomber, 0, -400);
                break;

            case 256:
                background.ChangeColour(color[0]);
                break;

            case 265:
                SpawnCircle(drone, 6, 700);
                SpawnCoinCircle(6, 700, PI / 6);
                break;

            case 270:
                SpawnCircle(drone, 6, 850, PI / 6);
                SpawnCoinCircle(6, 850);
                break;

            case 275:
                SpawnCircle(drone, 6, 1000);
                SpawnCoinCircle(6, 1000, PI / 6);
                break;

            case 288:
                SpawnCircle(hWing, 6, 1100);
                break;

            case 319:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                SpawnCoinCircle(6, 900, PI / 6);
                break;

            case 340:
                SpawnCoinCircle(6, 700);
                break;

            case 378:
                SpawnCircle(drone, 16, 1200);
                break;

            case 384:               
                background.ToggleStars(true);
                break;

            case 410:
                SpawnCircle(crossBomber, 3, 750, PI / 6);

                SpawnCoinCircle(3, 750, 4, 340, PI / 6);
                break;

            case 415:
                background.ChangeColour(color[2]);
                break;

            case 442:
                SpawnCircle(hWing, 3, 1100, PI / -6);
                break;

            case 474:
                SpawnCircle(drone, 20, 1800);
                break;

            case 480:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(6, 700, PI / 6);
                break;

            case 505:
                SpawnCircle(drone, 6, 700);
                
                break;

            case 510:
                background.ChangeColour(color[0]);
                SpawnCircle(drone, 6, 850, PI / 6);
                SpawnCoinCircle(6, 850);
                break;

            case 515:
                SpawnCircle(drone, 6, 1000);
                SpawnCoinCircle(6, 1000, PI / 6);
                break;


        }

    }

}
