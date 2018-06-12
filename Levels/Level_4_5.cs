using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_4_5 : BaseLevel
{  
    public GameObject drone_prefab;
    public GameObject phycho_prefab;
    public GameObject turtle_prefab;
    public GameObject mini_prefab;
    public GameObject motherShip_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> phycho = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> motherShip = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128); 
        SetTotalBeats(3, 0); //384

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);


        InstantiatePool(drone, drone_prefab, 16);
        InstantiatePool(phycho, phycho_prefab, 8);

        InstantiatePool(turtle, turtle_prefab, 4);
        InstantiatePool(mini, mini_prefab, 10);

        InstantiatePool(motherShip, motherShip_prefab, 4);
    }

    public override void SpawnMini(float x, float z, Vector3 pos)
    {
        SpawnEnemy(mini, x, z, pos);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {


            case 15:
                SpawnCircle(drone, 6, 600);
                break;

            case 35:
                SpawnCircle(drone, 12, 900);
                break;

            case 55:
                SpawnEnemy(turtle, 0, 0);
                break;

            case 60:
                SpawnCoinCircle(4, 300);
                break;

            case 75:
                SpawnCircle(drone, 10, 1000);
                break;

            case 115:
                SpawnCircle(turtle, 4, 1100);
                break;

            case 120:
                SpawnCoinCircle(4, 1100, 4, 300, 0);
                SpawnCircle(drone, 10, 400);
                break;

            case 124:
                SpawnEnemy(motherShip, 1100, 1100);
                SpawnEnemy(motherShip, -1100, 1100);
                SpawnEnemy(motherShip, 1100, -1100);
                SpawnEnemy(motherShip, -1100, -1100);
                break;

            case 130:
                SpawnCoinCircle(12, 900);
                break;


            case 200:
                SpawnCircle(mini, 20, 900);
                break;


            case 240:
                SpawnRandom(phycho, 3, 800);
                break;

            case 245:
                SpawnRandom(phycho, 3, 800);
                break;

            case 250:
                SpawnRandom(phycho, 4, 800);
                break;

            //case 280:
            //    SpawnCircle(mini, 20, 900);
            //    break;





            case 305:
                SpawnEnemy(turtle, 0, 0);
                SpawnCircle(turtle, 4, 850);
                break;

            case 310:
                SpawnCoinCircle(4, 850, 4, 300, 0);
                SpawnCoinCircle(4, 300);
                SpawnCircle(drone, 14, 700);
                break;


            case 314:

                SpawnEnemy(motherShip, 1250, 1250);
                SpawnEnemy(motherShip, -1250, 1250);
                SpawnEnemy(motherShip, 1250, -1250);
                SpawnEnemy(motherShip, -1250, -1250);


                //SpawnEnemy(motherShip, 1300, 900);
                //SpawnEnemy(motherShip, 900, 1300);

                //SpawnEnemy(motherShip, -1300, 900);
                //SpawnEnemy(motherShip, -900, 1300);

                //SpawnEnemy(motherShip, 1300, -900);
                //SpawnEnemy(motherShip, 900, -1300);

                //SpawnEnemy(motherShip, -1300, -900);
                //SpawnEnemy(motherShip, -900, -1300);
                break;

            case 320:
                SpawnCoinCircle(20, 1000);
                break;
            //case 340:
            //    SpawnCircle(drone, 12, 400);
            //    break;
        }
    }
}
