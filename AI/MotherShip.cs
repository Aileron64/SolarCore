using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MotherShip : BaseEnemy 
{
    public GameObject bottom;
    public Transform spawnPoint;

    float rotSpeed = 10;

    BaseLevel LM;

    override protected void Start() 
    {
        LM = Object.FindObjectOfType<BaseLevel>();


        base.Start();
	}

    public override void OnSpawn()
    {
        transform.rotation = Quaternion.identity;
        base.OnSpawn();
    }

    override protected void Normal() 
    {
        transform.Rotate(new Vector3(0, rotSpeed, 0) * Time.deltaTime * timeDilation);
        bottom.transform.Rotate(new Vector3(0, -rotSpeed * 1.5f, 0) * Time.deltaTime * timeDilation);
    }

    override protected void OnBeat()
    {

        spawnPoint.rotation = Quaternion.Euler(0, (Mathf.Atan2(transform.position.x - target.transform.position.x,
                transform.position.z - target.transform.position.z) * Mathf.Rad2Deg) + Random.Range(-75, 75), 0);

        float radius = Random.Range(-160, -300);

        LM.SpawnMini(spawnPoint.forward.x * radius, spawnPoint.forward.z * radius, this.transform.position);

    }
}
