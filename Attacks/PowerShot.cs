using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PowerShot : BaseAttack
{
    public float baseDamage;
    public float shotCharge;
    float maxCharge = 35;

    public ParticleSystem explosion;

    bool active;

    public Blue owner;
    public Transform shootPoint;

    float shotPower;
    const float MAX_SHOT_POWER = 100;
    const float SHOT_CHARGE_SPEED = 100;
    //Slider shotPowerBar;



    protected override void OnEnable()
    {
        active = false;
        SetVelocity(Vector2.zero);

        //shotCharge = (shotCharge * 0.3f) + 5;

        shotCharge = 5;

        //shotPowerBar = CombatBars.Instance.GetBar("Power Shot");


        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().enabled = false;

        GetComponent<Collider>().enabled = false;


        base.OnEnable();
    }

    void OnDisable()
    {
        //shotPowerBar.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        if(!active)
        {
            transform.position = shootPoint.position;

            shotCharge += Time.deltaTime * 30;

            //shotPowerBar.value = shotCharge / maxCharge;

            transform.localScale = new Vector3(shotCharge, 0, shotCharge);

            if (shotCharge > maxCharge)
            {
                shotCharge = maxCharge;
                owner.PowerShot();
            }

        }
        else 
            base.Update();
    }

    public void Shoot(Vector3 vel)
    {
        active = true;
        SetVelocity(vel);

        GetComponent<TrailRenderer>().startWidth = shotCharge;

        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

        //shotPowerBar.gameObject.SetActive(false);
        damage = baseDamage * shotCharge;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        if (col.gameObject.tag == "Block")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            SetVelocity(Vector2.zero);
            GetComponent<Collider>().enabled = false;
        }
    }


}
