using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_1_1 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject crossCannon_prefab;
    public GameObject crossBomber_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 0);
        //386

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 20);
        InstantiatePool(crossCannon, crossCannon_prefab, 15);
        InstantiatePool(crossBomber, crossBomber_prefab, 3);

        //beatNum = 316;

        //background.ChangeColour(color[0]);





        //StartAt(380);
    }

    protected override void Spawn(int beat)
    {

        switch (beat)
        {
            default:
                break;

            case 0:
                SpawnCircle(drone, 4, 1200);
                break;

            case 24:
                SpawnCircle(drone, 6, 800);
                break;

            case 30:
                SpawnCoinCircle(6, 500);
                background.ChangeColour(color[1]);
                break;

            case 56:
                SpawnCircle(drone, 10, 1000);
                break;

            case 62:
                //SpawnCoinCircle(8, 400);
                background.ChangeColour(color[2]);
                break;

            case 88:
                SpawnCircle(drone, 4, 800);
                break;

            case 94:
                SpawnCircle(drone, 6, 1200);
                SpawnCoinCircle(8, 600, PI / 6);
                background.ChangeColour(color[0]);
                break;

            case 120:
                SpawnCircle(crossCannon, 4, 900);
                break;

            case 126:
                //SpawnCoinCircle(8, 500);
                background.ChangeColour(color[1]);
                break;

            case 154:
                SpawnCircle(drone, 6, 1100);
                break;

            case 160:
                //SpawnCoinCircle(4, 300);
                background.ChangeColour(color[2]);
                break;

            case 191:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                break;
        }

        if (beat >= 191 && beat <= 218)
        {
            SpawnCoin(
                Mathf.Sin((beat - 192) * PI * 2 / 10 + PI / 2) * (300 + (beat - 192) * 15),
                Mathf.Cos((beat - 192) * PI * 2 / 10 + PI / 2) * (300 + (beat - 192) * 15),
                corePos);
        }

        switch (beat)
        {
            default:
                break;

            case 218:
                SpawnCircle(drone, 10, 1100);
                break;

            case 223:
                background.ToggleStars(true);
                break;

            case 250:
                SpawnEnemy(crossBomber, 0, 0);
                break;

            case 256:
                background.ChangeColour(color[2]);
                SpawnCircle(drone, 8, 1200);
                SpawnCoinCircle(8, 1000, PI / 8);
                break;

            case 281:
                SpawnCircle(crossCannon, 4, 1350);
                break;

            case 287:
                background.ChangeColour(color[1]);
                SpawnCoinCircle(6, 350);
                break;

            case 313:
                SpawnEnemy(crossBomber, 0, 850);
                SpawnEnemy(crossBomber, 0, -850);
                break;

            case 319:
                background.ChangeColour(color[0]);
                //SpawnCoinCircle(10, 450, new Vector3(1000, 0, 1650));
                //SpawnCoinCircle(10, 450, new Vector3(1000, 0, 350));
                break;

            //case 345:   
            //    SpawnCircle(drone, 8, 1100);
            //    break;
        }
    }

    public override void LevelComplete()
    {
        Achievements.Instance.Achievment("FIRST_CONTACT");
        base.LevelComplete();
    }
}

