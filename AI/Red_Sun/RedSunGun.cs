using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSunGun : MonoBehaviour
{
    public State state;
    public enum State
    {
        OFF, CROSS, ROTATE
    }

    GameObject clone;

    const float PI = 3.1415f;

    int count;
    bool flag;

    float rotateAngle;
    public float rotateSpeed;
    public int rotateCount;

    public float shootSpeed;
    public float bulletSize;

    public bool clockWise;
    public bool counterClockwise;
    public bool randomRotateSpeed;

    void OnBeat()
    {
        switch(state)
        {
            default:
            case State.OFF:
                break;

            case State.CROSS:
                Cross();
                break;

            case State.ROTATE:
                Rotate();
                break;

        }      
    }

    public void Activate_DuelRotate()
    {
        state = State.ROTATE;
        clockWise = true;
        counterClockwise = true;

        rotateCount = 6;
        shootSpeed = 500;
        bulletSize = 80;
        rotateSpeed = 1;
    }

    public void Activate_RapidFire(int rotateAmount)
    {
        state = State.ROTATE;
        clockWise = true;
        counterClockwise = true;

        rotateCount = rotateAmount;
        shootSpeed = 600;
        bulletSize = 60;
        rotateSpeed = 3;
        randomRotateSpeed = true;
    }

    public void Activate_SlowRotate(bool _clockwise)
    {
        state = State.ROTATE;

        rotateCount = 6;
        shootSpeed = 200;
        rotateSpeed = 0.5f;
        bulletSize = 120;

        if (_clockwise)
            clockWise = true;
        else
            counterClockwise = true;
    }



    void Cross()
    {
        if(flag)
            ShootCircle(4, bulletSize, shootSpeed, 0);
        else
            ShootCircle(4, bulletSize, shootSpeed, PI / 4);

        flag = !flag;
    }

    void Rotate()
    {
        for (int i = 0; i < rotateCount; i++)
        {
            if(clockWise)
                ShootCircle(rotateCount, bulletSize, shootSpeed, rotateAngle);

            if(counterClockwise)
                ShootCircle(rotateCount, bulletSize, shootSpeed, -rotateAngle);
        }

        if (randomRotateSpeed)
        {
            if (Random.Range(0, 6) > rotateSpeed)
                rotateSpeed++;
            else
                rotateSpeed--;
        }

        if (rotateAngle >= PI * 2)
            rotateAngle -= PI * 2;

        rotateAngle += rotateSpeed * 0.1f;
    }



    void ShootCircle(int num, float size, float speed, float rotDelay)
    {
        for (int i = 0; i < num; i++)
        {
            Shoot(i * (6.28f / num) + rotDelay, size, shootSpeed);
        }
    }

    void Shoot(float angle, float size, float speed)
    {
        clone = ObjectPool.Instance.GetLaser(transform.position);
        clone.GetComponent<RedLaser>().SetSize(size);
        clone.GetComponent<BaseAttack>().SetVelocity(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed);
    }



    void OnEnable() { BaseLevel.OnBeat += OnBeat; }
    void OnDisable() { BaseLevel.OnBeat -= OnBeat; }
}
