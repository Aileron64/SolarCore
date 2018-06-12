using UnityEngine;
using System.Collections;

public class BHMissle : BaseAttack 
{
    public GameObject blackHole;

    protected override void OnEnd()
    {
        Instantiate(blackHole, transform.position, Quaternion.identity);
        base.OnEnd();
    }

    void OnTriggerEnter(Collider col)
    {
        if (side == Team.BLUE)
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Red Sun")
            {
                OnEnd();
            }

            if (col.gameObject.tag == "Red Laser")
            {
                OnEnd();
            }
        }

        if (side == Team.RED)
        {
            if (col.gameObject.tag == "Chrono")
            {
                timeDilation = 0.2f;
            }
        }

    }
}
