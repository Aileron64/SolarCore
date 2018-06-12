using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_5_2 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject triShot_prefab;
    public GameObject crossCannon_prefab;
    public GameObject gigaCannon_prefab;
    public GameObject crossBeam_prefab;
    public GameObject shuriken_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> triShot = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> gigaCannon = new List<GameObject>();
    List<GameObject> crossBeam = new List<GameObject>();
    List<GameObject> shuriken = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(122);
        SetTotalBeats(2, 54);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 16);
        InstantiatePool(triShot, triShot_prefab, 8);
        InstantiatePool(crossCannon, crossCannon_prefab, 10);
        InstantiatePool(gigaCannon, gigaCannon_prefab, 4);
        InstantiatePool(crossBeam, crossBeam_prefab, 16);
        InstantiatePool(shuriken, shuriken_prefab, 16);

        background.ChangeColour(color[0]);

        //StartAt(240);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            case 0:
                SpawnCircle(drone, 8, 1100);
                break;

            case 26:
                SpawnCircle(crossCannon, 4, 1150, PI / 4);
                break;

            case 32:
                background.ChangeColour(color[1]);
                break;

            case 58:
                SpawnCircle(shuriken, 4, 1400);
                break;

            case 90:
                SpawnCircle(drone, 12, 1150);
                break;

            case 122:
                SpawnEnemy(crossBeam, 760, 760);
                SpawnEnemy(crossBeam, -760, 760);
                SpawnEnemy(crossBeam, 760, -760);
                SpawnEnemy(crossBeam, -760, -760);
                break;

            case 140:
                SpawnCircle(crossCannon, 4, 550);
                break;

            case 160:
                background.ToggleStars(false);
                break;

            case 217:
                SpawnEnemy(gigaCannon, 0, 0);
                break;

            case 223:
                SpawnCircle(drone, 8, 1100);
                background.ToggleStars(true);
                break;

            case 250:
                SpawnCircle(shuriken, 4, 1400);
                break;

            case 266:
                SpawnCircle(drone, 8, 1100);
                break;

            case 280:
                SpawnEnemy(gigaCannon, 0, 600);
                break;

            case 295:
                SpawnEnemy(gigaCannon, 0, -600);
                break;

            case 314:
                SpawnEnemy(gigaCannon, 600, 0);
                break;

            case 319:
                SpawnEnemy(gigaCannon, -600, 0);
                break;

        }
    }
}
