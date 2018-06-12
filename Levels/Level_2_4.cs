using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_4 : BaseLevel
{
    public GameObject triShot_prefab;
    public GameObject crossCannon_prefab;
    public GameObject hWing_prefab;
    public GameObject turtle_prefab;

    List<GameObject> triShot = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> hWing = new List<GameObject>();
    List<GameObject> turtle = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(110);
        SetTotalBeats(4, 24); //438

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(crossCannon, crossCannon_prefab, 12);
        InstantiatePool(triShot, triShot_prefab, 30);
        InstantiatePool(hWing, hWing_prefab, 12);
        InstantiatePool(turtle, turtle_prefab, 5);

        background.ChangeColour(color[0]);

        //StartAt(340);
    }

    protected override void Spawn(int num)
    {
        switch (num)
        {
            default:        
                break;

            case 0:
                SpawnCoinCircle(12, 1100, PI / 12);
                break;

            case 25:
                SpawnCircle(triShot, 6, 1100);
                break;

            case 31:
                background.ChangeColour(color[1]);
                break;

            case 42:
                SpawnCircle(crossCannon, 3, 1600, PI / 6);
                break;

            case 64:
                background.ChangeColour(color[2]);
                break;

            case 58:
                SpawnCircle(hWing, 3, 1200, PI / 6);
                break;


            case 127:
                background.ChangeColour(color[0]);
                background.ToggleStars(false);
                SpawnCoinCircle(12, 1100, PI / 12);
                break;


            case 137:
                SpawnEnemy(triShot, 400, 900);
                SpawnEnemy(triShot, -400, 900);

                SpawnEnemy(triShot, 400, -900);
                SpawnEnemy(triShot, -400, -900);
                break;

            case 143:
                background.ToggleStars(true);
                break;


            case 154:
                SpawnCircle(crossCannon, 6, 1600, PI / 6);
                break;

            case 160:
                background.ChangeColour(color[1]);
                break;

            case 185:
                SpawnCircle(hWing, 6, 1200, PI / 6);
                break;

            case 250:
                SpawnCircle(triShot, 4, 400);
                break;

            case 255:
                background.ChangeColour(color[0]);
                SpawnCoinCircle(12, 1100, PI / 12);
                break;

            case 270:
                SpawnCircle(triShot, 4, 1200, PI / 4);
                break;

            case 287:
                background.ToggleStars(false);
                break;

            case 298:
                SpawnCircle(crossCannon, 6, 1600, PI / 6);
                break;
            
            case 304:
                background.ToggleStars(true);
                SpawnCoinCircle(12, 1100, PI / 12);
                break;

            case 345:
                SpawnEnemy(turtle, 0, 0);
                SpawnCircle(hWing, 4, 1200, PI / 4);
                break;


            case 351:
                background.ChangeColour(color[1]);
                break;

            case 378:
                SpawnCircle(turtle, 4, 1000, PI / 4);
                break;

            case 410:
                SpawnCircle(triShot, 4, 400, new Vector3(0, 0, 900));
                SpawnCircle(triShot, 4, 400, new Vector3(0, 0, -900));
                break;

            case 441:
                SpawnCircle(turtle, 4, 1600, PI / 4);
                break;

        }
    }
}
