using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class HiveMind : BaseEnemy
{


    int phaseNum = 0;

    Boss_Hive LM;

    public GameObject[] cannon = new GameObject[4];

    //List<GameObject> blobs = new List<GameObject>();

    float time_r;
    float radius;
    const float PI = 3.14f;

    const float BOMB_DISTANCE = 1300;

    public Ease testEase;
    bool loopToggle = false;
    int loopCount = 0;

    protected override void Start()
    {

        stunImmune = true;
        timeImmune = true;
        forceImmune = true;

        LM = Object.FindObjectOfType<Boss_Hive>();


        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(transform, 0.1f, 0.1f, 1f);

        base.Start();
    }


    protected override void Normal()
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }


    protected override void OnBeat()
    {
        OnBeat(BaseLevel.Instance.GetBeatNum());

        //count++;

        //if(count >= 17)
        //{
        //    SpawnEnemies(Random.Range(0, 2));

        //    //SpawnEnemies(1);

        //    count = 0;
        //}
    }


    void OnBeat(int num)
    {
        switch (num)
        {
            default:
                break;

            ///////////// PHASE ONE /////////////

            case 5:
                LM.SpawnCoinCircle(8, 950, PI / 8);
                break;

            case 25:
                LM.SpawnCircle(LM.bee, 8, 800);
                break;

            case 29:
                LM.SpawnCircle(LM.bee, 8, 1050);
                break;

            case 34:
                LM.SpawnCircle(LM.bee, 8, 1300);
                break;

            case 57:
                LM.SpawnCircle(LM.wasp, 8, 1200);
                break;

            case 65:
                LM.SpawnCoinCircle(8, 1400, PI / 8);
                LM.SpawnCircle(LM.wasp, 8, 1500);
                break;

            case 89:
                LM.SpawnCircle(LM.bumbleBee, 4, 1500);
                break;

            case 122:
                LM.SpawnCircle(LM.bee, 8, 800);
                break;

            case 127:
                LM.SpawnCircle(LM.bee, 8, 650);
                break;

            case 153:
                LM.SpawnCircle(LM.wasp, 4, 1000);             
                break;

            case 154:
                LM.SpawnCircle(LM.wasp, 4, 1250, PI / 4);
                break;

            case 155:
                LM.SpawnCircle(LM.wasp, 4, 1500);
                break;

            case 160:
                LM.SpawnCoinCircle(8, 600);
                break;

            case 185:
                LM.SpawnCircle(LM.bee, 8, 800);
                break;

            case 188:
                LM.SpawnCircle(LM.bee, 8, 1050);
                break;

            case 191:
                LM.SpawnCircle(LM.bee, 8, 1300);
                break;

            case 218:
                LM.SpawnCircle(LM.bumbleBee, 4, 1300);
                break;



            ///////////// PHASE TWO /////////////

            case 283:
                // 287
                float doTime = 2.1f;

                for(int i = 0; i < 4; i++)
                {
                    cannon[i].GetComponent<BoxCollider>().enabled = true;
                    cannon[i].tag = "Enemy";

                    cannon[i].transform.DOLocalRotate(new Vector3(-110, 0, 0), doTime, 
                        RotateMode.LocalAxisAdd).SetEase(testEase);
                }

                cannon[0].transform.DOLocalMove(new Vector3(32, 2, 0), doTime).SetEase(testEase);
                cannon[1].transform.DOLocalMove(new Vector3(0, 2, -32), doTime).SetEase(testEase);
                cannon[2].transform.DOLocalMove(new Vector3(-32, 2, 0), doTime).SetEase(testEase);
                cannon[3].transform.DOLocalMove(new Vector3(0, 2, 32), doTime).SetEase(testEase);


                break;

            case 287:
                for (int i = 0; i < 4; i++)
                {
                    cannon[i].GetComponent<HiveCannon>().ActivateLaser();
                }
                break;


            case 300:
                LM.SpawnCoinCircle(8, 950, PI / 8);
                break;

            case 311:
                LM.SpawnCircle(LM.wasp, 5, 1000);
                break;

            case 312:
                LM.SpawnCircle(LM.wasp, 5, 1250, PI / 5);
                break;

            case 313:
                LM.SpawnCircle(LM.wasp, 5, 1500);
                break;


            case 345:
                LM.SpawnCircle(LM.bumbleBee, 6, 1450);
                break;



            case 375:
                LM.SpawnCircle(LM.bee, 8, 900);
                break;

            case 376:
                LM.SpawnCircle(LM.bee, 8, 1150, PI / 8);
                break;

            case 377:
                LM.SpawnCircle(LM.bee, 8, 1400);
                break;

            ///////////// PHASE THREE /////////////

            case 399:
                for (int i = 0; i < 4; i++)
                {
                    if (cannon[i].activeInHierarchy)
                        cannon[i].GetComponent<HiveCannon>().Explode(false);
                }
                break;

            // 400 -> 480 Bellow //

            // 487 // Intensify
            case 481:
                LM.SpawnCircle(LM.hiveBomb, 6 + loopCount * 2, BOMB_DISTANCE);
                break;

            case 487:
                LM.SpawnCoinCircle(6 + loopCount * 2, 1000, PI / (6 + loopCount * 2));
                break;

            case 515:
                LM.SpawnCircle(LM.hiveBomb, 6 + loopCount * 2, BOMB_DISTANCE, PI / (6 + loopCount * 2));
                break;

            case 543:
                LM.SpawnCircle(LM.wasp, 5 + loopCount, 700);
                break;

            case 544:
                LM.SpawnCircle(LM.wasp, 5 + loopCount, 1000, PI / (5 + loopCount));
                break;

            case 545:
                LM.SpawnCircle(LM.wasp, 5 + loopCount, 1300);
                break;

            case 575:
                LM.SpawnCircle(LM.bee, 8 + loopCount * 2, 600);
                break;

            case 576:
                LM.SpawnCircle(LM.bee, 8 + loopCount * 2, 900, PI / (8 + loopCount * 2));
                break;

            case 577:
                LM.SpawnCircle(LM.bee, 8 + loopCount * 2, 1200);
                break;

            case 580:
                LM.SpawnCircle(LM.bumbleBee, 4 + loopCount * 2, 1400);
                break;

            ///////////////// LOOP ////////////////

            case 615:
                loopToggle = true;
                loopCount++;
                Music.Instance.GetComponent<AudioSource>().time = 416 * LM.GetBeatTime();
                BaseLevel.Instance.SetBeatNum(416);
                break;
        }

        if (num >= 400 && num < 480 && !loopToggle)
            Phase3(num);
        else if (num >= 416 && num < 460 && loopToggle)
            PhaseLoop(num);
    }

    void Phase3(int num)
    {
        switch (num)
        {
            default:
                break;

            // 415 // First Bombs
            case 409:
                LM.SpawnEnemy(LM.hiveBomb, -BOMB_DISTANCE, 0);
                break;

            case 417:
                LM.SpawnEnemy(LM.hiveBomb, BOMB_DISTANCE, 0);
                break;

            case 425:
                LM.SpawnEnemy(LM.hiveBomb, 0, -BOMB_DISTANCE);
                break;

            case 433:
                LM.SpawnEnemy(LM.hiveBomb, 0, BOMB_DISTANCE);
                break;

            case 441:
                LM.SpawnEnemy(LM.hiveBomb, BOMB_DISTANCE, 0);
                LM.SpawnEnemy(LM.hiveBomb, -BOMB_DISTANCE, 0);
                break;

            case 449:
                LM.SpawnEnemy(LM.hiveBomb, 0, BOMB_DISTANCE);
                LM.SpawnEnemy(LM.hiveBomb, 0, -BOMB_DISTANCE);
                break;

            case 457:              
                LM.SpawnEnemy(LM.hiveBomb, -BOMB_DISTANCE, 0);
                LM.SpawnEnemy(LM.hiveBomb, BOMB_DISTANCE, 0);
                break;

            case 465:
                LM.SpawnEnemy(LM.hiveBomb, 0, BOMB_DISTANCE);
                LM.SpawnEnemy(LM.hiveBomb, 0, -BOMB_DISTANCE);
                break;
        }
    }

    void PhaseLoop(int num)
    {
        float randAngle;

        if (loopCount <= 3)    
        {
            if (num % (6 - loopCount) == 0)
            {
                randAngle = Random.Range(0, PI * 2);

                LM.SpawnEnemy(LM.hiveBomb,
                    Mathf.Sin(randAngle) * BOMB_DISTANCE,
                    Mathf.Cos(randAngle) * BOMB_DISTANCE);
            }
        }
        else
        {
            if (num % (5) == 0)
            {
                randAngle = Random.Range(0, PI * 2);

                LM.SpawnEnemy(LM.hiveBomb,
                    Mathf.Sin(randAngle) * BOMB_DISTANCE,
                    Mathf.Cos(randAngle) * BOMB_DISTANCE);

                randAngle = Random.Range(0, PI * 2);

                LM.SpawnEnemy(LM.hiveBomb,
                    Mathf.Sin(randAngle) * BOMB_DISTANCE,
                    Mathf.Cos(randAngle) * BOMB_DISTANCE);
            }
        }
    }


    public override void Explode()
    {
        //target.GetComponent<Player>().LevelComplete();
        //Object.FindObjectOfType<GameMan>().WinState();

        Achievements.Instance.Achievment("HIVE_MIND");

        BaseLevel.Instance.LevelComplete();
        base.Explode();

    }

}
