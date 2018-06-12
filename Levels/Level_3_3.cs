using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_3 : BaseLevel
{
    public GameObject drone_Prefab;
    public GameObject phyco_Prefab;
    public GameObject mini_Prefab;

    List<GameObject> drone = new List<GameObject>();
    List<GameObject> phyco = new List<GameObject>();
    List<GameObject> mini = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(3, 0);

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(drone, drone_Prefab, 12);
        InstantiatePool(phyco, phyco_Prefab, 12);
        InstantiatePool(mini, mini_Prefab, 12);
        //InstantiatePool(crossCannon, crossCannon_Prefab, 12);

        background.ChangeColour(color[0]);
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;

            case 8:
                SpawnCircle(phyco, 3, 800);
                break;

            case 24:
                SpawnRandom(phyco, 5, 800);
                SpawnCoinRandom(5, 800);
                break;

            case 30:
                background.ChangeColour(color[1]);
                break;

            case 59:
                SpawnCircle(mini, 12, 1200);
                break;

            case 64:
                background.ChangeColour(color[2]);
                break;

            case 88:
                SpawnRandom(phyco, 6, 800);
                SpawnCoinRandom(5, 800);
                break;

            case 94:
                background.ChangeColour(color[1]);
                break;

            case 123:
                SpawnCircle(mini, 16, 1000);
                break;

            case 128:
                background.ChangeColour(color[0]);
                break;

            case 138:
                SpawnRandom(phyco, 7, 800);
                SpawnCoinRandom(5, 800);
                break;

            case 144:
                
                break;

            case 164:
                SpawnCircle(drone, 8, 950);
                break;

            case 171:
                background.ChangeColour(color[0]);
                break;

            case 186:
                SpawnRandom(phyco, 8, 800);
                SpawnCoinRandom(5, 800);
                break;

            case 192:
                background.ChangeColour(color[2]);
                break;

            case 220:
                SpawnCircle(mini, 20, 1000);
                break;

            case 240:
                SpawnRandom(phyco, 9, 800);
                SpawnCoinRandom(5, 800);
                break;
   
            case 270:
                SpawnCircle(drone, 10, 1000);
                break;

            case 315:
                SpawnRandom(phyco, 10, 800);
                SpawnCoinRandom(5, 800);
                break;

            case 320:
                background.ChangeColour(color[0]);
                break;

            case 340:
                SpawnRandom(phyco, 11, 1000);
                SpawnCoinRandom(5, 800);
                break;
        }
    }
}