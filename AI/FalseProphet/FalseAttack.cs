using UnityEngine;
using System.Collections;

public class FalseAttack : MonoBehaviour
{

    const float PI = 3.145f;
    //public GameObject blast;

    FalseProphet boss;

    public enum AttackType
    {
        CIRCLE_OUT_0, CIRCLE_OUT_1, CIRCLE_OUT_2, CIRCLE_OUT_3,
        CIRCLE_IN_0, CIRCLE_IN_1,
        ROWS_IN,
        VERTICAL_CHASE_UP, VERTICAL_CHASE_DOWN, HORIZONTAL_CHASE_LEFT, HORIZONTAL_CHASE_RIGHT
    }

    public AttackType type;

    //float[] blastAngle = new float[10];
    //float[] radius = new float[10];


    public GameObject target;

    int num;
    int count;
    float radius;
    float positionX;
    float positionZ;

    public void Start()
    {
        BaseLevel.OnBeat += BeatEvent;
        boss = Object.FindObjectOfType<FalseProphet>();


        switch (type)
        {
            default:
            case AttackType.CIRCLE_OUT_0:
                count = 10;
                num = 5;
                radius = 300;
                break;

            case AttackType.CIRCLE_OUT_1:
                count = 10;
                num = 5;
                radius = 300;
                break;

            case AttackType.CIRCLE_OUT_2:
                count = 6;
                num = 10;
                radius = 300;
                break;

            case AttackType.CIRCLE_OUT_3:
                count = 6;
                num = 10;
                radius = 300;
                break;

            case AttackType.CIRCLE_IN_0:
                count = 5;
                num = 28;
                radius = 2500;
                break;

            case AttackType.CIRCLE_IN_1:
                count = 5;
                num = 28;
                radius = 2300;
                break;

            case AttackType.ROWS_IN:
                count = 10;
                num = 10;
                break;

            case AttackType.HORIZONTAL_CHASE_LEFT:
                count = 10;
                num = 0;
                positionX = target.transform.position.x;
                positionZ = target.transform.position.z - 1000;
                break;

            case AttackType.HORIZONTAL_CHASE_RIGHT:
                count = 10;
                num = 0;
                positionX = target.transform.position.x;
                positionZ = target.transform.position.z + 1000;
                break;

            case AttackType.VERTICAL_CHASE_UP:
                count = 10;
                num = 0;
                positionX = target.transform.position.x - 1000;
                positionZ = target.transform.position.z;
                break;

            case AttackType.VERTICAL_CHASE_DOWN:
                count = 10;
                num = 0;
                positionX = target.transform.position.x + 1000;
                positionZ = target.transform.position.z;
                break;
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
    }

    void BeatEvent()
    {

        switch (type)
        {
            default:
            case AttackType.CIRCLE_OUT_0:

                radius += 130;
                num += 1;

                for (int i = 0; i < num; i++)
                {
                    float angle = ((PI * 2) / num) * i;
                    SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius);
                }
                break;

    ///////////////////////////////////////////////////

            case AttackType.CIRCLE_OUT_1:

                radius += 130;
                num += 1;

                for (int i = 0; i < num; i++)
                {
                    float angle = (((PI * 2) / num) * i) + PI + PI / num;
                    SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius);
                }
            break;


    ///////////////////////////////////////////////////

            case AttackType.CIRCLE_OUT_2:

                for (int i = 0; i < num; i++)
                {
                    float angle = (((PI * 2) / num) * i);
                    SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius);
                }

                num += 5;
                radius += 400;

                break;

        ///////////////////////////////////////////////////

        case AttackType.CIRCLE_OUT_3:

            for (int i = 0; i < num; i++)
            {
                float angle = (((PI * 2) / num) * i);
                SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * (radius + 200));
            }

            num += 5;
            radius += 400;

            break;

        ///////////////////////////////////////////////////

        case AttackType.CIRCLE_IN_0:

            radius -= 400;
            num -= 3;

            for (int i = 0; i < num; i++)
            {
                float angle = ((PI * 2) / num) * i;
                SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius);
            }
            break;

        ///////////////////////////////////////////////////

        case AttackType.CIRCLE_IN_1:

            radius -= 400;
            num -= 3;

            for (int i = 0; i < num; i++)
            {
                float angle = ((PI * 2) / num) * i + PI;
                SpawnBlast(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius);
            }
            break;

        ///////////////////////////////////////////////////


        }

        count--;

        if (count <= 0)
        {
            BaseLevel.OnBeat -= BeatEvent;
            Destroy(this.gameObject);
        }

    }


    void SpawnBlast(Vector3 _pos)
    {
        boss.SpawnBlast(_pos + boss.transform.position);
    }


}
