using UnityEngine;
using System.Collections;

public class Supernova : BaseAttack 
{
    public GameObject nova;

    bool active = false;

    float startSize = 150;
    float size = 10;
    float timer;

    float sizeIncrease = 100;

    float sizeSpeed = 600;

    float maxSize = 800;
    float novaPower = 0;


    protected override void Start() 
    {
        transform.localScale = new Vector3(1, 1, 1);

        base.Start();
	}
	
    protected override void Update()
    {
        timer += Time.deltaTime;
        sizeSpeed -= Time.deltaTime * 800 * timeDilation;

        size += (sizeSpeed + (size * 0.1f)) * Time.deltaTime;

        if (size <= 1)
            OnEnd();

        if (size >= maxSize)
            size = maxSize;

        transform.localScale = new Vector3(size, 1, size);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Laser")
        {
            if (col.GetComponent<BaseAttack>().atkTag == "pLaser")
            {
                Destroy(col.gameObject);
                size += sizeIncrease * Time.deltaTime;
                novaPower += Time.deltaTime * 60;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {   // Called every frame

        if (col.gameObject.tag == "Enemy")
        {
            size += sizeIncrease * Time.deltaTime;
            novaPower += Time.deltaTime * 60;
        }

        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
        }

        if (col.gameObject.tag == "Laser Beam")
        {
            if (col.GetComponent<BaseAttack>().atkTag == "blackhole")
            {
                size += sizeIncrease * Time.deltaTime * 3;
                novaPower += Time.deltaTime * 60;
            }
        }
    }

    void OnTriggerEnd(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 1;
        }
    }

    protected override void OnEnd()
    {
        GameObject clone = Instantiate(nova, transform.position - new Vector3(0, -25, 0), transform.rotation) as GameObject;
        clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));

        //DebugText.Instance.SetText("" + novaPower);

        clone.GetComponent<Nova>().damage = novaPower * 2 + 100;
        clone.GetComponent<Nova>().lineWidth = novaPower * 0.5f + 15;

        base.OnEnd();
    }

}


