using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using Steamworks;


public class Archangel : BaseEnemy
{
    BaseLevel LM;
    public GameObject camPoint;

    public GameObject mainGun_obj;
    public GameObject gun1_obj;
    public GameObject gun2_obj;
    public GameObject laser1_obj;
    public GameObject laser2_obj;

    ArchGunSmall gun;

    ArchLaserGun laser;

    ArchMainGun mainGun;

    public bool testMode;
    float rand;

    int loopCount;

    override protected void Awake()
    {
        stunImmune = true;
        timeImmune = true;

        LM = Object.FindObjectOfType<BaseLevel>();
          
        //Move backwards slowly
        velocity.Set(100, 0, 0);

        ObjectPool.Instance.laserCount += 200;
        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(camPoint.transform, 0.4f, 0.4f, 0);

        base.Awake();

        laser1_obj.transform.DOLocalMove(new Vector3(-115, 0, 20), 1.4f);
        laser2_obj.transform.DOLocalMove(new Vector3(115, 0, 20), 1.4f);

        gun1_obj.transform.DOLocalMoveX(-55, 1.4f);
        gun2_obj.transform.DOLocalMoveX(55, 1.4f);

        gun = GetComponent<ArchGunSmall>();
        laser = GetComponent<ArchLaserGun>();


        mainGun = mainGun_obj.GetComponent<ArchMainGun>();

        //LM.SetBeatTime(128)

        //LM.StartAt(200);
    }



    protected override void OnBeat()
    {
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
                gun.Single(Random.Range(25, 35));
                break;

            case 26:
                gun.Disable();
                break;

            case 30:
                laser.Activate();
                break;

            case 59:
                laser.Aim();
                break;

            case 62:
                laser.Activate();
                gun.Single(Random.Range(25, 35));
                break;

            case 92:
                laser.Deactivate();
                gun.Disable();
                break;

            case 93:
                mainGun.StartLaser();
                break;

            case 108:
                mainGun.StartLaser();
                break;

            case 125:
                mainGun.StartLaser();
                break;

            case 126:
                if(loopCount == 0)
                    gun.Double(Random.Range(25, 35));     
                else 
                    gun.Quad(Random.Range(25, 35));              
                break;

            case 157:
                mainGun.StartLaser();
                gun.Disable();
                break;

            case 159:
                laser.Activate();
                break;

            case 173:
                mainGun.StartLaser();
                break;

            case 189:
                mainGun.StartLaser();
                break;

            case 192:
                if(loopCount == 0)
                    gun.Single(Random.Range(25, 35));
                else if (loopCount == 1)
                    gun.Double(Random.Range(25, 35));
                else
                    gun.Quad(Random.Range(25, 35));
                break;

            case 205:
                mainGun.StartLaser();
                break;

            case 220:
                mainGun.StartLaser();
                break;

            case 222: // Loop      192 
                loopCount++;

                gun.Disable();
                laser.Deactivate();

                Music.Instance.GetComponent<AudioSource>().time -= 128 * LM.GetBeatTime();
                BaseLevel.Instance.SetBeatNum(BaseLevel.Instance.GetBeatNum() - 128);
                break;
        }
    }




    protected override void ChangeMat(Material mat)
    {
        mainGun_obj.GetComponent<Renderer>().material = mat;

        laser1_obj.GetComponent<Renderer>().material = mat;
        laser2_obj.GetComponent<Renderer>().material = mat;

        gun1_obj.GetComponent<Renderer>().material = mat;
        gun2_obj.GetComponent<Renderer>().material = mat;
    }


    public override void Explode()
    {
        //target.GetComponent<Player>().LevelComplete();

        //Object.FindObjectOfType<GameMan>().WinState();

        Achievements.Instance.Achievment("ARCHANGEL");
        BaseLevel.Instance.LevelComplete();

        base.Explode();
    }
}
