using UnityEngine;
using System.Collections;

public class Laser : BaseAttack 
{
    public ParticleSystem explosion;

    public bool tooFastExplosion;
    public bool piercing;

    protected override void OnEnable()
    {
        base.OnEnable();
        timeDilation = 1;

        //if (GetComponent<ParticleSystem>())
        //{
        //    GetComponent<ParticleSystem>().Play();
        //}
            
    }

    void OnTriggerEnter(Collider col)
    {
        if(side == Team.BLUE)
        {
            if (col.gameObject.tag == "Enemy")
            {
                if(piercing)
                    Instantiate(explosion, transform.position, Quaternion.identity);
                else
                    OnEnd();
            }

            //if (col.gameObject.tag == "Enemy Laser")
            //{
            //    OnEnd();               
            //}
        }

        if(side == Team.RED)
        {
            if (col.gameObject.tag == "Chrono")
            {
                timeDilation = 0.2f;
            }

            if (col.gameObject.tag == "Player")
            {
                OnEnd();
            }

            //if (col.gameObject.tag == "Laser" && !unbreakable)
            //{
            //    OnEnd();
            //}


        }

        if (col.gameObject.tag == "Block")
        {
            OnEnd();
        }

    }


    void OnTriggerExit(Collider col)
    {
        if (side == Team.RED)
        {
            if (col.gameObject.tag == "Chrono")
            {
                timeDilation = 1;
            }
        }
    }

    protected override void OnEnd()
    {
        if(explosion)
        {
            if(tooFastExplosion)
                Instantiate(explosion, transform.position - (velocity * Time.deltaTime), Quaternion.identity);
            else
                Instantiate(explosion, transform.position, Quaternion.identity);
        }

        base.OnEnd();
    }
}
