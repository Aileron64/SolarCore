using UnityEngine;
using System.Collections;

public class Boss_Level : BaseLevel
{
    public GameObject boss;

    protected override void Awake()
    {
        base.Awake();

        Object.FindObjectOfType<BossHealth>().StartBar(boss);
        levelType = LevelType.BOSS;
        SetBeatTime(128);

        corePos = new Vector3(1000, 0, 1000);
    }
}
