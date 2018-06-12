using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_3 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject shuriken_prefab;
    public GameObject crossBeam_prefab;
    public GameObject crossBomber_prefab;

    List<GameObject> shuriken = new List<GameObject>();
    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossBeam = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(120);
        SetTotalBeats(4, 0); //4:15 - 544

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(shuriken, shuriken_prefab, 12);
        InstantiatePool(drone, drone_prefab, 30);
        InstantiatePool(crossBeam, crossBeam_prefab, 12);
        InstantiatePool(crossBomber, crossBomber_prefab, 5);

        background.ChangeColour(color[0]);

        //StartAt(377);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            case 0:
                SpawnCircle(drone, 8, 1000, PI / 8);
                break;

            case 26:
                SpawnCircle(drone, 12, 1200, PI / 12);
                break;

            case 57:
                SpawnCircle(shuriken, 4, 1000, PI / 4);
                break;

            case 62:
                background.ChangeColour(color[2]);
                break;

            case 90:
                SpawnCircle(shuriken, 8, 1000, PI / 8);
                break;

            case 122:
                SpawnCircle(drone, 12, 1200, PI / 12);
                break;

            case 127:
                background.ChangeColour(color[0]);

                SpawnEnemy(crossBeam, 1000, 1000);
                SpawnEnemy(crossBeam, -1000, 1000);
                SpawnEnemy(crossBeam, 1000, -1000);
                SpawnEnemy(crossBeam, -1000, -1000);
                break;

            case 154:
                SpawnEnemy(crossBomber, 0, 0);

                SpawnCircle(shuriken, 4, 1000, PI / 4);
                break;

            case 160:
                background.ChangeColour(color[2]);
                break;


            case 185:
                SpawnCircle(drone, 8, 1200, PI / 8);
                break;

            case 191:
                background.ChangeColour(color[3]);
                break;


            case 218:
                SpawnCircle(shuriken, 4, 1000, PI / 4);
                break;

            case 224:
                background.ChangeColour(color[0]);

                //SpawnCoinCircle(6, 600);
                SpawnEnemy(crossBeam, 520, 520);
                SpawnEnemy(crossBeam, -520, -520);

                //SpawnCoinCircle(12, 800);
                SpawnEnemy(crossBeam, 520, -520);
                SpawnEnemy(crossBeam, -520, 520);
                break;

            case 250:
                SpawnCircle(drone, 8, 1200, PI / 8);
                break;

            case 281:
                SpawnCircle(crossBomber, 4, 1450);       
                break;

            case 287:
                background.ChangeColour(color[2]);
                break;

            case 298:
                SpawnCircle(shuriken, 4, 1200, PI / 4);
                break;

            case 313:
                SpawnEnemy(crossBeam, 0, 520);
                SpawnEnemy(crossBeam, 0, -520);
                SpawnEnemy(crossBeam, 520, 0);
                SpawnEnemy(crossBeam, -520, 0);
                break;

            case 320:
                background.ChangeColour(color[3]);
                break;

            case 351:
                background.ChangeColour(color[0]);
                SpawnCoinCircle(10, 800);
                break;


            case 379:
                SpawnEnemy(crossBeam, 1000, -520);
                SpawnEnemy(crossBeam, 1000, 520);

                SpawnEnemy(crossBeam, -1000, -520);
                SpawnEnemy(crossBeam, -1000, 520);

                SpawnEnemy(crossBeam, 520, -1000);
                SpawnEnemy(crossBeam, -520, -1000);

                SpawnEnemy(crossBeam, 520, 1000);
                SpawnEnemy(crossBeam, -520, 1000);
                break;

            case 384:
                background.ChangeColour(color[2]);
                break;


            case 410:
                SpawnCircle(drone, 4, 1000, PI / 4);
                break;

            case 445:
                SpawnCircle(drone, 4, 1000, PI / 4);
                break;
        }
    }
}
