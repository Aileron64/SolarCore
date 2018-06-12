using UnityEngine;
using System.Collections;

public class TurtleShell : BaseAI 
{
    bool expand = false;
    bool unexpand = false;

    float delay = 2;
    float expandTimer;
    float unexpandTimer;


    void Start()
    {
        health = 500;
        rotate = false;
        impactDamage = 5;
        expAmount = 0;
        target = transform.parent.gameObject;
    }

    override protected void Normal()
    {
        if (expand)
        {
            expandTimer += Time.deltaTime;
            ChaseTarget(-2.5f);

            if (expandTimer >= delay)
                expand = false;
        }
        else if (unexpand)
        {
            unexpandTimer += Time.deltaTime;
            ChaseTarget(2.5f);

            if (unexpandTimer >= delay)
                unexpand = false;
        }
        else
            velocity = Vector3.zero;

        velocity.y = 0;
    }

    public void Expand()
    {
        expandTimer = 0;
        expand = true;
    }

    public void Unexpand()
    {
        unexpandTimer = 0;
        unexpand = true;
    }

    public void ExplodeNow()
    {
        Explode();
    }

    protected override void GivePoints()
    { }

    protected override void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
