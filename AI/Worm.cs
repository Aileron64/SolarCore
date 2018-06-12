using UnityEngine;
using System.Collections;

public class Worm : BaseEnemy
{
    public enum State
    {
        LEADER, FOLLOWER
    }
    public State state = State.LEADER;

    public GameObject leader;

    //float speed = 2000;
    float folowSpeed;
    float rotSpeed = 1.5f;

    float gap = 70;

    Vector3 directionOffset;

    float offSetSpeed;
    const float offSetValue = 10;

    int beatNum = 0;

    override protected void Start()
    {
        health = 3500;
        //stunImmune = true;

        speed = 100000;

        
        if (leader)
            state = State.FOLLOWER;

        base.Start();

    }

    override protected void Normal()
    {

        switch(state)
        {
            default:
            case State.LEADER:

                //rotation.y += Time.deltaTime * 50;

                Vector3 targetDir = target.transform.position - transform.position;
                float step = rotSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                //Debug.DrawRay(transform.position, newDir, Color.red);
                Quaternion tmpRotation = Quaternion.LookRotation(newDir);

                //rotation.y = tmpRotation.eulerAngles.y;

                directionOffset.y += offSetSpeed;
                FaceTarget(directionOffset, rotSpeed);

                velocity = transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;      


                break;


            case State.FOLLOWER:


                //FaceTarget(Vector3.zero, rotSpeed);



                if (leader)
                {
                    transform.rotation = Quaternion.LookRotation(
                        Vector3.RotateTowards(transform.forward,
                        Quaternion.Euler(Vector3.zero) * (leader.transform.position - transform.position),
                        30 * Time.deltaTime, 0.0F));

                    //folowSpeed = (Vector3.Magnitude(leader.transform.position - transform.position) - gap) * 5;
                    //velocity = Vector3.Normalize(leader.transform.position - transform.position) * folowSpeed;
                }
                else
                {
                    state = State.LEADER;
                }



                break;
        }

    }



    protected override void OnBeat()
    {
        //if (beatNum == 0)
        //{
        //    offSetSpeed = offSetValue * 0.5f;

        //}
        //else if (beatNum == 1)
        //{
        //    offSetSpeed *= -2;
        //}
        //else
        //{
        //    offSetSpeed *= -1;
        //}

        //beatNum++;
    }
}
