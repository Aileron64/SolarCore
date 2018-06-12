using UnityEngine;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class Blue : Player
{
    public GameObject laser1_prefab;
    public GameObject laser2_prefab;
    //public GameObject laserBlast_prefab;

    List<GameObject> laser1 = new List<GameObject>();
    List<GameObject> laser2 = new List<GameObject>();
    //List<GameObject> laserBlast = new List<GameObject>();

    public GameObject[] smallgun;
    public GameObject[] biggun;

    public GameObject[] shootPoint;

    public GameObject powerShot_prefab;
    PowerShot powerShot;

    public GameObject nova_prefab;
    StunNova nova;

    GameObject clone;


    //int shootParticleCount;


    protected override void Start()
    {
        

        // Power Shot
        skillCost[0] = 400;
        skillMax[0] = skillCost[0];

        // Nova
        skillCost[1] = 2000;
        skillMax[1] = skillCost[1];

        shootPower[0] = 1900;
        shootPower[1] = shootPower[0];
        shootPower[2] = shootPower[0];

        shootDelay[0] = 0.135f;
        shootDelay[1] = shootDelay[0];
        shootDelay[2] = shootDelay[0];

        Pooler.Instantiate(laser1, laser1_prefab, 15);
        Pooler.Instantiate(laser2, laser2_prefab, 30);
        //Pooler.Instantiate(laserBlast, laserBlast_prefab, 12);

        powerShot = Instantiate(powerShot_prefab).GetComponent<PowerShot>();
        powerShot.gameObject.SetActive(false);
        powerShot.owner = this;

        nova = Instantiate(nova_prefab).GetComponent<StunNova>();
        nova.gameObject.SetActive(false);
        nova.owner = this;



        base.Start();
    }

    protected override void ButtonInput()
    {
        if (Input.GetAxis("Right Trigger P" + playerNum) > 0 && skillEnergy[0] >= skillCost[0] && !trigger1Down)
        {
            trigger1Down = true;

            biggun[0].transform.DOLocalMoveZ(-3, 1f);
            biggun[1].transform.DOLocalMoveX(1, 1f);

            //iTween.MoveTo(biggun[0], iTween.Hash("z", -3, "islocal", true, "time", 1f));
            //iTween.MoveTo(biggun[1], iTween.Hash("x", 3, "islocal", true, "time", 1f));

            powerShot.transform.position = shootPoint[0].transform.position;
            powerShot.shootPoint = shootPoint[0].transform;        
            powerShot.gameObject.SetActive(true);         
        }

        if (Input.GetAxis("Right Trigger P" + playerNum) == 0 && trigger1Down)
        {
            PowerShot();
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0 && skillEnergy[1] >= skillCost[1] && !trigger2Down)
        {
            trigger2Down = true;
            nova.transform.position = transform.position;
            nova.gameObject.SetActive(true);
            speedMod = 0.2f;
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) == 0 && trigger2Down)
        {
            StunNova();
        }
    }

    protected override void ChangMat(Material newMat)
    {
        GetComponent<Renderer>().material = newMat;
        smallgun[0].GetComponent<Renderer>().material = newMat;
        smallgun[1].GetComponent<Renderer>().material = newMat;
        biggun[0].GetComponent<Renderer>().material = newMat;
        biggun[1].GetComponent<Renderer>().material = newMat;
    }

    protected override void Shoot()
    {
        if(!trigger1Down)
        {
            smallgun[0].transform.localPosition = new Vector3(17, 0, -3);
            smallgun[1].transform.localPosition = new Vector3(3, 0, -17);

            smallgun[0].transform.DOLocalMoveZ(1, shootDelay[0] * 0.8f).From();
            smallgun[1].transform.DOLocalMoveX(-1, shootDelay[0] * 0.8f).From();



            shootPoint[0].GetComponent<AudioSource>().Play();

            //rB.AddForce(shootPoint[0].transform.forward * -5000);

            switch (weaponLevel)
            {  
                default:
                case 0:
                    clone = Pooler.GetObject(laser1, shootPoint[0].transform.position, transform.rotation);
                    clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[0].transform.forward * shootPower[weaponLevel]);
                    clone.GetComponent<TrailRenderer>().Clear();

                    break;

                case 1:
                    for (int i = 1; i < 3; i++)
                    {
                        clone = Pooler.GetObject(laser2, shootPoint[i].transform.position, transform.rotation);
                        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootPower[weaponLevel]);
                        clone.GetComponent<TrailRenderer>().Clear();
                    }
                    break;

                case 2:

                    clone = Pooler.GetObject(laser1, shootPoint[0].transform.position, transform.rotation);
                    clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[0].transform.forward * shootPower[weaponLevel] * 1.2f);
                    clone.GetComponent<TrailRenderer>().Clear();

                    for (int i = 1; i < 3; i++)
                    {
                        clone = Pooler.GetObject(laser2, shootPoint[i].transform.position, transform.rotation);
                        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint[i].transform.forward * shootPower[weaponLevel]);
                        clone.GetComponent<TrailRenderer>().Clear();
                    }
                    break;
            }
        }
    }

    // Power Shot
    protected override void Skill_0()
    {
        biggun[0].transform.DOKill();
        biggun[1].transform.DOKill();

        biggun[0].transform.DOLocalMoveZ(-8, 0.4f);
        biggun[1].transform.DOLocalMoveX(8, 0.4f);

        ProCamera2DShake.Instance.Shake(
            0.5f, //duration
            new Vector3(1, 1, 1) * powerShot.shotCharge, //strength
            2, //vibrato
            0.5f, //randomness
            aimAngle * -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        rB.AddForce(shootPoint[0].transform.forward * speed * -0.4f * powerShot.shotCharge);

        powerShot.Shoot(shootPoint[0].transform.forward * shootPower[weaponLevel] * 1.5f);

        shootTimer = shootDelay[weaponLevel];
    }

    public void PowerShot()
    {
        trigger1Down = false;
        UseSkill(0);
    }

    // Stun Nova
    protected override void Skill_1()
    {
        ProCamera2DShake.Instance.Shake(
            0.7f, //duration
            new Vector3(1, 1, 1) * nova.novaCharge, //strength
            10, //vibrato
            1, //randomness
            -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        nova.Activate();
        speedMod = 1;                
    }

    public void StunNova()
    {
        trigger2Down = false;
        UseSkill(1);
    }

}
