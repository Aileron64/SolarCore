﻿using UnityEngine;
using System.Collections;

public class RedSunCannon : EnemyLaser 
{
    void Awake()
    {
        damage = 15;
    }

    protected override void Update()
    {
        //transform.localScale = transform.localScale + new Vector3(scaleRate, 0, scaleRate);

        if (timeDilation < 1)
            timeDilation += Time.deltaTime;

        base.Update();
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Explode();
        }

        if (col.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Explode();
        }

        //damage -= 50;

        //if (damage <= 0)

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
        }
    }
}
