using UnityEngine;
using System.Collections;

public class TestDrone : BaseEnemy 
{
    Vector3 dashVelocity;

    float initialRotateSpeed = 50;
    float spinTime = 0.7f;
    float dashSpeed = 400;

    float initialSpeed;
    //float speed;
    float timer;
    float targetDistance;
    float spinStartDistance;
    float dashTime;

    float rotateSpeed;

    enum State
    {
        SEEK, SPIN, DASH
    }

    State state = State.SEEK;

    override protected void Start()
    {
        value = 1;
        health = 100;
        speed = 100;

        initialSpeed = Random.Range(50, 150);
        spinStartDistance = Random.Range(100, 450);
        dashTime = Random.Range(0.5f, 3.0f);

        //targetPosition = new Vector3(1000, 0, 1000);

        base.Start();
    }

    override protected void Normal()
    {
        if (gameState == EnemyState.NORMAL)
        {


            targetDistance = Vector3.Magnitude(target.transform.position - transform.position);

            switch (state)
            {
                default:  /// Chase Player
                case State.SEEK:
                    speed = initialSpeed;

                    if (rotateSpeed > initialRotateSpeed)
                        rotateSpeed -= 500;
                    else
                        rotateSpeed = initialRotateSpeed;

                    if (targetDistance < spinStartDistance)
                    {
                        state = State.SPIN;
                    }

                    velocity = Vector3.Normalize(target.transform.position - transform.position) * speed;
                    break;

                case State.SPIN:  /// Spin to look cool
                    rotateSpeed += 1000 * Time.deltaTime;

                    timer += Time.deltaTime;
                    if (timer >= spinTime)
                    {
                        timer = 0;
                        dashVelocity = Vector3.Normalize(target.transform.position - transform.position) * dashSpeed;
                        state = State.DASH;
                    }

                    velocity = Vector3.zero;
                    break;

                case State.DASH:  /// Dash attack
                    timer += Time.deltaTime;

                    if (timer >= dashTime)
                    {
                        timer = 0;
                        state = State.SEEK;
                    }

                    velocity = dashVelocity;

                    break;
            }
        }

        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime * timeDilation);
        //base.Update();
    }


}
