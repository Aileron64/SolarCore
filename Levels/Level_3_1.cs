using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_1: BaseLevel
{
    public GameObject drone_prefab;
    public GameObject mini_prefab;
    public GameObject boxBoy_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> boxBoy = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 15);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 12);
        InstantiatePool(mini, mini_prefab, 20);
        InstantiatePool(boxBoy, boxBoy_prefab, 5);

        BoxSetup();
    }

    void BoxSetup()
    {

        int[,] boxArray = {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 },

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

        BoxManager.Instance.Setup(boxArray);

    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;

            case 5:
                SpawnEnemy(boxBoy, 0, 0);
                break;

            case 15:
                SpawnEnemy(boxBoy, 600, 600);
                SpawnEnemy(boxBoy, -600, 600);
                SpawnEnemy(boxBoy, 600, -600);
                SpawnEnemy(boxBoy, -600, -600);
                break;

            case 20:
                SpawnCoinCircle(8, 600);
                break;


            case 25:
                SpawnCircle(mini, 6, 1000);
                break;

            case 26:
                SpawnCircle(mini, 6, 1050, PI / 8 * 1);
                break;

            case 27:
                SpawnCircle(mini, 6, 1100, PI / 8 * 2);
                break;

            case 28:
                SpawnCircle(mini, 6, 1150, PI / 8 * 3);
                break;

            case 29:
                SpawnCircle(mini, 6, 1200, PI / 8 * 4);
                break;

            case 30:
                SpawnCircle(mini, 6, 1250, PI / 8 * 5);
                break;

            case 50:
                SpawnEnemy(boxBoy, 600, 600);
                SpawnEnemy(boxBoy, -600, 600);
                SpawnEnemy(boxBoy, 600, -600);
                SpawnEnemy(boxBoy, -600, -600);
                break;

            case 55:
                SpawnCoinCircle(8, 400);
                break;

            case 80:
                SpawnEnemy(boxBoy, 0, 500);
                SpawnEnemy(boxBoy, 0, -500);
                SpawnEnemy(boxBoy, 500, 0);
                SpawnEnemy(boxBoy, -500, 0);

                break;

            case 85:
                SpawnCoinCircle(12, 1000);
                break;

            case 95:
                SpawnCircle(mini, 8, 1000);
                break;

            case 96:
                SpawnCircle(mini, 8, 1050, PI / 8 * 1);
                break;

            case 97:
                SpawnCircle(mini, 8, 1100, PI / 8 * 2);
                break;

            case 98:
                SpawnCircle(mini, 8, 1150, PI / 8 * 3);
                break;

            case 99:
                SpawnCircle(mini, 8, 1200, PI / 8 * 4);
                break;

            case 100:
                SpawnCircle(mini, 8, 1250, PI / 8 * 5);
                break;

            //case 110:
            //    SpawnCircle(dronePool, 4, 600);
            //    break;

            //case 115:
            //    SpawnCircle(dronePool, 5, 700);
            //    break;

            //case 120:
            //    SpawnCircle(dronePool, 6, 800);
            //    break;

            case 140:
                SpawnCircle(mini, 8, 1000);
                break;

            case 141:
                SpawnCircle(mini, 8, 1050, PI / 8 * 1);
                break;

            case 142:
                SpawnCircle(mini, 8, 1100, PI / 8 * 2);
                break;

            case 143:
                SpawnCircle(mini, 8, 1150, PI / 8 * 3);
                break;

            case 144:
                SpawnCircle(mini, 8, 1200, PI / 8 * 4);
                break;

            case 145:
                SpawnCircle(mini, 8, 1250, PI / 8 * 5);
                break;

            case 146:
                SpawnCircle(mini, 8, 1300, PI / 8 * 6);
                break;

            case 147:
                SpawnCircle(mini, 8, 1350, PI / 8 * 7);
                break;

            case 148:
                SpawnCircle(mini, 8, 1400, PI / 8 * 8);
                break;

            case 149:
                SpawnCircle(mini, 8, 1450, PI / 8 * 9);
                break;

            case 150:
                SpawnCircle(mini, 8, 1500, PI / 8 * 10);
                break;

            case 170:
                SpawnEnemy(boxBoy, 600, 600);
                SpawnEnemy(boxBoy, -600, 600);
                SpawnEnemy(boxBoy, 600, -600);
                SpawnEnemy(boxBoy, -600, -600);
                break;


            case 175:
                SpawnCoinCircle(8, 400);
                break;

            case 180:
                SpawnCircle(mini, 8, 1000);
                break;

            case 181:
                SpawnCircle(mini, 8, 1050, PI / 8 * 1);
                break;

            case 182:
                SpawnCircle(mini, 8, 1100, PI / 8 * 2);
                break;

            case 183:
                SpawnCircle(mini, 8, 1150, PI / 8 * 3);
                break;

            case 184:
                SpawnCircle(mini, 8, 1200, PI / 8 * 4);
                break;

            case 185:
                SpawnCircle(mini, 8, 1250, PI / 8 * 5);
                break;

            case 186:
                SpawnCircle(mini, 8, 1300, PI / 8 * 6);
                break;

            case 187:
                SpawnCircle(mini, 8, 1350, PI / 8 * 7);
                break;

            case 188:
                SpawnCircle(mini, 8, 1400, PI / 8 * 8);
                break;

            case 189:
                SpawnCircle(mini, 8, 1450, PI / 8 * 9);
                break;

            case 190:
                SpawnCircle(mini, 8, 1500, PI / 8 * 10);
                break;


            case 230:
                SpawnEnemy(boxBoy, 0, 500);
                SpawnEnemy(boxBoy, 0, -500);
                SpawnEnemy(boxBoy, 500, 0);
                SpawnEnemy(boxBoy, -500, 0);
                break;


            case 235:
                SpawnCoinCircle(8, 400);
                break;

            case 250:
                SpawnCircle(drone, 4, 800);
                break;

            case 251:
                SpawnCircle(drone, 4, 900, PI / 4 * 1);
                break;

            case 252:
                SpawnCircle(drone, 4, 1000);
                break;

            case 300:
                SpawnEnemy(boxBoy, 600, 600);
                SpawnEnemy(boxBoy, -600, 600);
                SpawnEnemy(boxBoy, 600, -600);
                SpawnEnemy(boxBoy, -600, -600);
                break;



            case 301:
                SpawnEnemy(boxBoy, 0, 500);
                SpawnEnemy(boxBoy, 0, -500);
                SpawnEnemy(boxBoy, 500, 0);
                SpawnEnemy(boxBoy, -500, 0);
                break;


            case 305:
                SpawnCoinCircle(8, 400);
                break;

            case 306:
                SpawnCoinCircle(12, 1000);
                break;

            case 330:
                SpawnCircle(drone, 6, 800);
                break;

            case 331:
                SpawnCircle(drone, 6, 900, PI / 4 * 1);
                break;

            case 332:
                SpawnCircle(drone, 6, 1000);
                break;

            case 333:
                SpawnCircle(drone, 6, 1100, PI / 4 * 1);
                break;

            case 334:
                SpawnCircle(drone, 6, 1200);
                break;
        }
    }
}
