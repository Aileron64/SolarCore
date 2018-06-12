using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_1 : BaseLevel
{
    public GameObject _crossBeam;
    public GameObject _mini;
    public GameObject _drone;
    //public GameObject _crossCannon;
    public GameObject _crossBomber;

    List<GameObject> crossBeam = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> drone = new List<GameObject>();
    //List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(85);
        SetTotalBeats(3, 8); //345

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(crossBeam, _crossBeam, 12);
        InstantiatePool(mini, _mini, 25);
        InstantiatePool(drone, _drone, 20);
        //InstantiatePool(crossCannon, _crossCannon, 5);
        InstantiatePool(crossBomber, _crossBomber, 1);

        background.ChangeColour(color[0]);


        StartAt(150);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;

            case 0:
                SpawnEnemy(crossBeam, 0, 0);
                break;

            case 16:
                SpawnEnemy(crossBeam, 760, 760);
                SpawnEnemy(crossBeam, 760, -760);
                SpawnEnemy(crossBeam, -760, 760);
                SpawnEnemy(crossBeam, -760, -760);
                break;

            case 26:
                SpawnCircle(mini, 8, 500, PI / 8);
                break;

            case 31:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(8, 400);
                break;

            case 45:
                SpawnCircle(drone, 6, 500);
                break;

            case 58:
                SpawnCircle(mini, 20, 1200, PI / 20);
                break;

            case 65:
                SpawnEnemy(crossBeam, 0, 1360);
                SpawnEnemy(crossBeam, 1360, 0);
                SpawnEnemy(crossBeam, 0, -1360);
                SpawnEnemy(crossBeam, -1360, 0);
                break;

            case 80:
                SpawnCoinCircle(8, 400, new Vector3(1000, 0, 1760));
                SpawnCoinCircle(8, 400, new Vector3(1000, 0, 240));
                break;

            case 90:
                SpawnCircle(drone, 9, 450);
                break;

            case 128:
                background.ChangeColour(color[2]);
                break;

            case 130:
                SpawnEnemy(crossBeam, 520, 520);
                SpawnEnemy(crossBeam, 520, -520);
                SpawnEnemy(crossBeam, -520, 520);
                SpawnEnemy(crossBeam, -520, -520);
                break;

            case 135:
                SpawnCircle(drone, 10, 500);
                break;

            case 153:
                SpawnCircle(mini, 20, 1200, PI / 20);
                break;

            case 170:
                SpawnCoinCircle(8, 400, new Vector3(1760, 0, 1000));
                SpawnCoinCircle(8, 400, new Vector3(240, 0, 1000));
                break;

            case 185:
                SpawnCircle(mini, 20, 1200);
                break;

            case 217:
                SpawnEnemy(crossBeam, 520, 760);
                SpawnEnemy(crossBeam, 520, -760);
                SpawnEnemy(crossBeam, -520, 760);
                SpawnEnemy(crossBeam, -520, -760);
                break;

            case 223:
                background.ChangeColour(color[1]);
                break;

            case 250:
                SpawnCircle(drone, 8, 500, PI / 8);
                break;

            case 255:
                SpawnCoinCircle(8, 400);
                break;

            case 260:
                SpawnEnemy(crossBeam, 760, 520);
                SpawnEnemy(crossBeam, 760, -520);
                SpawnEnemy(crossBeam, -760, 520);
                SpawnEnemy(crossBeam, -760, -520);
                break;

            case 270:
                SpawnEnemy(crossBomber, 0, 0);
                break;

            case 300:
                SpawnEnemy(crossBeam, 0, 520);
                SpawnEnemy(crossBeam, 0, -520);
                break;

            case 320:
                SpawnEnemy(crossBeam, 520, 0);
                SpawnEnemy(crossBeam, -520, 0);
                break;

            case 330:
                SpawnCircle(drone, 20, 1200);
                break;
        }
    }
}
