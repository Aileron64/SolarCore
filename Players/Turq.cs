using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class Turq : Player
{

    public GameObject laser;
    public GameObject laser2;
    public GameObject laser3;
    //public GameObject rapidLaser;

    //public GameObject nova;
    GameObject shootPoint;
    GameObject shootPoint2;
    GameObject bubble;

    float recoil = -10;
    float recoilMod;
    float rapidSpread = 40;

    // Bubble
    float bubbleTime = 3;
    float bubbleTimer;
    bool isBubbled = false;
    public Material bubbleMat;

    SphereCollider col;

    // Chain Novas
    float novaDelay = 0.5f;
    float novaTimer;
    int incNovas = 0;
    int numOfNovas = 5;

    protected override void Start()
    {
        //// Bubble
        skillCost[1] = 1200;
        skillMax[1] = skillCost[1];

        speedMod = 0.5f;

        shootPower[0] = 500;
        shootPower[1] = shootPower[0];
        shootPower[2] = shootPower[0];

        shootDelay[0] = 0.12f;
        shootDelay[1] = shootDelay[0];
        shootDelay[2] = shootDelay[0];

        bubble = transform.Find("Bubble").gameObject;
        bubble.GetComponent<Bubble>().owner = this;

        shootPoint = transform.Find("Shoot Point").gameObject;

        col = GetComponent<SphereCollider>();
        Camera.main.GetComponent<ProCamera2DPointerInfluence>().enabled = false;

        base.Start();

        rB.drag = 5;
        speed = 4000;
    }

    protected override void Update()
    {
        if (isBubbled)
        {
            bubbleTimer += Time.deltaTime;

            if (bubbleTimer >= bubbleTime)
            {
                bubbleTimer = 0;
                isBubbled = false;
                ChangMat(defaultMat);
                bubble.GetComponent<Bubble>().Close();
                col.radius = 20;
                knockBackMod = 1;
            }
        }

        //if (incNovas > 0)
        //{
        //    novaTimer += Time.deltaTime;

        //    if (novaTimer >= novaDelay)
        //    {
        //        RapidNova();
        //        incNovas--;
        //        novaTimer = 0;
        //    }
        //}

        base.Update();
    }

    protected override void ButtonInput()
    {
        if (Input.GetAxis("Right Trigger P" + playerNum) > 0 && skillEnergy[0] >= skillCost[0])
        {
            trigger1Down = true;
            Camera.main.GetComponent<ProCamera2DPointerInfluence>().enabled = true;
        }

        if (Input.GetAxis("Right Trigger P" + playerNum) == 0 && trigger1Down)
        {
            trigger1Down = false;
            Camera.main.GetComponent<ProCamera2DPointerInfluence>().enabled = false;
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0)
        {
            UseSkill(1);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!isBubbled)
        {
            base.TakeDamage(damage);
        }
            
    }


    protected override void Shoot()
    {
        float spread = 8;

        switch (weaponLevel)
        {
            default:
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    GameObject clone;
                    float powerMod;
                    float angle = aimAngle + (spread * (i - 2));

                    if (i % 2 == 0)
                    {
                        clone = laser;
                        powerMod = 1;
                    }
                    else
                    {
                        clone = laser2;
                        powerMod = 0.9f;
                    }

                    clone = Instantiate(clone, shootPoint.transform.position, transform.rotation) as GameObject;

                    clone.GetComponent<Laser>().SetVelocity(new Vector3(
                        Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)).normalized * -1
                        * shootPower[weaponLevel] * powerMod);

                    //clone.GetComponent<Laser>().SetVelocity(new Vector3(
                    //    Mathf.Cos(angle * Mathf.Deg2Rad * -1), 0, Mathf.Sin(angle * Mathf.Deg2Rad * -1)).normalized
                    //    * shootPower[weaponLevel] * powerMod);

                }
                break;

            case 1:
                for (int i = 0; i < 5; i++)
                {
                    GameObject clone;
                    float powerMod;
                    float angle = aimAngle + (spread * (i - 3));

                    if (i % 2 == 0)
                    {
                        clone = laser;
                        powerMod = 1;
                    }
                    else
                    {
                        clone = laser2;
                        powerMod = 0.9f;
                    }

                    clone = Instantiate(clone, shootPoint.transform.position, transform.rotation) as GameObject;

                    clone.GetComponent<Laser>().SetVelocity(new Vector3(
                        Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)).normalized * -1
                        * shootPower[weaponLevel] * powerMod);

                }
                break;

            case 2:
                for (int i = 0; i < 7; i++)
                {
                    GameObject clone;
                    float powerMod;
                    float angle = aimAngle + (spread * (i - 4));

                    if (i == 3)
                    {
                        clone = laser3;
                        powerMod = 1.1f;
                    }
                    else if (i % 2 == 0)
                    {
                        clone = laser;
                        powerMod = 1;
                    }
                    else
                    {
                        clone = laser2;
                        powerMod = 0.9f;
                    }

                    clone = Instantiate(clone, shootPoint.transform.position, transform.rotation) as GameObject;

                    clone.GetComponent<Laser>().SetVelocity(new Vector3(
                        Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)).normalized * -1
                        * shootPower[weaponLevel] * powerMod);

                }
                break;

        }

        if (trigger1Down)
            recoilMod = 0.2f;
        else
            recoilMod = 1;

        Vector3 aimDirection = new Vector3(
            Mathf.Sin(aimAngle * Mathf.Deg2Rad), 0, Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized * -1;

        rB.AddForce(aimDirection * recoil * recoilMod * speed);
    }



    protected override void Skill_0() { }

    // Bubble
    protected override void Skill_1()
    {
        ChangMat(bubbleMat);
        isBubbled = true;
        bubble.SetActive(true);
        //StatusBars.StatusBar(bubbleTime, "Shield");
        knockBackMod = 5;
        col.radius = 85;
    }

    //public void EndBubble()
    //{
        
    //}

    // Nova
    //protected override void Skill_3()
    //{
    //    incNovas += numOfNovas;
    //    novaTimer = 0;
    //    //RapidNova();
    //}

    //void RapidNova()
    //{
    //    GameObject clone = Instantiate(nova, transform.position, transform.rotation) as GameObject;
    //    clone.GetComponent<BaseAttack>().SetPower(power);
    //    clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    //    clone.GetComponent<Nova>().FollowOwner(this.gameObject.transform);
    //}
}
