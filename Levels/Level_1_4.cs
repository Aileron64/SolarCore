using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_4 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject turtle_prefab;
    public GameObject mini_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(2, 39); //384

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 25);
        InstantiatePool(turtle, turtle_prefab, 8);
        InstantiatePool(mini, mini_prefab, 30);

        //StartAt(186);
    }

    protected override void Spawn(int waveNum)
    {

        int ringSize = 4;

        switch (waveNum)
        {
            case 0:
                SpawnEnemy(turtle, 0, 0);
                break;

            case 6:
                SpawnCoinCircle(ringSize, 300);
                break;

            case 24:
                SpawnCircle(mini, 14, 1600);
                break;

            case 58:
                SpawnCircle(turtle, 4, 900, PI / 4);
                break;

            case 64:
                background.ToggleStars(false);
                SpawnCoinCircle(4, 900, ringSize, 300, PI / 4);
                break;

            case 95:
                background.ToggleStars(true);
                break;


            case 123:
                SpawnCircle(drone, 6, 1200, PI / 4);
                break;

            case 124:
                SpawnCircle(drone, 6, 1300);
                break;

            case 125:
                SpawnCircle(drone, 6, 1400, PI / 4);
                break;


            case 153:
                SpawnCircle(turtle, 4, 1000);
                break;

            case 159:
                background.ChangeColour(color[1]);
                SpawnCircle(mini, 14, 1600);
                break;



            case 190:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                break;


        }

        if (waveNum >= 191 && waveNum <= 222)
        {
            if(waveNum % 2 == 1)
            {
                SpawnCoin(
                    Mathf.Sin((waveNum - 192) * PI * 2 / 20) * (400 + (waveNum - 192) * 15),
                    Mathf.Cos((waveNum - 192) * PI * 2 / 20) * (400 + (waveNum - 192) * 15),
                    corePos);
            }
            else
            {
                SpawnCoin(
                    Mathf.Sin((waveNum - 192) * PI * 2 / 20 + PI) * (400 + (waveNum - 192) * 15),
                    Mathf.Cos((waveNum - 192) * PI * 2 / 20 + PI) * (400 + (waveNum - 192) * 15),
                    corePos);
            }

        }
        else if (waveNum >= 223 && waveNum <= 254)
        {
            if (waveNum % 2 == 1)
            {
                SpawnEnemy(mini,
                    Mathf.Sin((waveNum - 192) * PI * 2 / 20) * (400 + (waveNum - 192) * 15),
                    Mathf.Cos((waveNum - 192) * PI * 2 / 20) * (400 + (waveNum - 192) * 15));
            }
            else
            {
                SpawnEnemy(mini,
                    Mathf.Sin((waveNum - 192) * PI * 2 / 20 + PI) * (400 + (waveNum - 192) * 15),
                    Mathf.Cos((waveNum - 192) * PI * 2 / 20 + PI) * (400 + (waveNum - 192) * 15));
            }
        }

        switch (waveNum)
        {
            default:
                break;

            case 223:
                background.ToggleStars(true);
                break;

            case 250:
                SpawnCircle(drone, 6, 1200, PI / 4);
                break;

            case 251:
                SpawnCircle(drone, 6, 1300);
                break;

            case 252:
                SpawnCircle(drone, 6, 1400, PI / 4);
                break;

            case 255:
                background.ChangeColour(color[1]);
                break;


            case 281:
                SpawnCircle(turtle, 8, 1200);
                break;


            case 297:
                SpawnCircle(mini, 14, 1000);
                break;

            case 319:
                background.ChangeColour(color[0]);
                break;

        }


                //case 170:
                //    SpawnCircle(mini, 4, 800);
                //    break;

                //case 208:
                //    SpawnCircle(drone, 6, 1200, PI / 4);
                //    break;

                //case 209:
                //    SpawnCircle(drone, 6, 1300);
                //    break;

                //case 210:
                //    SpawnCircle(drone, 6, 1400, PI / 4);
                //    break;

                //case 240:
                //    SpawnCircle(turtle, 4, 800);
                //    break;

                //case 245:
                //    SpawnCoinCircle(4, 800, ringSize, 300, 0);
                //    break;

                //case 288:
                //    SpawnCircle(drone, 6, 1200, PI / 4);
                //    break;

                //case 289:
                //    SpawnCircle(drone, 6, 1300);
                //    break;

                //case 290:
                //    SpawnCircle(drone, 6, 1400, PI / 4);
                //    break;

                //case 320:
                //    SpawnCircle(turtle, 8, 1200);
                //    break;

                //case 325:
                //    SpawnCoinCircle(8, 1200, ringSize, 300, 0);
                //    break;

                //case 348:
                //    SpawnCircle(drone, 6, 750, PI / 4);
                //    break;

                //case 349:
                //    SpawnCircle(drone, 6, 850);
                //    break;

                //case 350:
                //    SpawnCircle(drone, 6, 950, PI / 4);
                //    break;
        }
}
