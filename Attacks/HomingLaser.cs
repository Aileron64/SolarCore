using UnityEngine;
using System.Collections;

public class HomingLaser : BaseAttack 
{
    public GameObject target;

    float speed = 600;

	void Awake()
    {
        lifeTime = 5;

        //target = GameObject.FindWithTag("Player");   
    }
	

	protected override void Update()
    {
        //velocity = Vector3.MoveTowards(transform.position, target.transform.position, speed);

        if (target)
            velocity = Vector3.Normalize(target.transform.position - transform.position) * speed;
        //else
        //{
        //    Explode();
        //    Destroy(this.gameObject);
        //}
            

        base.Update();
    }
 
}
