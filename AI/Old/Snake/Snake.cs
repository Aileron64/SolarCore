using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : BaseEnemy
{
    enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    Direction direction = Direction.LEFT;
    float directionTimer;
    float directionTime = 1;

    public GameObject pellet;
    List<GameObject> pellets = new List<GameObject>();

    const int xLength = 10;
    const int yLength = 10;

    Vector3[,] waypoint = new Vector3[xLength, yLength];

    public GameObject SnakePart;
    List<GameObject> parts = new List<GameObject>();


    public void RemovePart(GameObject _part)
    {
        parts.Remove(_part);
    }

    public GameObject FindLeader(GameObject _part)
    {
        int tmp = parts.IndexOf(_part);

        Debug.Log("" + tmp);

        if (tmp < parts.Count)
        {
            if (!parts[tmp + 1])
                Debug.Log("WUT O SPEGETITO");

            return parts[tmp + 1];
        }
        else
            return this.gameObject;
    }

    override protected void Start()
    {
        health = 40000;
        impactDamage = 50;
        value = 50;
        speed = 300;

        stunImmune = true;
        timeImmune = true;

        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                waypoint[i, j].Set(((i - (xLength / 2)) * 600) + 300, restHeight, ((j - (yLength / 2)) * 600) + 300);
                pellets.Add(Instantiate(pellet, waypoint[i, j], pellet.transform.localRotation) as GameObject);
            }
        }

        base.Start();

        //for (int i = 1; i <= 3; i++)
        //{
        //    AddPart(transform.position);// + new Vector3(0, 0, -300 * i));
        //}

        ChangeDirection();
    }



    override protected void Normal()
    {
        directionTimer += Time.deltaTime;

        if(directionTimer >= directionTime)
        {
            directionTimer = 0;
            ChangeDirection();          
        }

        if(target)
        {
            if (direction == Direction.UP || direction == Direction.DOWN)
            {
                if (direction == Direction.UP)
                {
                    velocity.Set(speed, 0, 0);
                }
                else
                {
                    velocity.Set(-speed, 0, 0);
                }

                if (Mathf.Abs(transform.position.x - target.transform.position.x) <= 10)
                    ChangeDirection();
            }

            if (direction == Direction.LEFT || direction == Direction.RIGHT)
            {
                if (direction == Direction.LEFT)
                {
                    velocity.Set(0, 0, speed);
                }
                else
                {
                    velocity.Set(0, 0, -speed);
                }

                if (Mathf.Abs(transform.position.z - target.transform.position.z) <= 10)
                    ChangeDirection();
            }
        }

    }

    void ChangeDirection()
    {
        if (target)
        {
            Vector3 distance = transform.position - target.transform.position;

            //Debug.Log("CHANGEING DIRECTION");

            AddWaypoint(transform.position);

            //closer on x or z axis
            if (Mathf.Abs(distance.x) >= Mathf.Abs(distance.z))
            {
                if(distance.x > 50)
                    direction = Direction.DOWN;
                else if(distance.x < -50)
                    direction = Direction.UP;
                else // Must be colliding
                    EatPellet();
            }
            else
            {
                if (distance.z > 50)
                    direction = Direction.RIGHT;
                else if (distance.z < -50)
                    direction = Direction.LEFT;
                else // Must be colliding
                    EatPellet();
            }
        }
    }

    private void EatPellet()
    {
        pellets.Remove(target);
        Destroy(target);

        FindTarget();
        AddPart(transform.position - (velocity));

    }

    void AddPart(Vector3 pos)
    {
        GameObject clone = Instantiate(SnakePart, pos, Quaternion.identity) as GameObject;
        clone.GetComponent<SnakePart>().leader = this.gameObject;

        if (parts.Count > 0)
            parts[parts.Count - 1].GetComponent<SnakePart>().leader = clone;

        parts.Add(clone);
    }

    void AddWaypoint(Vector3 waypoint)
    {
        foreach (GameObject p in parts)
        {
            p.GetComponent<SnakePart>().AddWayPoint(waypoint);
        }
    }

    protected override void FindTarget()
    {
        float shortestDistance = 10000;

        //Debug.Log("FINDING TARGET");

        foreach (GameObject pel in pellets)
        {
            float distance = (pel.transform.position - transform.position).magnitude;

            if(distance < shortestDistance)
            {
                target = pel;
                shortestDistance = distance;
            }
        }

        if (!target)
            Debug.Log("Can't find target");
    }
}
