using UnityEngine;
using System.Collections;

public class DeleteItself : MonoBehaviour
{
    public float lifeTime = 10;

    float timer;

    public bool deleteObject;

    void OnEnable()
    {
        timer = 0;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            if(!deleteObject)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
