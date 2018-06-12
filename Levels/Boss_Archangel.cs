using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Archangel : BaseLevel
{
    public GameObject boss;

    protected override void Awake()
    {
        base.Awake();

        SetBeatTime(128 * music.pitch);

        levelType = LevelType.BOSS;
        Object.FindObjectOfType<BossHealth>().StartBar(boss);
    }
}
