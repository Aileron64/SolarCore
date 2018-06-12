using UnityEngine;
using System.Collections;

public class ControlPoint : MonoBehaviour
{
    public GameObject turret;

    bool turretActive = false;

    float numOfCubes = 10;
    GameObject[] cube = new GameObject[10];
    GameObject[] cubeLight = new GameObject[10];

    public Material offLightMaterial;
    public Material onLightMaterial;

    public TextMesh debugtext;

    float time_r;
    float rotateSpeed = 0.1f;
    float targetRotateSpeed;

    float radius;
    float maxRadius = 400;
    float minRadius = 100;

    float delay;
    float captureRate = 25;

    bool active;
    bool testMode;

    bool onPoint;
    public bool GetState() { return turretActive; }

    public float influence;
    int influenceLevel = 10;
    const float MAX_INFLUENCE = 250;

    void Start()
    {
        delay = (Mathf.PI * 2) / cube.Length;

        for (int i = 0; i < 10; i++)
        {
            cube[i] = transform.Find("" + i).gameObject;
            cubeLight[i] = cube[i].transform.Find("Inner").gameObject;
            //cubeLight[i].GetComponent<Renderer>().material = offLightMaterial;
        }

        if (influence >= 100)
        {
            turret.GetComponent<ArcTurret>().Activate();
            turretActive = true;
        }
    }

    void Update()
    {

        //targetSize = player.GetComponent<Player>().GetHealth() / playerMaxHealth * (defaultSize.x - 5) + 5;

        //transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, targetSize, Time.smoothDeltaTime * 2), 
        //    transform.localScale.y, transform.localScale.z);


        // Update Turret

        if (turretActive)
        {
            if (influence < 100)
            {
                turret.GetComponent<ArcTurret>().Deactivate();
                turretActive = false;
            }

            targetRotateSpeed = 0.1f + (influence - 100) * 4 / (MAX_INFLUENCE - 100);
            rotateSpeed = Mathf.Lerp(rotateSpeed, targetRotateSpeed, Time.smoothDeltaTime * 2);
        }
        else
        {
            // Update Colours
            if (influenceLevel != ((int)influence / 10))
            {
                influenceLevel = (int)influence / 10;
                UpdateTextures(influenceLevel);
            }

            // Reduce rotatespeed if deactive
            if (rotateSpeed > 0.1f)
                rotateSpeed -= Time.deltaTime * 0.1f;
            else
                rotateSpeed = 0.1f;

            if (influence >= 100)
            {
                influence = MAX_INFLUENCE;
                turret.GetComponent<ArcTurret>().Activate();
                turretActive = true;
            }
        }

        // Update Influence
        if (onPoint)
        {
            if (influence < 100)
            {
                influence += Time.deltaTime * captureRate;

                if (rotateSpeed <= 3)
                    rotateSpeed += Time.deltaTime * 2f;
            }
            else
            {
                influence = MAX_INFLUENCE;
            }

        }
        else
        {
            if (influence > 0)
                influence -= Time.deltaTime * captureRate / 5;
            else
                influence = 0;
        }
    }

    void FixedUpdate()
    {
        time_r += Time.deltaTime * rotateSpeed;

        if(influence <= 100)
            radius = 400 - (300 * (influence / 100));
        else
            radius = 400 - (300);

        GetComponent<SphereCollider>().radius = radius;

        // Rotate Bars
        for (int i = 0; i < cube.Length; i++)
        {
            cube[i].transform.localPosition = new Vector3(
                Mathf.Sin(time_r + delay * i) * radius, 0, Mathf.Cos(time_r + delay * i) * radius);

            cube[i].transform.localRotation = Quaternion.Euler(new Vector3(
                0, Mathf.Rad2Deg * (time_r + delay * i), 0));
        }
    }

    protected virtual void UpdateTextures(int num)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < num)
                cubeLight[i].GetComponent<Renderer>().material = onLightMaterial;
            else
                cubeLight[i].GetComponent<Renderer>().material = offLightMaterial;
        }
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            onPoint = true;
        }
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            onPoint = false;
        }
    }


}
