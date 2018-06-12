using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoxBomber : BaseEnemy
{
    public GameObject boxBomb_prefab;
    public GameObject orb_prefab;

    BaseLevel LM;

    List<GameObject> boxBomb = new List<GameObject>();
    GameObject[] orbs = new GameObject[80];

    float time_r;
    float[] radius = new float[3];
    const float TWO_PI = 6.28f;

    float yEuler;

    int beatCount;
    int spawnCount;

    override protected void Start()
    {
        stunImmune = true;
        timeImmune = true;

        rotationSpeed = 4;

        for (int i = 0; i < 10; i++)
        {
            boxBomb.Add(Instantiate(boxBomb_prefab) as GameObject);
            boxBomb[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < orbs.Length; i++)
        {
            orbs[i] = Instantiate(orb_prefab) as GameObject;
        }

        LM = Object.FindObjectOfType<BaseLevel>();

        base.Start();

    }

    override protected void Normal()
    {
        OrbsUpdate();


    }

    override protected void OnBeat()
    {
        yEuler += 15;
        targetRotation = Quaternion.Euler(0, yEuler, 0);


        if(spawnCount >= 6)
        {
            LM.SpawnCircle(boxBomb, 1, 1000, (TWO_PI / 8) * beatCount);
            spawnCount = 0;
        }
        
        beatCount++;
        spawnCount++;
    }


    void OrbsUpdate()
    {
        time_r += Time.deltaTime * 0.2f;


        if(radius[0] <= 2000)
        {

            radius[0] += Time.deltaTime * 100;
        }
        else
        {
            radius[0] = 2000;
        }

        // Max = 180
        for (int i = 0; i < orbs.Length; i++)
        {
            

            float y = 0;
            float x = 0;
            float z = 0;

            switch (i % 4)
            {
                default:
                    break;

                case 0:
                    x = Mathf.Sin(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Sin(time_r);
                    z = Mathf.Cos(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Sin(time_r);
                    break;

                case 1:
                    x = Mathf.Sin(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Cos(time_r);
                    z = Mathf.Cos(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Cos(time_r);
                    break;

                case 2:
                    x = Mathf.Sin(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Sin(time_r);
                    z = Mathf.Cos(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Cos(time_r);
                    break;

                case 3:
                    x = Mathf.Sin(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Cos(time_r);
                    z = Mathf.Cos(time_r + (TWO_PI / orbs.Length) * i) * radius[0] * Mathf.Sin(time_r);
                    break;

            }

            orbs[i].transform.position = new Vector3(x, y, z)
                + new Vector3(transform.position.x, 0, transform.position.z);
        }
    }



    public override void Explode()
    {

        //target.GetComponent<Player>().LevelComplete();

        //Object.FindObjectOfType<GameMan>().WinState();
        BaseLevel.Instance.LevelComplete();

        base.Explode();
    }

}
