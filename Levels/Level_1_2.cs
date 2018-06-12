using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level_1_2 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject crossCannon_prefab;
    public GameObject mini_prefab;
    public GameObject mothership_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> mothership = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(110);
        SetTotalBeats(4, 4);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_prefab, 25);
        InstantiatePool(crossCannon, crossCannon_prefab, 8);
        InstantiatePool(mini, mini_prefab, 50);
        InstantiatePool(mothership, mothership_prefab, 3);


        //StartAt(340);

        background.ChangeColour(color[1]);
    }

    public override void SpawnMini(float x, float z, Vector3 pos)
    {
        SpawnEnemy(mini, x, z, pos);
    }

    protected override void Spawn(int beat)
    {

        switch (beat)
        {
            default:
                break;


            case 5:
                SpawnCircle(mini, 10, 1100);
                break;

            case 9:
                SpawnCoinCircle(6, 500);
                break;


            case 25:
                SpawnCircle(mini, 4, 900);
                break;

            case 27:
                SpawnCircle(mini, 4, 1000, PI / 4);
                break;

            case 29:
                SpawnCircle(mini, 4, 1100);
                break;

            case 31:
                SpawnCircle(mini, 4, 1200, PI / 4);
                break;

            case 32:
                SpawnCircle(mini, 4, 1300);
                break;



            case 58:
                SpawnCircle(drone, 10, 1200);
                break;



            case 63:
                background.ChangeColour(color[2]);
                break;


            case 73:                
                SpawnCircle(crossCannon, 4, 1100);
                break;

            case 78:
                SpawnCoinCircle(6, 800);
                break;

            case 122:
                SpawnCircle(mini, 16, 1200);
                break;

            case 127:
                background.ChangeColour(color[0]);
                SpawnCoinCircle(16, 1300, PI / 16);
                break;

            case 137:
                SpawnCircle(crossCannon, 4, 900);
                break;


            case 186:
                SpawnEnemy(mothership, 0, 0);
                break;

            case 191:
                background.ChangeColour(color[2]);
                break;

            case 202:
                SpawnCircle(drone, 8, 1200);
                break;


            case 218:
                SpawnCircle(drone, 3, 1000);
                break;

            case 222:
                SpawnCircle(drone, 3, 1000, PI / 3);
                break;

            case 226:
                SpawnCircle(drone, 3, 1000);
                break;

            case 230:
                SpawnCircle(drone, 3, 1000, PI / 3);
                break;


            case 235:
                SpawnCircle(drone, 3, 1000);
                break;

            case 239:
                SpawnCircle(drone, 3, 1000, PI / 3);
                break;

            case 243:
                SpawnCircle(drone, 3, 1000);
                break;

            case 247:
                SpawnCircle(drone, 3, 1000, PI / 3);
                break;


            case 255:
                background.ChangeColour(color[0]);
                SpawnCoinCircle(10, 1000);
                break;




            case 282:
                SpawnCircle(crossCannon, 5, 1200);
                break;

            case 314:
                SpawnEnemy(mothership, 500, 0);
                //SpawnEnemy(mothership, 0, 500);
                break;

            case 319:
                background.ChangeColour(color[1]);
                break;

            case 330:
                SpawnEnemy(mothership, -500, 0);
                break;

        }

        if(beat >= 347)
        {
            if ((beat - 347) % 8 == 0)
                SpawnCircle(drone, 3, 1200, PI / 3);
            else if((beat - 347) % 4 == 0)
                SpawnCircle(drone, 3, 1200);

        }
    }
}


