using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trailer_Level : BaseLevel
{
    public GameObject drone_prefab;
    public GameObject crossCannon_prefab;
    public GameObject crossBomber_prefab;


    List<GameObject> drone = new List<GameObject>();
    List<GameObject> crossCannon = new List<GameObject>();
    List<GameObject> crossBomber = new List<GameObject>();

    public GameObject threeDUI;

    bool camZoomOut = true;

    CameraControl cam;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128);
        SetTotalBeats(4, 30);

        beatTime = 0.5f;
        totalBeats = 540;

        levelType = LevelType.NORMAL;

        corePos = new Vector3(1000, 0, 1000);


        InstantiatePool(drone, drone_prefab, 20);
        InstantiatePool(crossCannon, crossCannon_prefab, 10);
        InstantiatePool(crossBomber, crossBomber_prefab, 1);

        cam = Camera.main.GetComponent<CameraControl>();
    }

    protected override void Start()
    {

        //Screen.showCursor = false;

        Cursor.visible = false;

        //cam.ClearTargets();
        //cam.AddTarget(GameObject.FindWithTag("Core"));
        //cam.offsetDistance = 0.3f; // 1.4;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Application.LoadLevel(0);
        }
    }

    protected override void Spawn(int waveNum)
    {

        switch (waveNum)
        {
            default:
                break;

            case 0:
                SpawnCircle(drone, 6, 1200);
                break;


            case 22:
                SpawnCircle(drone, 4, 700);
                break;

            case 23:
                SpawnCircle(drone, 4, 800, PI / 4);
                break;

            case 24:
                SpawnCircle(drone, 4, 900);
                break;

            case 25:
                SpawnCircle(crossCannon, 4, 1000, PI / 4);
                break;

            case 26:
                SpawnEnemy(crossBomber, 0, 0);
                break;

            case 30:
                SpawnCoinCircle(6, 500);
                background.ChangeColour(color[1]);
                break;

        }
    }
}

