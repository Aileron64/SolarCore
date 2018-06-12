using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static void Instantiate(List<GameObject> pool, GameObject obj, int size)
    {
        for (int i = 0; i < size + 1; i++)
        {
            pool.Add(Instantiate(obj) as GameObject);      
            pool[i].name = pool[i].name.Split('(')[0];
            pool[i].gameObject.SetActive(false);
        }
    }

    public static GameObject GetObject(List<GameObject> pool, Vector3 position, Quaternion rotation)
    {
        for (int i = 1; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].transform.position = position;
                pool[i].transform.rotation = rotation;
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject obj = Instantiate(pool[0], position, rotation) as GameObject;
        obj.name = obj.name.Split('(')[0];
        pool.Add(obj);
        Debug.Log(pool[0].name + " Overflow - Size: " + pool.Count);
        return obj;
    }

    public static GameObject GetObject(List<GameObject> pool, GameObject obj, Vector3 position, Quaternion rotation)
    {
        for (int i = 1; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].transform.position = position;
                pool[i].transform.rotation = rotation;
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        //GameObject newObj = Instantiate(obj, position, rotation) as GameObject;

        pool.Add(Instantiate(obj) as GameObject);
        pool[pool.Count - 1].name = pool[pool.Count - 1].name.Split('(')[0];
        pool[pool.Count - 1].transform.position = position;
        pool[pool.Count - 1].transform.rotation = rotation;
        //pool.Add(obj);
        Debug.Log(pool[0].name + " Overflow - Size: " + pool.Count);
        return obj;
    }
}
