using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;

public class White : Player
{
    public GameObject laser;
    public GameObject aoe;
    public GameObject nova;

    GameObject orbs;
    GameObject[] orb = new GameObject[8];

    GameObject laserBeams;
    GameObject[] laserBeam = new GameObject[8];


    //const int SKILL_CHARGES = 50;
    //const int SKILL_RECHARGE = 2;
    //const int SKILL_COST = 5;

    float time_r;

    float orbRadius = 100;
    float orbSpeed = 2;
    float orbDelay;

    float targetRadius;

    float maxRadius = 250;
    float minRadius = 50;

    float orbExpandSpeed = 500;

    float orbShootDelay = 0.1f;
    float orbShootTimer;

    bool lasers = false;

    Slider orbSlider;


    protected override void Start()
    {
        //skillCost[0] = SKILL_COST;
        //skillCD[0] = SKILL_RECHARGE;
        //maxCharges[0] = SKILL_CHARGES;

        //skillCost[1] = 0.2f;
        //skillCD[1] = 0;
        //maxCharges[1] = 1;

        //Laser Orbs
        skillCost[0] = 100;
        skillMax[0] = skillCost[0] * 20;

        //Laser Beams
        skillCost[1] = 300;
        skillMax[1] = skillCost[1] * 5;

        targetRadius = orbRadius;
        orbDelay = (Mathf.PI * 2) / orb.Length;

        shootPower[0] = 400;
        shootPower[1] = shootPower[0];
        shootPower[2] = shootPower[0];

        orbs = transform.Find("Orbs").gameObject;
        laserBeams = transform.Find("LaserBeams").gameObject;

        for (int i = 0; i < orb.Length; i++)
        {
            orb[i] = orbs.transform.Find("" + i).gameObject;
            laserBeam[i] = laserBeams.transform.Find("" + i).gameObject;
        }

        orbSlider = CombatBars.Instance.GetOrbBar();
        //orbSlider.gameObject.SetActive(true);

        Camera.main.GetComponent<ProCamera2DPointerInfluence>().enabled = false;

        base.Start();
    }

    protected override void FixedUpdate()
    {
        orbShootTimer += Time.deltaTime;

        if(lasers)
        {
            rotation.y += Time.deltaTime * 20;

            skillEnergy[1] -= skillCost[1] * 2 * Time.deltaTime;

            if (skillEnergy[1] <= 0)
                DeactivateLasers();
        }
        else
        {
            time_r += Time.deltaTime * orbSpeed;
        }

        speedMod = 0.8f + (0.08f * (maxRadius / orbRadius));

        for (int i = 0; i < orb.Length; i++)
        {
            orb[i].transform.localPosition = new Vector3(
                Mathf.Sin(time_r + orbDelay * i) * orbRadius, 0, Mathf.Cos(time_r + orbDelay * i) * orbRadius);

            laserBeam[i].transform.localPosition = orb[i].transform.localPosition;

        }

        base.FixedUpdate();
    }

    protected override void ButtonInput()
    {

        if (Input.GetMouseButton(0) && orbShootTimer >= orbShootDelay && playerNum == 1)
        {
            orbShootTimer = 0;
            ToggleInput(true);
            UseSkill(0);
        }


        if (Input.GetAxis("Right Trigger P" + playerNum) > 0 && orbShootTimer >= orbShootDelay)
        {
            orbShootTimer = 0;
            UseSkill(0);
        }

        if (!lasers)
        {
            if (Input.GetAxis("Left Trigger P" + playerNum) > 0 && skillEnergy[1] >= skillCost[1])
            {
                ActivateLasers();
            }
        }

        if (lasers)
        {
            if (Input.GetAxis("Left Trigger P" + playerNum) <= 0)
            {
                DeactivateLasers();
            }
        }

        if (Input.GetButton("2nd Skill P" + playerNum))
        {
            orbRadius += 5;
        }

        if (Input.GetButton("3rd Skill P" + playerNum))
        {
            orbRadius -= 5;
        }
    }

    // Controls the radius of the orbs rather than aim
    protected override void AimInput()
    {

        if (Input.GetAxis("Vertical Aim P" + playerNum) != 0)
        {
            ToggleInput(false);
            orbRadius += Input.GetAxis("Vertical Aim P1") * orbExpandSpeed * Time.deltaTime;
        }

        //MouseClickControls();

        MouseWheelControls();

        //SliderControls();



        if (orbRadius > maxRadius)
            orbRadius = maxRadius;

        if (orbRadius < minRadius)
            orbRadius = minRadius;
    }

    void SliderControls()
    { 
        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    orbSlider.gameObject.SetActive(true);
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    orbSlider.gameObject.SetActive(false);
        //}



        orbRadius = minRadius + ((maxRadius - minRadius) * orbSlider.value);
    }

    public void MouseWheelControls()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && playerNum == 1)
        {
            ToggleInput(true);
            orbRadius += 15;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && playerNum == 1)
        {
            ToggleInput(true);
            orbRadius -= 15;
        }
    }

    //Turning this because its stupid
    public void MouseClickControls()
    {
        if (inputType == InputType.MOUSE && playerNum == 1)
        {
            if (orbRadius < (targetRadius - 20))
            {
                orbRadius += Time.deltaTime * orbExpandSpeed;
            }
            else if (orbRadius > (targetRadius + 20))
            {
                orbRadius -= Time.deltaTime * orbExpandSpeed;
            }
        }

        if (Input.GetMouseButton(0) && playerNum == 1)
        {
         // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, 3000f, floorMask))
            {
                float distance = Vector3.Magnitude(floorHit.point - transform.position);

                //distance -= 525;
                distance *= 0.8f;
                //Debug.Log(distance);
                targetRadius = distance;

            }

            ToggleInput(true);
        }
    }

    // Orb Cannon
    protected override void Skill_0()
    {
        for (int i = 0; i < orb.Length; i++)
        {
            GameObject clone = Instantiate(laser, new Vector3(orb[i].transform.position.x, 0, orb[i].transform.position.z),
                transform.rotation) as GameObject;
            clone.GetComponent<BaseAttack>().SetVelocity(
                Vector3.Normalize(orb[i].transform.position - transform.position) * shootPower[weaponLevel]);
        }
    }

    //// Laser Beams
    //protected override void Skill_1()
    //{   // Button Down
    //    if (!lasers)
    //    {
    //        lasers = true;
    //        laserBeams.SetActive(true);

    //        //for (int i = 0; i < orb.Length; i++)
    //        //{
    //        //    //orb[i].GetComponent<WispOrb>().ActivateLaser(true);
    //        //    laserBeam[i].transform.localPosition = orb[i].transform.localPosition;
    //        //}
    //    }

    //    //energy -= Time.deltaTime * 25;
    //}

    void ActivateLasers()
    {

        lasers = true;
        laserBeams.SetActive(true);
        
    }

    void DeactivateLasers()
    {   // Button Up
        //for (int i = 0; i < orb.Length; i++)
        //{
        //    orb[i].GetComponent<WispOrb>().ActivateLaser(false); 
        //}
        laserBeams.SetActive(false);
        lasers = false;
    }

    // AoE
    //protected override void Skill_2()
    //{
    //    orbRadius += 5;
    //}

    //// Force Nova
    //protected override void Skill_3()
    //{
    //    orbRadius -= 5;

    //}



}

