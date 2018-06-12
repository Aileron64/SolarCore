using UnityEngine;
using System.Collections;

public class BoxCat : BaseEnemy
{


    enum State
    {
        WAIT, UP, DOWN, LEFT, RIGHT
    }
    State state = State.WAIT;


    override protected void Start()
    {
        health = 7000;
        impactDamage = 30;
        value = 12;

        speed = 1000;

        base.Start();
    }


    protected override void Normal()
    {
        if(state != State.WAIT)
        {
            float distance = (transform.position - target.transform.position).magnitude;

            if(distance <= 250)
            {
                velocity = Vector3.zero;
                state = State.WAIT;
            }
        }
    }


    protected override void OnBeat()
    {
        if(state == State.WAIT)
        {

            switch(Random.Range(0, 4))
            {
                default:
                case 0:
                    CheckUp();
                    CheckDown();
                    CheckLeft();
                    CheckRight();
                    break;

                case 1:          
                    CheckDown();
                    CheckLeft();
                    CheckRight();
                    CheckUp();
                    break;

                case 2:
                    CheckLeft();
                    CheckRight();
                    CheckUp();
                    CheckDown();
                    break;

                case 3:
                    CheckRight();
                    CheckUp();
                    CheckDown();
                    CheckLeft();
                    break;
            }


        }

        //Debug.Log(Random.Range(0, 4));
    }

    bool CheckUp()
    {
        RaycastHit[] upRaycast = Physics.RaycastAll(transform.position, new Vector3(1, 0, 0), 3000);

        foreach (RaycastHit hit in upRaycast)
        {
            if (hit.collider.tag == "Block")
            {
                if ((transform.position - hit.point).magnitude > 200)
                {
                    velocity = new Vector3(speed, 0, 0);
                    target = hit.collider.gameObject;
                    state = State.UP;

                    return true;
                }
                else
                    return false;
            }
        }

        return false;
    }


    bool CheckDown()
    {
        RaycastHit[] downRaycast = Physics.RaycastAll(transform.position, new Vector3(-1, 0, 0), 3000);

        foreach (RaycastHit hit in downRaycast)
        {
            if (hit.collider.tag == "Block")
            {
                if ((transform.position - hit.point).magnitude > 200)
                {
                    velocity = new Vector3(-speed, 0, 0);
                    target = hit.collider.gameObject;
                    state = State.DOWN;

                    return true;
                }
                else
                    return false;
            }
        }

        return false;
    }

    bool CheckLeft()
    {
        RaycastHit[] leftRaycast = Physics.RaycastAll(transform.position, new Vector3(0, 0, 1), 3000);

        foreach (RaycastHit hit in leftRaycast)
        {
            if (hit.collider.tag == "Block")
            {
                if ((transform.position - hit.point).magnitude > 200)
                {
                    velocity = new Vector3(0, 0, speed);
                    target = hit.collider.gameObject;
                    state = State.LEFT;

                    return true;
                }
                else
                    return false;
            }
        }

        return false;
    }
    
    bool CheckRight()
    {
        RaycastHit[] rightRaycast = Physics.RaycastAll(transform.position, new Vector3(0, 0, -1), 3000);

        foreach (RaycastHit hit in rightRaycast)
        {
            if (hit.collider.tag == "Block")
            {
                if ((transform.position - hit.point).magnitude > 200)
                {
                    velocity = new Vector3(0, 0, -speed);
                    target = hit.collider.gameObject;
                    state = State.RIGHT;

                    return true;
                }
                else
                    return false;
            }
        }

        return false;
    }
    
}
