using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_3 : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject mini_prefab;
    public GameObject motherShip_prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();
    List<GameObject> motherShip = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(126);
        SetTotalBeats(2, 29.5f); // 324

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);


        InstantiatePool(drone, drone_prefab, 22);
        InstantiatePool(mini, mini_prefab, 100);
        InstantiatePool(motherShip, motherShip_prefab, 6);

        //  StartAt(310);
    }

    public override void SpawnMini(float x, float z, Vector3 pos)
    {
        SpawnEnemy(mini, x, z, pos);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            case 10:
                SpawnCircle(mini, 10, 600);
                SpawnCoinCircle(6, 300);
                break;

            case 30:
                SpawnCircle(mini, 8, 300, new Vector3(0, 0, 800));
                SpawnCircle(mini, 8, 300, new Vector3(0, 0, -800));
                break;

            case 35:
                SpawnCoinCircle(4, 200, new Vector3(1000, 0, 1800));
                SpawnCoinCircle(4, 200, new Vector3(1000, 0, 200));
                break;

            case 55:
                SpawnCoinCircle(6, 300);
                SpawnCircle(drone, 6, 450, PI / 6);
                SpawnEnemy(motherShip, 0, 1600);
                SpawnEnemy(motherShip, 0, -1600);
                break;

            case 61:
                background.ChangeColour(color[1]);
                break;

            case 73:
                SpawnCircle(drone, 6, 450, PI / 6);
                break;

            case 87:
                SpawnCircle(mini, 12, 400, new Vector3(0, 0, 1600));
                SpawnCircle(mini, 12, 400, new Vector3(0, 0, -1600));
                break;

            case 120:
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, 1000), -PI / 2);
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, -1000), PI / 2);

                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, 1000), -PI * 5 / 8);
                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, -1000), PI * 3 / 8);
                break;

            case 125:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                SpawnCoinCircle(5, 200, new Vector3(1000, 0, 1800));
                SpawnCoinCircle(5, 200, new Vector3(1000, 0, 200));
                break;

            case 152:
                SpawnEnemy(motherShip, 0, 1900);
                SpawnEnemy(motherShip, 0, -1900);
                break;

            case 157:
                background.ToggleStars(true);
                break;

            case 183:
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, 1000), -PI / 2);
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, -1000), PI / 2);

                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, 1000), -PI * 5 / 8);
                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, -1000), PI * 3 / 8);

                SpawnHalfCircle(drone, 3, 4, 650, new Vector3(0, 0, 1000), -PI / 2);
                SpawnHalfCircle(drone, 3, 4, 650, new Vector3(0, 0, -1000), PI / 2);
                break;

            case 189:
                SpawnCoinCircle(6, 300);
                background.ChangeColour(color[1]);
                break;

            case 213:
                SpawnHalfCircle(mini, 3, 4, 250, new Vector3(0, 0, 1000), -PI / 2);
                SpawnHalfCircle(mini, 3, 4, 250, new Vector3(0, 0, -1000), PI / 2);

                SpawnHalfCircle(mini, 6, 8, 500, new Vector3(0, 0, 1000), -PI * 5 / 8);
                SpawnHalfCircle(mini, 6, 8, 500, new Vector3(0, 0, -1000), PI * 3 / 8);

                SpawnHalfCircle(mini, 3, 4, 650, new Vector3(0, 0, 1000), -PI / 2);
                SpawnHalfCircle(mini, 3, 4, 650, new Vector3(0, 0, -1000), PI / 2);
                break;

            case 245:
                SpawnCircle(mini, 10, 600);

                SpawnCoinCircle(6, 300);

                SpawnEnemy(motherShip, 350, 1650);
                SpawnEnemy(motherShip, -350, 1650);

                SpawnEnemy(motherShip, 350, -1650);
                SpawnEnemy(motherShip, -350, -1650);
                break;

            case 251:
                background.ChangeColour(color[2]);
                SpawnCoinCircle(4, 200, new Vector3(1000, 0, 1800));
                SpawnCoinCircle(4, 200, new Vector3(1000, 0, 200));
                break;

            case 277:
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, 2100), -PI / 2);
                SpawnHalfCircle(drone, 3, 4, 250, new Vector3(0, 0, -2100), PI / 2);

                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, 2100), -PI * 5 / 8);
                SpawnHalfCircle(drone, 6, 8, 500, new Vector3(0, 0, -2100), PI * 3 / 8);

                SpawnHalfCircle(drone, 3, 4, 650, new Vector3(0, 0, 2100), -PI / 2);
                SpawnHalfCircle(drone, 3, 4, 650, new Vector3(0, 0, -2100), PI / 2);
                break;

        }
    }
}
