using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hive : BaseLevel
{
    public GameObject boss;

    public GameObject beePrefab;
    public GameObject waspPrefab;
    public GameObject bumbleBeePrefab;
    public GameObject hiveBombPrefab;

    public List<GameObject> bee = new List<GameObject>();
    public List<GameObject> wasp = new List<GameObject>();
    public List<GameObject> bumbleBee = new List<GameObject>();
    public List<GameObject> hiveBomb = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(140);
        levelType = LevelType.BOSS;

        corePos = new Vector3(1000, 0, 1000);

        Object.FindObjectOfType<BossHealth>().StartBar(boss);

        InstantiatePool(bee, beePrefab, 40);
        InstantiatePool(wasp, waspPrefab, 20);
        InstantiatePool(bumbleBee, bumbleBeePrefab, 12);
        InstantiatePool(hiveBomb, hiveBombPrefab, 12);

        //StartAt(480);
    }





}
