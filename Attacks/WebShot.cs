using UnityEngine;
using System.Collections;

public class WebShot : BaseAttack 
{
    public ParticleSystem explosion;

    public GameObject smallExplosion;
    public GameObject bigExplosion;

    bool poweredUp = false;
    TrailRenderer trail;

    protected override void Start()
    {
        trail = GetComponent<TrailRenderer>();
        base.Start();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            if (col.GetComponent<BaseAttack>().atkTag == "Web"
                && !poweredUp)
            {
                poweredUp = true;
                transform.localScale *= 2;
                velocity *= 2;
                trail.startWidth *= 2;
                Instantiate(smallExplosion, transform.position, Quaternion.identity);
             }
        }

        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Red Sun")
        {
            OnEnd();
        }

        if (col.gameObject.tag == "Red Laser")
        {
            OnEnd();
        }
    }

    protected override void OnEnd()
    {
        if(explosion)
            Instantiate(explosion, transform.position, Quaternion.identity);

        if(poweredUp)
            Instantiate(bigExplosion, transform.position, Quaternion.identity);
        else
            Instantiate(smallExplosion, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }



}
