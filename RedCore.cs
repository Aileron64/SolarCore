using UnityEngine;
using System.Collections;

public class RedCore : MonoBehaviour
{
    GameObject sun;
    GameObject outer;
    GameObject inner;
    GameObject[] ring = new GameObject[3];

    GameObject audioManager;

    //LineRenderer circle;

    public GameObject redSun;
    //public ParticleSystem redSunSpawn;

    float maxSize = 15;
    float size = 5;
    float sizeIncrease = 0;

    //public Material red;

    float ringSpeed = 1;
    float ringSize = 1;

    bool goingRed = false;
    bool grow = false;

    float timer;


    void Start()
    {
        sun = transform.Find("Sun").gameObject;
        inner = sun.transform.Find("Inner").gameObject;
        outer = sun.transform.Find("Outer").gameObject;

        ring[0] = transform.Find("Ring 0").gameObject;
        ring[1] = ring[0].transform.Find("Ring 1").gameObject;
        ring[2] = ring[1].transform.Find("Ring 2").gameObject;

        //circle = GetComponent<LineRenderer>();
        //DrawCircle(2000, circle);

        audioManager = GameObject.Find("Audio Manager");
    }

    void Update()
    {
        sun.transform.Rotate(new Vector3(0, 0.2f, 0) * Time.timeScale);

        ring[0].transform.Rotate(new Vector3(0.5f, 0.5f, 0) * ringSpeed * Time.timeScale);
        ring[1].transform.Rotate(new Vector3(0.5f, 0, 0) * ringSpeed * Time.timeScale);
        ring[2].transform.Rotate(new Vector3(0, 0, 0.8f) * ringSpeed * Time.timeScale);


        if (goingRed)
        {
            timer += Time.deltaTime;
            ringSpeed += Time.deltaTime;



            if (timer > 3)
            {
                if (ringSize > 0)
                {
                    ringSize -= Time.deltaTime * 0.5f;

                    ring[0].transform.localScale = new Vector3(ringSize, ringSize, ringSize);
                }
                else
                    ring[0].SetActive(false);


                if (size < maxSize)
                {
                    sizeIncrease += Time.deltaTime * 3;
                    size += Time.deltaTime * sizeIncrease;

                    sun.transform.localScale = new Vector3(size, size, size);
                }
                else
                {
                    sun.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                    //audioManager.GetComponent<Music>().PlayBossMusic();

                    GameObject clone = Instantiate(redSun, new Vector3(1100, 0, 1000), transform.rotation) as GameObject;

                    goingRed = false;
                    this.gameObject.SetActive(false);
                }
            }

        }
    }

    public void TurnRed()
    {
        //audioManager.GetComponent<Music>().PlaySiren();

        //inner.GetComponent<Renderer>().material = red;
        //outer.GetComponent<Renderer>().material = red;
        goingRed = true;

        ringSpeed = 3;
    }


}