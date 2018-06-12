using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickyCube : BaseEnemy
{

    bool active;
    bool cubeCheck;

    protected override void Start()
    {
        base.Start();
        stunImmune = true;
        explosion.GetComponent<EnemyExplosion>().meshSize = 30;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        active = false;
        enemyTag = "sticky";
        //GetComponent<BoxCollider>().enabled = false;
    }

    protected override void OnBeat()
    {
        cubeCheck = false;

        if (!active)
        {         
            if(transform.position.y < -500)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Vector3.up, out hit, 800.0f))
                {
                    if(hit.collider.GetComponent<BaseEnemy>())
                    {
                        if (hit.collider.GetComponent<BaseEnemy>().enemyTag == "sticky")
                            cubeCheck = true;
                    }
                }

                if(!cubeCheck)
                    transform.DOMoveY(transform.position.y + 450, 0.1f);
                
            }


        }


    }

    void Activate()
    {
        //GetComponent<BoxCollider>().enabled = true;
    }

    public override void Explode()
    {
        Explode(false);
    }
}
