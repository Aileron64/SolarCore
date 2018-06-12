using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Radar : MonoBehaviour 
{
    float maxDistance = 2000;
    float distanceMod;

    GameObject player;
    GameObject solarIcon;
    GameObject solarPointer;
    Vector3 solarPointerRotation;

    GameObject[] enemyDots = new GameObject[100];
    GameObject[] enemies;

    List<GameObject> enemySqaures = new List<GameObject>();

    float x;
    float y;

    RawImage pointer;

    public GameObject redSquare;

    Vector3 pointerRotation;
    Transform core;


	void Start () 
    {
        //player = GameObject.FindWithTag("Player");

        pointer = transform.Find("Pointer").GetComponent<RawImage>();
        solarIcon = transform.Find("Solar Core Icon").gameObject;
        solarPointer = transform.Find("Solar Pointer").gameObject;
        //enemyDots = transform.FindChild("Enemy Dots").gameObject;


        for (int i = 0; i < 500; i++)
        {
            enemySqaures.Add(Instantiate(redSquare) as GameObject);
            enemySqaures[i].transform.SetParent(this.transform);
        }

        core = GameObject.FindWithTag("Core").transform;

        distanceMod = 100 / maxDistance;

        SetPlayer(1);
	}

    public void SetPlayer(int playerNum)
    {
        player = GameObject.FindWithTag("Player");
    }




	void LateUpdate () 
    {
        if(player)
        {
            // Player Pointer
            pointerRotation.z = player.GetComponent<Player>().GetRot() * -1;
            pointer.transform.rotation = Quaternion.Euler(pointerRotation);

            // Solar Core Icon   // If it leaves the mini map, replace it with a pointer pointing at it
            if (solarIcon.activeSelf && Vector3.Magnitude(core.position - player.transform.position) > maxDistance)
            {
                solarIcon.SetActive(false);
                solarPointer.SetActive(true);
            }

            if (!solarIcon.activeSelf && Vector3.Magnitude(core.position - player.transform.position) <= maxDistance)
            {
                solarIcon.SetActive(true);
                solarPointer.SetActive(false);
            }

            solarIcon.transform.position = MapDistance(core.position);

            if (!solarIcon.activeSelf)
            {
                solarPointerRotation.z = (Mathf.Atan2(player.transform.position.z - core.position.z,
                    player.transform.position.x - core.position.x) * Mathf.Rad2Deg) + 180;

                solarPointer.transform.rotation = Quaternion.Euler(solarPointerRotation);
            }

            EnemyUpdate();

        }
	}

    void EnemyUpdate()
    {
        // Enemies


        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int x = enemies.Length - enemySqaures.Count;

        if (x > 0)
        {
            for (int i = 0; i < x; i++)
            {
                enemySqaures.Add(Instantiate(redSquare) as GameObject);
                enemySqaures[i].transform.parent = transform;
            }
        }

        for (int i = 0; i < enemySqaures.Count; i++)
        {
            if (i < enemies.Length)
            {
                if (Vector3.Magnitude(player.transform.position - enemies[i].transform.position) <= maxDistance)
                {
                    enemySqaures[i].transform.position = MapDistance(enemies[i].transform.position);
                }
                else
                    enemySqaures[i].transform.position = Vector3.zero;
            }
            else
                enemySqaures[i].transform.position = Vector3.zero;
        }
    }

    Vector3 MapDistance(Vector3 pos)
    {
        x = (pos.z - player.transform.position.z) * distanceMod * -1 + transform.position.x;
        y = (pos.x - player.transform.position.x) * distanceMod + transform.position.y;

        return new Vector3(x, y, 0);
    }
}
