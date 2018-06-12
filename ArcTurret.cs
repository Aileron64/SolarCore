using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ArcTurret : MonoBehaviour
{
    public GameObject arcLaser;
    public ParticleSystem laserExplosion;
    public GameObject orb;
    public GameObject[] part = new GameObject[5];

    Vector3[] partPosOrigin = new Vector3[5];
    Quaternion[] partRotOrigin = new Quaternion[5];

    private GameObject[] target = new GameObject[3];
    private GameObject[] enemies;

    public Material offLightMaterial;
    public Material onLightMaterial;

    float targetRadius = 1200;
    float bounceRadius = 600;
    float laserDamage = 30;

    bool active = false;

    float orbScale = 0;

    void Start()
    {
        BaseLevel.OnBeat += FindTargets;

        for(int i = 0; i < 5; i++)
        {
            partPosOrigin[i] = part[i].transform.localPosition;
            partRotOrigin[i] = part[i].transform.localRotation;
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= FindTargets;
    }

    void FixedUpdate()
    {
        if(active)
        {
            //shootTimer += Time.deltaTime;

            //if (shootTimer >= shootDelay)
            //{
            //    FindTargets();
            //    shootTimer = 0;
            //}

            if (orbScale <= 50)
            {
                orbScale += Time.deltaTime * 100;

                orb.transform.localScale = new Vector3(orbScale, orbScale, orbScale);
            }
            
        }
        else
        {

            if (orbScale >= 0)
            {
                orbScale -= Time.deltaTime * 100;

                orb.transform.localScale = new Vector3(orbScale, orbScale, orbScale);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (active)
                Deactivate();
            else
                Activate();
        }

    }

    void FindTargets()
    {
        if (active)
        {
            float minDistance = targetRadius;

            //Find closest enemy in range
            foreach (Collider col in Physics.OverlapSphere(transform.position, targetRadius))
            {
                if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<BaseEnemy>() && col.gameObject.activeSelf)
                {
                    float distance = (col.gameObject.transform.position - orb.transform.position).magnitude;

                    if (distance <= minDistance)
                    {
                        minDistance = distance;
                        target[0] = col.gameObject;
                    }
                }
            }

            //Blow it up
            if (target[0])
            {
                Shoot(0);

                // 2nd Bounch
                minDistance = bounceRadius;
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<BaseEnemy>())
                    {
                        float distance = (target[0].transform.position - enemy.transform.position).magnitude;

                        if (distance <= minDistance && enemy != target[0])
                        {
                            minDistance = distance;
                            target[1] = enemy;
                        }
                    }

                }

                if (target[1])
                {
                    Shoot(1);

                    // 3rd Bounch
                    minDistance = bounceRadius;

                    foreach (GameObject enemy in enemies)
                    {
                        if (enemy.GetComponent<BaseEnemy>())
                        {
                            float distance = (target[1].transform.position - enemy.transform.position).magnitude;

                            if (distance <= minDistance && enemy != target[0] && enemy != target[1])
                            {
                                minDistance = distance;
                                target[2] = enemy;
                            }
                        }
                    }

                    if (target[2])
                    {
                        Shoot(2);
                    }
                }
            }
        }

        // Prevent targets from being hit again
        target[0] = null;
        target[1] = null;
        target[2] = null;
    }

    public void Activate()
    {
        active = true;

        part[0].transform.DOLocalMove(new Vector3(0, -50f, 0), 0.5f);
        part[1].transform.DOLocalMove(new Vector3(6, -1.5f, 6), 0.5f);
        part[2].transform.DOLocalMove(new Vector3(6, -1.5f, -6), 0.5f);
        part[3].transform.DOLocalMove(new Vector3(-6, -1.5f, -6), 0.5f);
        part[4].transform.DOLocalMove(new Vector3(-6, -1.5f, 6), 0.5f);

        part[1].transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        part[2].transform.DOLocalRotate(new Vector3(0, 90, 0), 0.5f);
        part[3].transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);
        part[4].transform.DOLocalRotate(new Vector3(0, 270, 0), 0.5f);

        for (int i = 0; i < 5; i++)
        {
            part[i].GetComponent<Renderer>().material = onLightMaterial;
        }
    }

    public void Deactivate()
    {
        active = false;

        for (int i = 0; i < 5; i++)
        {
            part[i].transform.DOLocalMove(partPosOrigin[i], 1.5f);
            part[i].transform.DOLocalRotateQuaternion(partRotOrigin[i], 1.5f);

            part[i].GetComponent<Renderer>().material = offLightMaterial;
        }
    }

    void Shoot(int num)
    {
        target[num].GetComponent<BaseEnemy>().TakeDamage(laserDamage, transform.position);

        GameObject clone = Instantiate(arcLaser, transform.position, Quaternion.identity) as GameObject;
        LineRenderer line = clone.GetComponent<LineRenderer>();

        line.SetWidth(10, 10);

        if(num <= 0)
            line.SetPosition(0, orb.transform.position);
        else
            line.SetPosition(0, target[num - 1].transform.position);

        line.SetPosition(1, target[num].transform.position);

        Instantiate(laserExplosion, target[num].transform.position, Quaternion.identity);
    }
}
