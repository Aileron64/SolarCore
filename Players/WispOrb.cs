using UnityEngine;
using System.Collections;

public class WispOrb : BaseAttack 
{
    float orbDamage = 500;
    GameObject parent;
    Player wisp;


    protected override void Start()
    {
        orbDamage *= 0.5f + (0.1f * power);

        parent = transform.parent.gameObject;
        wisp = parent.transform.parent.gameObject.GetComponent<Player>();

	}

    protected override void Update()
    {
        damage = orbDamage * (1 + 0.5f * wisp.GetWeaponLevel());
    }

}
