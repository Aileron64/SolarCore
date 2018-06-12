using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
using Steamworks;


public class CubeGod : BaseEnemy
{
    public GameObject[] part;
    public GameObject[] connector;

    bool flag;

    CubeGrid grid;

    public bool testMode;

    BaseLevel LM;

    override protected void Start()
    {
        stunImmune = true;
        timeImmune = true;

        base.Start();

        grid = GetComponent<CubeGrid>();

        LM = BaseLevel.Instance;
        LM.StartAt(350);

        if (!Application.isEditor)
            testMode = false;

        if (!testMode)
            grid.gridState = GridState.OFF;

        if (cameraInfluence != 0)
            Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(
                transform, cameraInfluence, cameraInfluence, 0f);
    }

    override protected void Normal()
    {

        transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);

    }

    override protected void OnBeat()
    {
        for (int i = 0; i < part.Length; i++)
        {
            Vector3 pos1 = part[i].transform.localPosition.normalized;
            Vector3 pos2 = connector[i].transform.localPosition.normalized;

            if (flag)
                pos1 *= 2;         
            else
                pos1 *= 5;

            pos2 *= Mathf.Abs(pos1.x);

            part[i].transform.DOLocalMove(pos1, 0.2f);
            connector[i].transform.DOLocalMove(pos2, 0.2f);
        }

        flag = !flag;

        if(!testMode)
            OnBeat(BaseLevel.Instance.GetBeatNum());
    }

    void OnBeat(int num)
    {
        switch (num)
        {
            default:
                break;

            case 0:
                grid.randomAmount = 2;
                grid.randomStickyAmount = 0;
                grid.gridState = GridState.RANDOM;
                break;

            case 56:
                grid.randomAmount = 5;
                break;
            
            case 63:
                grid.randomAmount = 12;
                break;

            case 95:
                grid.randomAmount = 16;
                break;

            case 120:
                grid.randomAmount = 2;
                break;

            case 159:
                grid.randomAmount = 6;
                break;

            case 191:
                grid.gridState = GridState.TUNNEL_RANDOM;
                break;

            case 312:
                grid.gridState = GridState.OFF;
                break;

            case 350:
                grid.randomAmount = 0;
                grid.randomStickyAmount = 1;
                grid.gridState = GridState.RANDOM;
                break;

            case 383:
                grid.randomAmount = 4;
                break;

            case 445:
                grid.randomAmount = 8;
                break;
            

            case 510:
                grid.randomAmount += 5;
                Music.Instance.GetComponent<AudioSource>().time = 448 * LM.GetBeatTime();
                BaseLevel.Instance.SetBeatNum(448);
                break;
        }

        // 640 Enrage
    }




    protected override void ChangeMat(Material mat)
    {

        for (int i = 0; i < 4; i++)
        {
            part[i].GetComponent<Renderer>().material = mat;
            connector[i].GetComponent<Renderer>().material = mat;
        }
    }


    public override void Explode()
    {
        Achievements.Instance.Achievment("CUBE_GOD");
        //target.GetComponent<Player>().LevelComplete();

        //Object.FindObjectOfType<GameMan>().WinState();
        BaseLevel.Instance.LevelComplete();

        base.Explode();
    }

}
