using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StunNova : BaseAttack
{
    float maxStun;

    float expandSpeed = 3000;
    float unActiveSize = 100;

    float size;
    float timer;
    float maxDamage;

    LineRenderer circle;

    float lineWidth;

    public float novaCharge;

    bool active;


    float novaPower;
    const float MAX_NOVA_POWER = 100;
    const float NOVA_CHARGE_SPEED = 100;
    //Slider novaPowerBar;

    public Blue owner;

    protected override void Start()
    {
        lifeTime = 2;
        circle = GetComponent<LineRenderer>();
    }

    protected override void OnEnable()
    {
        timer = 0;
        active = false;

        //novaPowerBar = CombatBars.Instance.GetBar("Stun Nova");

        //if (circle)
         //   DrawCircle((size / 2) - lineWidth, circle);

        GetComponent<Collider>().enabled = false;

        base.OnEnable();
    }

    void OnDisable()
    {
        novaCharge = 0;

        size = 10;
        transform.localScale = new Vector3(size, size, 0);

        //novaPowerBar.gameObject.SetActive(false);
    }


    protected override void Update()
    {
        if(active)
        {
            timer += Time.deltaTime;

            damage = maxDamage * ((lifeTime - timer) / lifeTime);
            stunTime = maxStun * ((lifeTime - timer) / lifeTime);

            lineWidth = novaCharge + 10;

            size += Time.deltaTime * expandSpeed;
            base.Update();
        }
        else
        {
            transform.position = owner.transform.position;

            if (size < unActiveSize)
                size += Time.deltaTime * expandSpeed * 0.2f;
            else
                size += Time.deltaTime * expandSpeed * 0.02f;
            //size = unActiveSize;

            if (novaCharge < MAX_NOVA_POWER)
                novaCharge += Time.deltaTime * 50;
            else
            {
                novaCharge = MAX_NOVA_POWER;
                owner.StunNova();
            }

            //novaPowerBar.value = novaCharge / MAX_NOVA_POWER;
            lineWidth = (novaCharge * 0.3f) + 5;
        }

        transform.localScale = new Vector3(size, size, 0);
        DrawCircle((size / 2), circle);

    }

    public void Activate()
    {      
        active = true;

        maxDamage = (novaCharge * 2) + 40;
        maxStun = (novaCharge * 0.05f) + 2;
        
        GetComponent<Collider>().enabled = true;
        //novaPowerBar.gameObject.SetActive(false);
    }

    void DrawCircle(float _radius, LineRenderer line)
    {
        float theta_scale = 0.1f;             //Set lower to add more points
        int size = (int)((2.0f * 3.14f) / theta_scale) + 10; //Total number of points in circle.

        line.startWidth = lineWidth;
        line.endWidth = line.startWidth;
        line.positionCount = size;

        float x = 0;
        float z = 0;

        for (int i = 0; i < size; i++)
        {
            x = _radius * Mathf.Cos(i * theta_scale);
            z = _radius * Mathf.Sin(i * theta_scale);

            Vector3 pos = new Vector3(x, 0, z) + transform.position;

            line.SetPosition(i, pos);
            //i += 1;
        }
    }
}
