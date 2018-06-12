using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class HiveBomb : BaseEnemy
{
    const float SLIDE_FORCE = 20000;
    float yEuler;

    public GameObject nova;
    public GameObject orb;

    float orbDefaultSize;
    float orbDefaultHeight;

    LineRenderer line;

    override protected void Start()
    {
        rotationSpeed = 8;
        base.Start();

        orbDefaultSize = orb.transform.localScale.x;
        orbDefaultHeight = orb.transform.localPosition.y;
        
    }

    protected override void EndWarp()
    {
        base.EndWarp();

        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(transform, 0.05f, 0.05f, 5f);
    }

    protected override void OnBeat()
    {
        orb.transform.DOScale(orbDefaultSize + 10, 0.2f).From();
        orb.transform.DOLocalMoveY(orbDefaultHeight + 2, 0.2f).From();

        transform.DOScale(4.4f, 0.2f).From(); 

        if (target)
        {
            if ((target.transform.position - transform.position).magnitude >= 300)
            {
                rB.AddForce((target.transform.position - transform.position).normalized * SLIDE_FORCE);

                yEuler += 45;
                targetRotation = Quaternion.Euler(0, yEuler, 0);
            }
            else
            {
                ProCamera2DShake.Instance.Shake(
                    0.9f, //duration
                    new Vector3(100, 100), //strength
                    10, //vibrato
                    1, //randomness
                    -1, //initialAngle (-1 = random)
                    new Vector3(0, 0, 0), //rotation
                    0.1f); //smoothness

                GameObject clone = Instantiate(nova, transform.position, transform.rotation) as GameObject;
                size = 2;
                Explode(false);
            }
        }
    }

    protected override void FindTarget()
    {
        if (Object.FindObjectOfType<HiveMind>())
            target = Object.FindObjectOfType<HiveMind>().gameObject;
        else
            target = this.gameObject;
    }

    public override void Explode(bool reward)
    {
        Camera.main.GetComponent<ProCamera2D>().RemoveCameraTarget(transform, 0);
        base.Explode(reward);
    }
}
