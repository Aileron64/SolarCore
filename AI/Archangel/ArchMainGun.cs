using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArchMainGun : MonoBehaviour
{
    public GameObject laser;
    BoxCollider col;

    Transform player;
    float beatTime;

    Vector3 tarRot;
    bool rotate;
    float rotSpeed;
    float targetRotSpeed;

    bool sideToggle;

    void Start()
    {
        beatTime = BaseLevel.Instance.GetBeatTime();
        player = PlayerManager.Instance.GetPlayer(0).transform;

        col = laser.GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        if(rotate)
        {
            rotSpeed = Mathf.Lerp(rotSpeed, targetRotSpeed, Time.smoothDeltaTime);

            tarRot = player.position - transform.position;
            tarRot.y = 0;
            //tarRot.z = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,
                tarRot, Time.smoothDeltaTime * rotSpeed, 0.0F));
        }
    }

    public void StartLaser()
    {
        if(sideToggle)
            transform.DOLocalRotate(new Vector3(0, 35, 0), beatTime * 2);
        else
            transform.DOLocalRotate(new Vector3(0, -35, 0), beatTime * 2);

        sideToggle = !sideToggle;

        Invoke("ActivateLaser", beatTime * 2);
    }

    void ActivateLaser()
    {
        rotate = false;
        Invoke("MoveLaser", beatTime);
        rotSpeed = 0;
        laser.transform.DOScale(new Vector3(25, 5, 1000), beatTime * 2);
    }

    void MoveLaser()
    {
        rotate = true;
        targetRotSpeed = 0.32f * Music.Instance.GetComponent<AudioSource>().pitch; 
        col.enabled = true;
        Invoke("EndLaser", beatTime * 9);
    }

    void EndLaser()
    {
        rotate = false;
        col.enabled = false;
        laser.transform.DOScale(new Vector3(0, 0, 1000), 0.1f);
        //Debug.Log("End Time: " + BaseLevel.Instance.GetBeatNum());
    }

    public void ResetRotation()
    {       
        transform.DOLocalRotate(Vector3.zero, 1f);    
    }


}
