using UnityEngine;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class Green : Player
{
    public GameObject laser_prefab;
    List<GameObject> laser = new List<GameObject>();

    public GameObject bomb_prefab;
    List<GameObject> bomb = new List<GameObject>();

    public GameObject nova_prefab;
    GameObject nova;

    GameObject shootPoint;

    public GameObject gun;
    public GameObject[] part;

    protected int[] amount = { 15, 25, 35 };
    protected float[] spread = { 12, 15, 19 };

    float bombTimer;
    float bombTime = 0.1f;
    bool queBomb = false;

    GameObject clone;

    float defaultSize = 12;

    protected override void Start()
    {
        // Bomb
        skillCost[0] = 300;
        skillMax[0] = skillCost[0] * 5;

        skillCost[1] = 200;
        skillMax[1] = skillCost[1];

        //// Detonate
        //skillCD[1] = 0;

        shootPower[0] = 900;
        shootPower[1] = 1000;
        shootPower[2] = 1100;

        shootDelay[0] = 0.5f;
        shootDelay[1] = shootDelay[0];
        shootDelay[2] = shootDelay[0];

        shootPoint = transform.Find("Shoot Point").gameObject;

        Pooler.Instantiate(laser, laser_prefab, 150);
        Pooler.Instantiate(bomb, bomb_prefab, 20);

        nova = Instantiate(nova_prefab);
        nova.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            part[i].transform.localPosition = part[i].transform.localPosition.normalized * defaultSize;
        }

        base.Start();
    }

    protected override void Update()
    {
        bombTimer += Time.deltaTime;

        if(queBomb && bombTimer >= bombTime)
        {
            UseSkill(0);
            bombTimer = 0;
        }

        base.Update();
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

            if (bombTimer >= bombTime)
            {
                UseSkill(0);
                bombTimer = 0;
            }
            else
            {
                queBomb = true;
            }
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0 && skillEnergy[1] >= skillCost[1])
        {
            UseSkill(1);
        }
    }

    protected override void Shoot()
    {
        for (int i = 0; i < 4; i++)
        {
            part[i].transform.localPosition = part[i].transform.localPosition.normalized * defaultSize;
            part[i].transform.DOLocalMove(part[i].transform.localPosition.normalized * 8f, 0.4f).From();
        }

        for (int i = 0; i < amount[weaponLevel]; i++)
        {
            clone = Pooler.GetObject(laser, shootPoint.transform.position, transform.rotation);

            float randDir = Random.Range(-spread[weaponLevel], spread[weaponLevel]);

            shootPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 135 
                + randDir, 0));

            clone.GetComponent<BaseAttack>().GetRigidBody().AddForce(shootPoint.transform.forward *
                (shootPower[weaponLevel] + Random.Range(-100, 100)) * (120 - (Mathf.Abs(randDir) * 3)) + velocity);

            clone.GetComponent<TrailRenderer>().Clear();
        }

        ProCamera2DShake.Instance.Shake(
            0.1f, //duration
            new Vector3(35, 35), //strength
            2, //vibrato
            0.5f, //randomness
            aimAngle * -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        bombTimer = 0;
    }


    // Bomb
    protected override void Skill_0()
    {
        for (int i = 0; i < 4; i++)
        {
            part[i].transform.localPosition = part[i].transform.localPosition.normalized * defaultSize;
            part[i].transform.DOLocalMove(part[i].transform.localPosition.normalized * 15f, 0.2f).From();
        }

        clone = Pooler.GetObject(bomb, transform.position, transform.rotation);

        clone.GetComponent<Bomb>().SetOwner(this.gameObject);
        clone.GetComponent<Bomb>().ownerNum = playerNum;

        clone.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * 400000 + velocity);

        if(nova.activeInHierarchy == true)
            clone.GetComponent<Bomb>().novaImune = 0.4f;

        shootTimer = shootDelay[weaponLevel];

        rB.AddForce(shootPoint.transform.forward * -60000);

        ProCamera2DShake.Instance.Shake(
            0.3f, //duration
            new Vector3(35, 35), //strength
            2, //vibrato
            0.5f, //randomness
            aimAngle * -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        queBomb = false;
    }


    // Detonate
    protected override void Skill_1()
    {
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

