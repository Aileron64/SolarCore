using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class Cyan : Player
{
    //public GameObject bubble_prefab;
    //List<GameObject> bubble = new List<GameObject>();
    //public GameObject laser_prefab;
    //public GameObject powerShot_prefab;

    public GameObject[] laser_prefab;

    List<GameObject> laser1 = new List<GameObject>();
    List<GameObject> laser2 = new List<GameObject>();
    List<GameObject> laser3 = new List<GameObject>();

    public GameObject[] shootPoint;
    public GameObject laserBeam;
    bool laserBeamActive = false;
    public GameObject nova_prefab;
    GameObject nova;

    float spread = 15;

    int attackMode = 2;

    const float LASER_SIZE = 40;
    const float LASER_DURATION = 2f;
    const float LASER_DAMAGE = 2000;

    float[,] shootPowerMod = {
        { 2500, 2500, 2500 },
        { 1900, 2000, 2100 }, 
        { 1100, 1150, 1200 }};

    float[,] shootDelayMod = {
        { 0.5f, 0.4f, 0.3f },
        { 0.25f, 0.225f, 0.2f },
        { 0.1f, 0.09f, 0.08f }};

    bool shootToggle;

    float[] speed_Mod = { 0.8f, 1, 1.3f };

    GameObject clone;

    protected override void Start()
    {
        // Laser
        skillCost[0] = 3200;
        skillMax[0] = skillCost[0];

        // Switch
        skillCost[1] = 1200;
        skillMax[1] = skillCost[1];

        shootPower[0] = shootPowerMod[attackMode - 1, 0];
        shootPower[1] = shootPowerMod[attackMode - 1, 1];
        shootPower[2] = shootPowerMod[attackMode - 1, 2];

        shootDelay[0] = shootDelayMod[attackMode - 1, 0];
        shootDelay[1] = shootDelayMod[attackMode - 1, 1];
        shootDelay[2] = shootDelayMod[attackMode - 1, 2];

        Pooler.Instantiate(laser1, laser_prefab[0], 20);
        Pooler.Instantiate(laser2, laser_prefab[1], 50);
        Pooler.Instantiate(laser3, laser_prefab[2], 200);

        angleDelay = 180;

        nova = Instantiate(nova_prefab);
        nova.gameObject.SetActive(false);
        nova.GetComponent<Nova>().FollowOwner(this.transform);

        

        base.Start();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        
        if(laserBeamActive)
        {
            rB.AddForce(shootPoint[0].transform.forward * -1000);
        }

    }

    protected override void ButtonInput()
    {
        if (Input.GetAxis("Right Trigger P" + playerNum) > 0 && skillEnergy[0] >= skillCost[0])
        {
            trigger1Down = true;
            UseSkill(0);        
        }

        if (Input.GetAxis("Right Trigger P" + playerNum) == 0 && trigger1Down)
        {
            trigger1Down = false;

            if(laserBeamActive)
            {
                laserBeam.transform.DOScaleX(0, 0.5f);
                Invoke("DeactivateLaser", 0.5f);
                speedMod = speed_Mod[attackMode - 1];
            }

        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0)
        {
            UseSkill(1);
        }
    }

    protected override void Shoot()
    {
        if(!laserBeamActive)
        {
            switch (attackMode)
            {
                default:
                case 1:
                    Shoot1();
                    break;

                case 2:
                    Shoot2();
                    break;

                case 3:
                    Shoot3();
                    break;
            }
        }

    }


    void Shoot1()
    {
        clone = Pooler.GetObject(laser1, shootPoint[0].transform.position, transform.rotation);
        clone.GetComponent<Laser>().SetVelocity(shootPoint[0].transform.forward * shootPower[weaponLevel]);
        clone.GetComponent<TrailRenderer>().Clear();

        ProCamera2DShake.Instance.Shake(
            0.2f, //duration
            new Vector3(40, 40), //strength
            2, //vibrato
            0.5f, //randomness
            aimAngle * -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        rB.AddForce(shootPoint[0].transform.forward * speed * -5f * shootDelay[weaponLevel]);
    }

    void Shoot2()
    {

        clone = Pooler.GetObject(laser2, shootPoint[1].transform.position, transform.rotation);
        clone.GetComponent<Laser>().SetVelocity(shootPoint[1].transform.forward * shootPower[weaponLevel]);
        clone.GetComponent<TrailRenderer>().Clear();

        clone = Pooler.GetObject(laser2, shootPoint[2].transform.position, transform.rotation);
        clone.GetComponent<Laser>().SetVelocity(shootPoint[2].transform.forward * shootPower[weaponLevel]);
        clone.GetComponent<TrailRenderer>().Clear();

    }

    void Shoot3()
    {
        if(shootToggle)
        {
            clone = Pooler.GetObject(laser3, shootPoint[0].transform.position, transform.rotation);
            clone.GetComponent<Laser>().SetVelocity(shootPoint[0].transform.forward * shootPower[weaponLevel]);
            clone.GetComponent<TrailRenderer>().Clear();
        }
        else
        {
            float offset = Random.Range(0, spread);

            clone = Pooler.GetObject(laser3, shootPoint[1].transform.position, transform.rotation);
            clone.GetComponent<BaseAttack>().SetVelocity(new Vector3(
                Mathf.Sin((aimAngle - offset) * Mathf.Deg2Rad), 0,
                Mathf.Cos((aimAngle - offset) * Mathf.Deg2Rad)).normalized * shootPower[weaponLevel] * -1);
            clone.GetComponent<TrailRenderer>().Clear();

            clone = Pooler.GetObject(laser3, shootPoint[2].transform.position, transform.rotation);
            clone.GetComponent<BaseAttack>().SetVelocity(new Vector3(
                Mathf.Sin((aimAngle + offset) * Mathf.Deg2Rad), 0,
                Mathf.Cos((aimAngle + offset) * Mathf.Deg2Rad)).normalized * shootPower[weaponLevel] * -1);
            clone.GetComponent<TrailRenderer>().Clear();
        }

        shootToggle = !shootToggle;
    }


    // Big Laser
    protected override void Skill_0()
    {
        if(!laserBeamActive)
        {
            laserBeam.GetComponent<BaseAttack>().damage = LASER_DAMAGE;
            laserBeam.transform.localScale = new Vector3(0, 5, 3000);

            laserBeam.transform.DOScaleX(LASER_SIZE, 0.5f);
            Invoke("LaserTimeOut", LASER_DURATION);
        }
        else
        {
            // Overcharge
            laserBeam.GetComponent<BaseAttack>().damage = LASER_DAMAGE * 2;
            laserBeam.transform.DOScaleX(LASER_SIZE * 2, 0.5f);

            CancelInvoke();
            Invoke("LaserTimeOut", LASER_DURATION * 1.2f);
        }

        laserBeamActive = true;
        laserBeam.SetActive(true);
    
        speedMod = 0.1f;       
    }

    void LaserTimeOut()
    {
        if(laserBeamActive)
        {
            laserBeam.transform.DOScaleX(0, 0.5f);
            Invoke("DeactivateLaser", 0.5f);
            speedMod = speed_Mod[attackMode - 1];
        }
    }

    void DeactivateLaser()
    {
        laserBeamActive = false;
        laserBeam.SetActive(false);
        speedMod = speed_Mod[attackMode - 1];
    }

    // SWITCH
    protected override void Skill_1()
    {
        if (attackMode >= 3)
            attackMode = 1;
        else
            attackMode++;

        shootTimer = 0.5f;

        DebugText.Instance.SetText("" + attackMode);

        shootPower[0] = shootPowerMod[attackMode - 1, 0];
        shootPower[1] = shootPowerMod[attackMode - 1, 1];
        shootPower[2] = shootPowerMod[attackMode - 1, 2];

        shootDelay[0] = shootDelayMod[attackMode - 1, 0];
        shootDelay[1] = shootDelayMod[attackMode - 1, 1];
        shootDelay[2] = shootDelayMod[attackMode - 1, 2];

        speedMod = speed_Mod[attackMode - 1];

        if(skillEnergy[0] <= skillMax[0] - 50)
            skillEnergy[0] = skillMax[0] - 50;

        ProCamera2DShake.Instance.Shake(
            0.7f, //duration
            new Vector3(25, 25), //strength
            10, //vibrato
            1, //randomness
            -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        nova.transform.position = transform.position;
        nova.SetActive(true);
    }

}
