using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridState
{
    OFF, RANDOM, TUNNEL, TUNNEL_RANDOM, TEST
}

public class CubeGrid : MonoBehaviour
{
    public GameObject box_prefab;
    public GameObject sticky_prefab;
    public GridState gridState;

    GameObject clone;

    public int randomAmount;
    public int randomStickyAmount;

    public bool clockWise;
    bool tunnelflag;
    int tunnelCount;
    Vector2[] tunnelPath = {
            new Vector2(2, 4),
            new Vector2(2, 3),
            new Vector2(2, 2),
            new Vector2(3, 2),
            new Vector2(4, 2),
            new Vector2(5, 2),
            new Vector2(6, 2),
            new Vector2(6, 3),
            new Vector2(6, 4),
            new Vector2(6, 5),
            new Vector2(6, 6),
            new Vector2(5, 6),
            new Vector2(4, 6),
            new Vector2(3, 6),
            new Vector2(2, 6),
            new Vector2(2, 5),};

    int tunnelRandX = 3;
    int tunnelRandY = 4;
   
    public enum RandDirection
    {
        UP, DOWN, RIGHT, LEFT
    }
    RandDirection randDir = RandDirection.DOWN;


    const int GRID_SIZE = 9;
    const int CUBE_DISTANCE = 450;
    const int START_DISTANCE = -3550;

    void Awake()
    {
        //tunnelPath.x = 1;
        //tunnelPath.y = 3;

    }



    void OnBeat()
    {
        switch(gridState)
        {
            default:
            case GridState.OFF:
                break;

            case GridState.RANDOM:

                for (int i = 0; i < randomAmount; i++)
                {
                    SpawnCube(Random.Range(0, 9), Random.Range(0, 9));
                }

                for (int i = 0; i < randomStickyAmount; i++)
                {
                    SpawnStickyCube(Random.Range(0, 9), Random.Range(0, 9));
                }
                break;

            case GridState.TUNNEL:
                Tunnel();
                break;


            case GridState.TUNNEL_RANDOM:
                TunnelRandom();
                break;
            
            case GridState.TEST:

                for (int x = 0; x < GRID_SIZE; x++)
                {
                    for (int y = 0; y < GRID_SIZE; y++)
                    {
                        SpawnCube(x, y);                    
                    }
                }
                break;
        }
    }

    void Tunnel()
    {

        if (tunnelCount >= tunnelPath.Length)
            tunnelCount = 0;

        if (tunnelCount < 0)
            tunnelCount = tunnelPath.Length - 1;

        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                float pathX = tunnelPath[tunnelCount].x;
                float pathY = tunnelPath[tunnelCount].y;

                if ((x != pathX || y != pathY)
                    && (x != pathX || y != pathY + 1)
                    && (x != pathX || y != pathY - 1)
                    && (x != pathX + 1 || y != pathY)
                    && (x != pathX - 1 || y != pathY)
                    && (x != pathX + 1 || y != pathY + 1)
                    && (x != pathX - 1 || y != pathY - 1)
                    && (x != pathX - 1 || y != pathY + 1)
                    && (x != pathX + 1 || y != pathY - 1))
                    SpawnCube(x, y);
            }
        }

        if (tunnelflag)
        {
            if (clockWise)
                tunnelCount--;
            else
                tunnelCount++;
        }


        tunnelflag = !tunnelflag;

    }

    void TunnelRandom()
    {
        if (tunnelflag)
        {
            switch (randDir)
            {
                default:
                case RandDirection.UP:

                    tunnelRandX++;
                    if (tunnelRandX >= 6)
                    {
                        if (tunnelRandY >= 6 || Random.Range(0, 3) == 0)
                            randDir = RandDirection.RIGHT;
                        else if (tunnelRandY <= 2)
                            randDir = RandDirection.LEFT;
                        else if (Random.Range(0, 2) == 0)
                            randDir = RandDirection.LEFT;
                        else
                            randDir = RandDirection.RIGHT;
                    }
                    break;

                case RandDirection.DOWN:

                    tunnelRandX--;
                    if (tunnelRandX <= 2 || Random.Range(0, 3) == 0)
                    {
                        if (tunnelRandY >= 6)
                            randDir = RandDirection.RIGHT;
                        else if (tunnelRandY <= 2)
                            randDir = RandDirection.LEFT;
                        else if (Random.Range(0, 2) == 0)
                            randDir = RandDirection.LEFT;
                        else
                            randDir = RandDirection.RIGHT;
                    }
                    break;

                case RandDirection.RIGHT:

                    tunnelRandY--;
                    if (tunnelRandY <= 2 || Random.Range(0, 3) == 0)
                    {
                        if (tunnelRandX >= 6)
                            randDir = RandDirection.DOWN;
                        else if (tunnelRandX <= 2)
                            randDir = RandDirection.UP;
                        else if (Random.Range(0, 2) == 0)
                            randDir = RandDirection.UP;
                        else
                            randDir = RandDirection.DOWN;
                    }
                    break;

                case RandDirection.LEFT:

                    tunnelRandY++;
                    if (tunnelRandY >= 6 || Random.Range(0, 3) == 0)
                    {
                        if (tunnelRandX >= 6)
                            randDir = RandDirection.DOWN;
                        else if (tunnelRandX <= 2)
                            randDir = RandDirection.UP;
                        else if (Random.Range(0, 2) == 0)
                            randDir = RandDirection.UP;
                        else
                            randDir = RandDirection.DOWN;
                    }
                    break;
            }
        }

        tunnelflag = !tunnelflag;

        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                float pathX = tunnelRandX;
                float pathY = tunnelRandY;

                if ((x != pathX || y != pathY)
                    && (x != pathX || y != pathY + 1)
                    && (x != pathX || y != pathY - 1)
                    && (x != pathX + 1 || y != pathY)
                    && (x != pathX - 1 || y != pathY)
                    && (x != pathX + 1 || y != pathY + 1)
                    && (x != pathX - 1 || y != pathY - 1)
                    && (x != pathX - 1 || y != pathY + 1)
                    && (x != pathX + 1 || y != pathY - 1))
                    SpawnCube(x, y);
            }
        }

    }

    void SpawnCube(int x, int y)
    {
        clone = Instantiate(box_prefab, 
            new Vector3(
                CUBE_DISTANCE * x + 1000 - CUBE_DISTANCE * (GRID_SIZE - 1) / 2, 
                START_DISTANCE,
                CUBE_DISTANCE * y + 1000 - CUBE_DISTANCE * (GRID_SIZE - 1) / 2), 
                Quaternion.identity) as GameObject;
    }

    void SpawnStickyCube(int x, int y)
    {
        if (x != 4 || y != 4)
        {
            clone = Instantiate(sticky_prefab,
                new Vector3(
                    CUBE_DISTANCE * x + 1000 - CUBE_DISTANCE * (GRID_SIZE - 1) / 2,
                    START_DISTANCE,
                    CUBE_DISTANCE * y + 1000 - CUBE_DISTANCE * (GRID_SIZE - 1) / 2),
                    Quaternion.identity) as GameObject;
        }
    }

    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }
}
