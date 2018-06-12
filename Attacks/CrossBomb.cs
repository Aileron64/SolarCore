using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrossBomb : MonoBehaviour
{
    LineRenderer circle;

    public GameObject explosion;

    float size = 0;
    float maxSize = 5;
    private object lines;

    float beatTime;
    float timer;

    int beatCount = 0;

    void Start ()
    {
        beatTime = Object.FindObjectOfType<BaseLevel>().GetBeatTime();
    }

    void FixedUpdate()
    {
        if(timer <= beatTime)
        {
            timer += Time.deltaTime;

            if(beatCount == 0)
            {
                size = (timer / beatTime) * maxSize;
                transform.localScale = new Vector3(size, 1, size);
            }

        }
        else
        {
            switch (beatCount)
            {
                default:
                case 0:     // Gets bigger
                    break;

                case 1:     // Explosion
                    GameObject clone = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                    clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                    break;

                case 2:     // Destroy Self
                    Destroy(this.gameObject);
                    break;
            }

            beatCount++;
            timer = 0;
        }

        
    }
}
