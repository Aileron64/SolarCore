using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArchLaserGun : MonoBehaviour
{
    enum State
    {
        OFF, AIM, ACTIVE
    }
    State state;

    BoxCollider[] col = new BoxCollider[2];

    public Transform[] gun;
    public GameObject[] laser;  

    bool laserToggle;

    bool[] rotate = new bool[2];
    Vector3[] targetDir = new Vector3[2];
    //Vector3 newDir;

    float beatTime;
    Transform player;

    void Start()
    {
        col[0] = laser[0].GetComponent<BoxCollider>();
        col[1] = laser[1].GetComponent<BoxCollider>();
        beatTime = BaseLevel.Instance.GetBeatTime();

        player = PlayerManager.Instance.GetPlayer(0).transform;

        //state = State.ACTIVE;
    }

    public void Aim()
    {
        state = State.AIM;
        col[0].enabled = false;
        col[1].enabled = false;
    }

    public void Activate()
    {
        state = State.ACTIVE;
    }

    public void Deactivate()
    {
        state = State.OFF;
        rotate[0] = false;
        rotate[1] = false;
        col[0].enabled = false;
        col[1].enabled = false;
        gun[0].DOLocalRotate(Vector3.zero, 2.2f);
        gun[1].DOLocalRotate(Vector3.zero, 2.2f);
    }

    void FixedUpdate()
    {
        if(state == State.AIM)
        {
            gun[0].rotation = Quaternion.LookRotation(Vector3.RotateTowards(gun[0].forward,
                player.position - gun[0].position, Time.smoothDeltaTime * 0.3f, 0.0F));

            gun[1].rotation = Quaternion.LookRotation(Vector3.RotateTowards(gun[1].forward,
                player.position - gun[1].position, Time.smoothDeltaTime * 0.3f, 0.0F));
        }

        if(rotate[0])
        {
            gun[0].rotation = Quaternion.LookRotation(Vector3.RotateTowards(gun[0].forward, 
                targetDir[0], Time.smoothDeltaTime * 0.3f, 0.0F));
        }

        if (rotate[1])
        {
            gun[1].rotation = Quaternion.LookRotation(Vector3.RotateTowards(gun[1].forward,
                targetDir[1], Time.smoothDeltaTime * 0.3f, 0.0F));
        }
    }

    void OnBeat()
    {
        switch(state)
        {
            default:
            case State.OFF:
                break;

            case State.ACTIVE:

                if (laserToggle)
                {
                    rotate[0] = false;
                    laser[0].transform.DOScale(new Vector3(13, 3, 1000), 0.6f).From();
                    Invoke("EnableCollider0", 0.1f);

                    rotate[1] = true;
                    targetDir[1] = player.position - gun[1].position;
                    col[1].enabled = false;
                }
                else
                {
                    rotate[1] = false;
                    laser[1].transform.DOScale(new Vector3(13, 3, 1000), 0.6f).From();
                    Invoke("EnableCollider1", 0.1f);

                    rotate[0] = true;
                    targetDir[0] = player.position - gun[0].position;
                    col[0].enabled = false;
                }

                laserToggle = !laserToggle;
                break;
        }
    }

    void EnableCollider0()
    {
        col[0].enabled = true;
        Invoke("DisableCollider0", 0.1f);
    }

    void DisableCollider0()
    {
        col[0].enabled = false;
    }

    void EnableCollider1()
    {
        col[1].enabled = true;
        Invoke("DisableCollider1", 0.1f);
    }

    void DisableCollider1()
    {
        col[1].enabled = false;
    }

    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }
}
