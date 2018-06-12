using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class RedSun : BaseEnemy
{
    //public Material defaultMat;
    //public Material flashMat;
    //public ParticleSystem hit;

    public GameObject sun;
    public GameObject outer;
    public GameObject inner;
    public GameObject laserRing;
    public GameObject laserRing2;

    //public GameObject cannon;
    public GameObject debris;
    public Transform shootPoint;

    float shootDelay = 2.5f;
    float shootTimer;

    public bool isAlive = true;

    GameObject clone;

    int phaseNum = 1;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        stunImmune = true;
        timeImmune = true;

        //sun = transform.Find("Sun").gameObject;
        //outer = sun.transform.Find("Outer Sun").gameObject;
        //inner = sun.transform.Find("Inner Sun").gameObject;

        //shootPoint = transform.Find("Shoot Point").gameObject;

        //laserRing = transform.Find("Laser Ring").gameObject;
        //laserRing2 = transform.Find("Laser Ring 2").gameObject;

        Object.FindObjectOfType<BossHealth>().StartBar(this.gameObject);

        for (int i = 0; i <= 60; i++)
        {
            SpawnDebris();
        }

        //ObjectPool.Instance.laserCount += 1000;

    }

    override protected void Normal()
    {
        //if (isAlive)
        //{
        //    if (health <= 0)
        //    {
        //        Explode();
        //    }

        //    switch (phaseNum)
        //    {
        //        case 1:

        //            if (health <= maxHealth / 3 * 2)
        //            {
        //                shootDelay -= 0.5f;
        //                laserRing.SetActive(true);
        //                phaseNum = 2;
        //            }

        //            break;

        //        case 2:

        //            if (health <= maxHealth / 3)
        //            {
        //                shootDelay -= 0.5f;
        //                laserRing2.SetActive(true);
        //                phaseNum = 3;
        //            }

        //            break;

        //        default:
        //            break;
        //    }

        //    laserRing.transform.Rotate(new Vector3(0, -0.3f, 0) * Time.timeScale);
        //    laserRing2.transform.Rotate(new Vector3(0, -0.3f, 0) * Time.timeScale);
        //}



        sun.transform.Rotate(new Vector3(0, 0.2f, 0));
    }



    void SpawnDebris()
    {
        //Vector3 offset;
        //while (true)
        //{
        //    offset = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));

        //    if (offset.x + offset.z != 0)
        //        break;
        //}

        //GameObject clone = Instantiate(debris, shootPoint.transform.position + offset, transform.rotation) as GameObject;
    }

    override protected void ChangeMat(Material mat)
    {
        //sun.GetComponent<Renderer>().material = mat;
    }


    public override void Explode()
    {
        if(isAlive)
        {
            //target.GetComponent<Player>().LevelComplete();
            BaseLevel.Instance.LevelComplete();
            //Object.FindObjectOfType<GameMan>().WinState();

            Destroy(outer.gameObject);
            Destroy(inner.gameObject);
            Destroy(laserRing.gameObject);
            Destroy(laserRing2.gameObject);
            isAlive = false;

            explosion.transform.position = transform.position;
            explosion.gameObject.SetActive(true);
            
        }

    }
}
