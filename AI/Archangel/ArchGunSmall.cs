using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArchGunSmall : MonoBehaviour
{
    enum State
    {
        OFF, SINGLE, DOUBLE, QUAD
    }
    State state;

    public Transform[] gun;
    public Transform[] shootPoint;

    GameObject clone;

    int posCount;

    float shootAngle;
    public float angleChange;
    float angleChangeValue;

    float shootSpeed = 200;

    void Start()
    {        
        state = State.OFF;
        angleChange = 27;
        angleChangeValue = angleChange;
    }

    public void Single(float _angleChange)
    {
        state = State.SINGLE;

        angleChange = _angleChange;
        angleChangeValue = -angleChange;
        shootAngle = 0;
    }

    public void Double(float _angleChange)
    {
        state = State.DOUBLE;

        angleChange = _angleChange;
        angleChangeValue = -angleChange;
        shootAngle = 25;
    }

    public void Quad(float _angleChange)
    {
        state = State.QUAD;

        angleChange = _angleChange;
        angleChangeValue = -angleChange;
        shootAngle = 40;
    }

    public void Disable()
    {
        state = State.OFF;
    }

    void OnBeat()
    {
        switch (state)
        {
            default:
            case State.OFF:
                break;

            case State.SINGLE:        
                Shoot(shootAngle);
                break;

            case State.DOUBLE:
                Shoot(shootAngle);
                Shoot(-shootAngle);
                break;

            case State.QUAD:
                Shoot(shootAngle);
                Shoot(-shootAngle);
                Shoot(shootAngle * 0.4f);
                Shoot(-shootAngle * 0.4f);
                break;
        }

        if(state != State.OFF)
        {
            if (shootAngle <= -60)
            {
                angleChangeValue = angleChange;
            }

            if (shootAngle >= 60)
            {
                angleChangeValue = -angleChange;
            }

            shootAngle += angleChangeValue;

            switch (posCount)
            {
                default:
                case 0:
                    gun[0].transform.DOLocalMoveX(55 * -1, 0.35f);
                    gun[1].transform.DOLocalMoveX(55, 0.35f);
                    break;

                case 1:
                    gun[0].transform.DOLocalMoveX(65 * -1, 0.35f);
                    gun[1].transform.DOLocalMoveX(65, 0.35f);
                    break;

                case 2:
                    gun[0].transform.DOLocalMoveX(75 * -1, 0.35f);
                    gun[1].transform.DOLocalMoveX(75, 0.35f);
                    break;

                case 3:
                    gun[0].transform.DOLocalMoveX(65 * -1, 0.35f);
                    gun[1].transform.DOLocalMoveX(65, 0.35f);
                    break;
            }

            if (posCount < 3)
                posCount++;
            else
                posCount = 0;
        }
    }


    void Shoot(float angle)
    {
        shootPoint[1].localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
        clone = ObjectPool.Instance.GetLaser(shootPoint[1].transform.position);
        clone.GetComponent<RedLaser>().SetSize(45);
        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[1].transform.forward * shootSpeed);

        shootPoint[0].localRotation = Quaternion.Euler(new Vector3(0, angle * -1, 0));
        clone = ObjectPool.Instance.GetLaser(shootPoint[0].transform.position);
        clone.GetComponent<RedLaser>().SetSize(45);
        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[0].transform.forward * shootSpeed);
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
