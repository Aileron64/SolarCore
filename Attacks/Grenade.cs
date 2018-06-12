using UnityEngine;
using System.Collections;

public class Grenade : BaseAttack
{
    public GameObject missleExplosion;

    protected override void Update()
    {
        base.Update();
    }

    void DetonateInput()
    {
        if (Input.GetAxis("Triggers P1") < 0)
        {
            OnEnd();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnEnd();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" 
            || col.gameObject.tag == "Red Sun" 
            || col.gameObject.tag == "Enemy Laser")
        {
            OnEnd();
        }
    }

    protected override void OnEnd()
    {
        GameObject clone = Instantiate(missleExplosion, transform.position, transform.rotation) as GameObject;
        clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        Destroy(this.gameObject);
    }
}
