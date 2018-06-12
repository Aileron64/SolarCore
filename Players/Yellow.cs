using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Yellow : Player
{
    public GameObject bubble_prefab;
    List<GameObject> bubble = new List<GameObject>();


    GameObject shootPoint;

    float spread = 15;

    public GameObject chrono;

    AimedCircle aimedCircle;
    public GameObject trackCircle;
    public GameObject aimedBlast;

    bool aimingBlast = false;

    GameObject prevNode;

    protected override void Start()
    {
        // Aimed Blast
        skillCost[0] = 800;
        skillMax[0] = skillCost[0];

        // Chrono
        skillCost[1] = 1000;
        skillMax[1] = skillCost[1];

        shootPower[0] = 700;
        shootPower[1] = 750;
        shootPower[2] = 850;

        shootDelay[0] = 0.03f;
        shootDelay[1] = 0.025f;
        shootDelay[2] = 0.02f;

        Pooler.Instantiate(bubble, bubble_prefab, 40);



        shootPoint = transform.Find("Shoot Point").gameObject;

        base.Start();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //if (!aimingBlast)
        //{


        //    rB.MoveRotation(Quaternion.Euler(rotation));
        //}
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
            AimedBlast();
        }

        if (Input.GetAxis("Left Trigger P" + playerNum) > 0)
        {
            UseSkill(1);
        }
    }

    protected override void Shoot()
    {
        GameObject clone = Pooler.GetObject(bubble, shootPoint.transform.position, transform.rotation);

        shootPoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 135 + Random.Range(-spread, spread), 0));
        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint.transform.forward * shootPower[weaponLevel] + velocity);

    }





    // Aimed Blast
    protected override void Skill_0()
    {
        aimingBlast = true;

        GameObject clone = Instantiate(trackCircle, shootPoint.transform.position, transform.rotation) as GameObject;
        clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        clone.GetComponent<BaseAttack>().SetVelocity(shootPoint.transform.forward * 1000);

        //clone.GetComponent<BaseAttack>().SetVelocity(new Vector3(
        //    Mathf.Cos(aimAngle * Mathf.Deg2Rad * -1), 0,
        //    Mathf.Sin(aimAngle * Mathf.Deg2Rad * -1)).normalized
        //    * 500);



        aimedCircle = clone.GetComponent<AimedCircle>();
        aimedCircle.owner = this;
    }

    public void AimedBlast()
    {
        if(aimingBlast)
        {
            aimingBlast = false;

            if (aimedCircle)
            {
                aimedCircle.EndTargeting();

                GameObject clone = Instantiate(aimedBlast, shootPoint.transform.position, transform.rotation) as GameObject;
                clone.GetComponent<AimedBlast>().target = aimedCircle;
            }
        }
    }

            // Web
    protected override void Skill_1()
    {
        GameObject clone = Instantiate(chrono, transform.position, transform.rotation) as GameObject;
    }

}
