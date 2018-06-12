using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoxBoy : BaseEnemy
{
    public GameObject box;
    public GameObject[] part;

    int boxMax = 6;
    int boxMin = 3;

    int boxCount;

    //Vector3 TargetPos;
    
    enum State
    {
        BUILD, MOVE
    }
    State state = State.BUILD;

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public Direction direction;

    BoxManager boxMan;

    public int xIndex;
    public int yIndex;

    protected override void Start() 
    {
        boxMan = BoxManager.Instance;
        speed = 400;
        base.Start();
	}

    protected override void EndWarp()
    {
        base.EndWarp();


        //xIndex = (int)((transform.position.x + 550) / 100);
        //yIndex = (int)((transform.position.z + 550) / 100);

        float x = 2550 - transform.position.x;
        float y = 2550 - transform.position.z;

        xIndex = (int)((x - (x % 100)) / 100);
        yIndex = (int)((y - (y % 100)) / 100);

        //TargetPos = new Vector3(xIndex * 100 - 500, 0, yIndex * 100 - 500);

        float rand = Random.Range(0, 3);

        if (rand <= 1)
        {
            direction = Direction.UP;
        }
        else if (rand <= 2)
        {
            direction = Direction.DOWN;
        }
        else if (rand <= 3)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }

        boxCount = Random.Range(boxMin, boxMax);
    }


    protected override void OnBeat()
    {
        switch(state)
        {
            default:
                break;

            case State.BUILD:

                BuildBox();    
                

                boxCount--;

                

                switch (direction)
                {
                    default:
                    case Direction.UP:

                        if(boxMan.GetArrayValue(xIndex + 1, yIndex) == 0)                   
                            ChangeDirection();
                        else
                            state = State.MOVE;

                        break;

                    case Direction.DOWN:
                        if (boxMan.GetArrayValue(xIndex - 1, yIndex) == 0)
                            ChangeDirection();
                        else
                            state = State.MOVE;

                        break;

                    case Direction.RIGHT:
                        if (boxMan.GetArrayValue(xIndex, yIndex + 1) == 0)
                            ChangeDirection();
                        else
                            state = State.MOVE;

                        break;

                    case Direction.LEFT:
                        if (boxMan.GetArrayValue(xIndex, yIndex - 1) == 0)
                            ChangeDirection();
                        else
                            state = State.MOVE;

                        break;
                }

                if (boxCount <= 0) //if(xIndex <= 1 || xIndex >= 28 || yIndex <= 1 || yIndex >= 28)
                {
                    ChangeDirection();
                }                
                break;

            case State.MOVE:

                switch(direction)
                {
                    default:
                    case Direction.UP:
                        MoveToBox(xIndex + 1, yIndex);             
                        break;

                    case Direction.DOWN:
                        MoveToBox(xIndex - 1, yIndex);
                        break;

                    case Direction.RIGHT:
                        MoveToBox(xIndex, yIndex + 1);
                        break;

                    case Direction.LEFT:
                        MoveToBox(xIndex, yIndex - 1);
                        break;
                }

                //TargetPos += dir;
                //if (boxMan.GetArrayValue(xIndex, yIndex) == 1)
                    state = State.BUILD;

                break;

        }


    }

    void MoveToBox(int x, int y)
    {
        xIndex = x;
        yIndex = y;

        //DebugText.Instance.SetText(xIndex + ", " + yIndex);

        transform.DOMove(new Vector3(2500 - x * 100, restHeight, 2500 - y * 100), 0.4f);
    }

    void BuildBox()
    {
        boxMan.ActivateBox(xIndex, yIndex);
    }

    void ChangeDirection()
    {
        //float rand = Random.Range(1000 - BOX_RANGE, 1000 + BOX_RANGE);

        switch (direction)
        {
            default:
            case Direction.UP:
            case Direction.DOWN:

                float rightValue = 0;
                float leftValue = 0;

                if (boxMan.GetArrayValue(xIndex, yIndex + 1) == 0)
                {
                    direction = Direction.LEFT;
                }
                else if (boxMan.GetArrayValue(xIndex, yIndex - 1) == 0)
                {
                    direction = Direction.RIGHT;
                }
                else
                {
                    for (int i = 0; i < 30; i++)
                    {
                        if (boxMan.GetArrayValue(xIndex, i) == 1)
                        {
                            if (i > yIndex)
                                leftValue++;
                            else if (i < yIndex)
                                rightValue++;
                        }
                    }

                    leftValue += Random.Range(-5.5f, 5.5f);

                    if (leftValue <= rightValue)
                    {
                        direction = Direction.LEFT;
                    }
                    else
                    {
                        direction = Direction.RIGHT;
                    }
                    
                }

                break;

            case Direction.LEFT:
            case Direction.RIGHT:

                float upValue = 0;
                float downValue = 0;

                if (boxMan.GetArrayValue(xIndex + 1, yIndex) == 0)
                {
                    direction = Direction.DOWN;
                }
                else if (boxMan.GetArrayValue(xIndex - 1, yIndex) == 0)
                {
                    direction = Direction.UP;
                }
                else
                {
                    for (int i = 0; i < 30; i++)
                    {
                        if (boxMan.GetArrayValue(xIndex, i) == 1)
                        {
                            if (i < yIndex)
                                upValue++;
                            else if (i > yIndex)
                                downValue++;
                        }
                    }

                    downValue += Random.Range(-5.5f, 5.5f);

                    if (downValue <= upValue)
                    {
                        direction = Direction.DOWN;
                    }
                    else
                    {
                        direction = Direction.UP;
                    }
                }

                break;
        }

        boxCount = Random.Range(boxMin, boxMax);

    }


    protected override void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;

        for (int i = 0; i < part.Length; i++)
        {
            part[i].GetComponent<Renderer>().material = mat;
        }
    }
}
