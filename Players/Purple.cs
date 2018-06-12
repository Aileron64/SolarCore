using UnityEngine;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class Purple : Player
{
    public GameObject laser_prefab;
    List<GameObject> laser = new List<GameObject>();

    public GameObject blackHole_prefab;
    List<GameObject> blackHole = new List<GameObject>();

    GameObject shootPoint;

    public GameObject chrono;
    //public GameObject superNova;

    float blinkDistance = 300;
    float spread = 5;

    GameObject clone;

    protected override void Start()
    {
        // Black Hole
        skillCost[0] = 800;
        skillMax[0] = skillCost[0];

        // Super Nova
        skillCost[1] = 1400;
        skillMax[1] = skillCost[1];

        shootPower[0] = 1500;
        shootPower[1] = shootPower[0] + 50;
        shootPower[2] = shootPower[0] + 100;

        shootDelay[0] = 0.1f;
        shootDelay[1] = 0.08f;
        shootDelay[2] = 0.05f;

        shootPoint = transform.Find("Shoot Point").gameObject;

        Pooler.Instantiate(laser, laser_prefab, 42);
        Pooler.Instantiate(blackHole, blackHole_prefab, 8);

        base.Start();
    }

    protected override void ButtonInput()
    {
        if (Input.GetAxis("Right Trigger P" + playerNum) > 0 && skillEnergy[0] >= skillCost[0])
        {
            trigger1Down = true;
        }

        if (Input.GetAxis("Right Trigger P" + playerNum) == 0 && trigger1Down)
        {
            trigger1Down = false;
            UseSkill(0);
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0 && skillEnergy[1] >= skillCost[1])
        {
            trigger2Down = true;
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) == 0 && trigger2Down)
        {
            trigger2Down = false;
            UseSkill(1);
        }
    }

    protected override void Shoot()
    {
        clone = Pooler.GetObject(laser, shootPoint.transform.position, transform.rotation);

        float randAim = Random.Range(aimAngle - spread, aimAngle + spread);

        clone.GetComponent<BaseAttack>().SetVelocity(new Vector3(
            Mathf.Sin(randAim * Mathf.Deg2Rad), 0, Mathf.Cos(randAim * Mathf.Deg2Rad)).normalized * shootPower[weaponLevel] * -1);
        clone.GetComponent<TrailRenderer>().Clear();
    }

    // Black Hole
    protected override void Skill_0()
    {
        clone = Pooler.GetObject(blackHole, shootPoint.transform.position, transform.rotation);
        //clone.GetComponent<BaseAttack>().SetVelocity(shootPoint.transform.forward * 700);
        clone.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward *
            (shootPower[weaponLevel]) * 100 + velocity);


        ProCamera2DShake.Instance.Shake(
            0.4f, //duration
            new Vector3(75, 75), //strength
            2, //vibrato
            0.5f, //randomness
            aimAngle * -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        rB.AddForce(shootPoint.transform.forward * -60000);
    }

    // Super Nova
    protected override void Skill_1()
    {
        GameObject clone = Instantiate(chrono, transform.position, transform.rotation) as GameObject;
    }

}
