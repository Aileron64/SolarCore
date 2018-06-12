using UnityEngine;
using System.Collections;

public class Flame : BaseAttack 
{
    float size = 0;
    float flameDilation;

    protected override void OnEnable()
    {
        size = 5;
        flameDilation = 1;
    }

    protected override void Update()
    {
        size += Time.deltaTime * 50 * flameDilation;
        transform.localScale = new Vector3(size, size, size);

        lifeTimer += Time.deltaTime * flameDilation;

        if (lifeTimer >= lifeTime && lifeTime != 0)
        {
            lifeTimer = 0;
            OnEnd();
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Enemy" 
            || col.gameObject.tag == "Block")
        {
            velocity = Vector3.zero;
        }

        if (col.gameObject.tag == "Chrono")
        {
            flameDilation = 0.2f;
        }
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            flameDilation = 1;
        }
    }

}
