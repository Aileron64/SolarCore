using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FalseProphet : BaseEnemy
{
    public int phase;

    public GameObject[] cube;
    public GameObject camPoint;
    GameObject drone;

    const float PI = 3.145f;
    public GameObject blast_prefab;
    public GameObject attack;

    float[] blastAngle = new float[10];
    float[] radius = new float[10];

    int count = 0;

    public FalseAttack.AttackType testAttackType;

    BaseLevel LM;

    bool flag;

    int beatNum;

    List<GameObject> blast = new List<GameObject>();

    override protected void Start()
    {

        stunImmune = true;
        timeImmune = true;


        LM = BaseLevel.Instance;

        drone = Resources.Load("Prefabs/Enemies/MiniDrone") as GameObject;

        //Camera.main.GetComponent<CameraControl>().AddTarget(camPoint);

        Pooler.Instantiate(blast, blast_prefab, 150);
        //LM.StartAt(190);
        //beatNum = 190;

        base.Start();
    }

    override protected void Normal()
    {


        //if (health / maxHealth <= 0.8f && phase == 1)
        //{
        //    phase = 2;
        //    count = 0;
        //}            
        //else if (health / maxHealth <= 0.6f && phase == 2)
        //{
        //    phase = 3;
        //    count = 0;
        //}
        //else if (health / maxHealth <= 0.45f && phase == 3)
        //{
        //    phase = 4;
        //    count = 0;
        //}
        //else if (health / maxHealth <= 0.3f && phase == 4)
        //{
        //    phase = 5;
        //    count = 0;
        //}

        transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime * timeDilation);
    }

    public void SpawnBlast(Vector3 pos)
    {
        GameObject clone = Pooler.GetObject(blast, pos, transform.rotation);

        //GameObject clone = Instantiate(blast_prefab, pos, Quaternion.identity) as GameObject;
    }

    protected override void OnBeat()
    {

        for(int i = 0; i < cube.Length; i++)
        {
            Vector3 pos = cube[i].transform.localPosition.normalized;

            if (flag)
                pos *= 40;
            else
                pos *= 43;

            pos.y = 5;

            cube[i].transform.DOLocalMove(pos, 0.2f);

            //iTween.MoveTo(cube[i], iTween.Hash(
            //    "position", pos, 
            //    "islocal", true, 
            //    "time", 0.9f));
        }

        flag = !flag;


        OnBeat(beatNum);
        beatNum++;


        if(phase == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                float speed = 150;
                float radius = (350 + 250 * i);

                if (i % 2 == 0)
                    blastAngle[i] += Mathf.Sin(speed / radius);
                else
                    blastAngle[i] -= Mathf.Sin(speed / radius);


                for (int j = 0; j < 4; j++)
                {
                    float angle = blastAngle[i] + PI / 2 * j;

                    //Vector3 bombPos = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle) * radius;

                    SpawnBlast(transform.position
                        + (new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius));

                    //GameObject clone = Instantiate(blast_prefab, transform.position
                    //    + (new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius),
                    //    Quaternion.identity) as GameObject;
                }
            }
        } 

    }

    void OnBeat(int num)
    {
        switch (num)
        {
            default:
                break;

            case 5:
            case 15:
            case 25:
            case 35:
            case 83:
            case 99:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_0);
                break;

            case 10:
            case 20:
            case 30:
            case 40:
            case 91:
            case 107:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_1);
                break;

            case 45:
            case 55:
            case 65:
            case 75:
            case 88:
            case 104:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_2);
                break;

            case 50:
            case 60:
            case 70:
            case 80:
            case 96:
            case 112:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_3);
                break;

            case 117:
                LM.SpawnCoinCircle(8, 850);
                LM.SpawnCoinCircle(8, 1000, PI / 8);
                LM.SpawnCoinCircle(8, 1150);
                break;
            

            case 125:
                phase = 1;
                break;

            case 188:
                phase = 0;
                //InstantiateAttack(FalseAttack.AttackType.CIRCLE_IN_0);
                break;

            case 190:
            case 200:
            case 210:
            case 239:
            case 268:
            case 291:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_IN_0);
                break;

            case 195:
            case 205:
            case 215:
            case 227:
            case 277:
            case 301:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_IN_1);
                break;

            case 222:
            case 273:
            case 296:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_2);
                break;

            case 234:
            case 282:
            case 306:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_3);
                break;

            case 244:
            case 252:
            case 260:
            case 286:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_0);
                break;

            case 248:
            case 256:
            case 264:
            case 300:
                InstantiateAttack(FalseAttack.AttackType.CIRCLE_OUT_1);
                break;

            case 308:
                LM.SpawnCoinCircle(8, 850);
                LM.SpawnCoinCircle(8, 1000, PI / 8);
                LM.SpawnCoinCircle(8, 1150);
                break;

            case 316:
                phase = 1;
                break;

            case 318:        
                Music.Instance.GetComponent<AudioSource>().time -= 192 * LM.GetBeatTime();
                beatNum -= 192;
                break;
        }
    }


    void InstantiateAttack(FalseAttack.AttackType attackType)
    {
        GameObject clone = Instantiate(attack, transform.position, Quaternion.identity) as GameObject;

        clone.GetComponent<FalseAttack>().type = attackType;
        clone.GetComponent<FalseAttack>().target = target;
    }


    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        for (int i = 0; i <= 3; i++)
        {
            cube[i].GetComponent<Renderer>().material = mat;
        }
    }

    public override void Explode()
    {
        Achievements.Instance.Achievment("FALSE_PROPHET");
        BaseLevel.Instance.LevelComplete();       
        base.Explode();
    }


}
