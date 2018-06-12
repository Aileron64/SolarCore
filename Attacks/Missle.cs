using UnityEngine;
using System.Collections;

public class Missle : BaseAttack 
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

    protected override void OnEnd()
    {
        //GameObject clone = Instantiate(blackHole, transform.position, transform.rotation) as GameObject;
        Instantiate(missleExplosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
