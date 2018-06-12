using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakePart : BaseEnemy
{
    public GameObject leader;

    int waypointNum = 0;
    List<Vector3> waypoints = new List<Vector3>();

    override protected void Start()
    {
        health = 100;
        impactDamage = 50;
        value = 50;
        speed = 300;

        stunImmune = true;
        timeImmune = true;

        base.Start();
    }

    public void AddWayPoint(Vector3 _waypoint)
    {
        waypoints.Add(_waypoint);
    }



    override protected void Normal()
    {
        if(leader)
        {
            float distance = Vector3.Magnitude(transform.position - leader.transform.position);
            //float targetDistance = Vector3.Magnitude(targetLoc - transform.position);

            if (distance < 250)
                speed -= 40;
            else if (distance > 300)
                speed += 20;
            else
                speed = 300;

            if (waypoints.Count > waypointNum)
            {
                velocity = (waypoints[waypointNum] - transform.position).normalized * speed;

                if ((waypoints[waypointNum] - transform.position).magnitude <= 10)
                {
                    //waypoints.RemoveAt(0);
                    waypointNum++;
                }
            }
            else
            {
                velocity = (leader.transform.position - transform.position).normalized * speed;
            }
        }
        else
        {
            leader = Object.FindObjectOfType<Snake>().FindLeader(this.gameObject);
        }


    }


    public override void Explode()
    {
        Object.FindObjectOfType<Snake>().RemovePart(this.gameObject);

        base.Explode();
    }

}