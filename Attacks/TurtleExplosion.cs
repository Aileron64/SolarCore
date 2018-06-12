using UnityEngine;
using System.Collections;

public class TurtleExplosion : EnemyLaser 
{
    float maxSize = 200;
    float size = 1;
    float timer;

	void Awake () 
    {
        lifeTime = 1f;
        damage = 50;
	}

	void Update () 
    {
        timer += Time.deltaTime;

        if (size < maxSize)
        {
            size += Time.deltaTime * 300 * timeDilation;
            transform.localScale = new Vector3(size, size, size);
        }
        else
        {
            size = maxSize;
            transform.localScale = new Vector3(size, size, size);
        }
	}

    protected virtual void OnTriggerEnter(Collider col)
    {

    }
}
