using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public GameObject exp_prefab;
    public GameObject redLaser_prefab;

    List<GameObject> exp = new List<GameObject>();
    List<GameObject> redLaser = new List<GameObject>();

    GameObject clone;

    public int laserCount;

    static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPool>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ObjectPool>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        Pooler.Instantiate(exp, exp_prefab, 100);
        Pooler.Instantiate(redLaser, redLaser_prefab, laserCount);

        //DebugText.Instance.SetText("" + laserCount);
    }

    public GameObject GetLaser(Vector3 pos)
    {
        return Pooler.GetObject(redLaser, pos, transform.rotation);
    }

    public void ExpExplosion(float value, Vector3 pos)
    {
        float expSpeed = 5000 + 2000 * value;

        for (int i = 0; i <= (value / 3) + 1; i++)
        {
            clone = Pooler.GetObject(exp, pos, transform.rotation);
   
            clone.GetComponent<Rigidbody>().AddForce(new Vector3(
                Random.Range(-expSpeed, expSpeed),
                Random.Range(-10, 10),
                Random.Range(-expSpeed, expSpeed)));
        }
    }

    void OnDisable()
    {

    }
}
