using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSunLasers : MonoBehaviour
{
    public Transform shootPoint;
    public bool active;

    GameObject clone;

    int count;

    void OnBeat()
    {
        count++;

        if (count >= 10)
        {
            count = 0;

            int rand = Random.Range(1, 6);

            switch (rand)
            {
                default:
                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        Shoot(5, i, 400 + (i * 25));
                    }
                    break;

                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        Shoot(5, i * -1, 400 + (i * 25));
                    }
                    break;

                case 3:
                    Shoot(15, 0, 600);
                    Shoot(15, 2, 500);
                    Shoot(15, 4, 400);
                    break;

                case 4:
                    Shoot(20, 0, 600);
                    Shoot(20, 4, 400);
                    break;

                case 5:
                    Shoot(10, 0, 700);
                    Shoot(10, 2, 600);
                    Shoot(10, 4, 500);
                    Shoot(10, 6, 400);
                    break;
            }
        }
    }

    void Shoot(int num, float rotDelay, float shootSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            clone = ObjectPool.Instance.GetLaser(shootPoint.position);
            clone.GetComponent<RedLaser>().SetSize(80);

            float angle = 0 + i * (6.28f / num) + rotDelay * 0.1f;

            Vector3 direction = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            clone.GetComponent<BaseAttack>().SetVelocity(direction * shootSpeed);
        }
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
