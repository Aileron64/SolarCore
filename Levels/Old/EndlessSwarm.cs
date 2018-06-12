using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSwarm : BaseLevel
{
    const int TOTAL_POINTS = 5;
    static int pointsLeft;

    float timer;
    float delay = 25;

    const int TOTAL_WAVES = 7;
    int wave = 0;
    int phase = 1;

    float offset = 0;

    public GameObject mini;

    List<GameObject> miniPool = new List<GameObject>();

    GameObject[] enemies;

    void Start()
    {
        //wave = Random.Range(1, 8);
        beatTime = 0.47f;
        //levelID = 101;
        timer = delay;

        totalBeats = 10;

        levelType = LevelType.NONE;

        corePos = new Vector3(1000, 0, 1000);

        InstantiatePool(miniPool, mini, 50);
    }

    protected override void Spawn(int waveNum)
    {
        SpawnCircle(miniPool, 14, 1800, offset);

        offset += 0.1f;
    }




}
