using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour 
{

    public ParticleSystem explosion;

    //public Rigidbody debris;

    public Vector3 velocity;

    int debrisNum = 10;

    public float damage = 100;
    public float power;
    protected float lifeTime = 20;
    protected float timeDilation = 1;

    protected virtual void Awake()
    {
        damage = 15;
    }

    protected virtual void Start()
    {
        damage *= 0.5f + (0.1f * power);

        Destroy(this.gameObject, lifeTime);
    }

    protected virtual void Update()
    {
        //transform.localScale = transform.localScale + new Vector3(scaleRate, 0, scaleRate);
        transform.position += velocity * Time.deltaTime * timeDilation;
        if (timeDilation < 1)
            timeDilation += Time.deltaTime;

    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Explode();
        }

        if (col.gameObject.tag == "Laser")
        {
            Destroy(this.gameObject);
            Explode();
        }

        //damage -= 50;

        //if (damage <= 0)

    }

    protected virtual void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
        }
    }

    protected virtual void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
