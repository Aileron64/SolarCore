using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject box_prefab;
    public GameObject boxTest;
   
    GameObject[,] box = new GameObject[30, 30];

    int size = 30;

    int[,] boxArray = new int[30, 30];

    public bool testBoxes;
    public bool spawnBoxes;

    float xPos;
    float yPos;

    public void Setup(int[,] _boxArray)
    {
        boxArray = _boxArray;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (boxArray[x, y] > 0)
                {
                    //xPos = ((x + (size / -2)) * 100);
                    //yPos = ((y + (size / -2)) * 100);

                    xPos = 2500 - (x * 100);
                    yPos = 2500 - (y * 100);

                    box[x, y] = Instantiate(box_prefab, new Vector3(
                        xPos, 0, yPos), Quaternion.identity);

                    box[x, y].GetComponent<Box>().xIndex = x;
                    box[x, y].GetComponent<Box>().xIndex = y;

                    if (boxArray[x, y] == 1)
                    {
                        box[x, y].SetActive(false);
                    }

                    if (testBoxes)
                    {
                        GameObject clone = Instantiate(boxTest, new Vector3(
                            xPos, -10, yPos),
                            Quaternion.Euler(90, 90, 0)) as GameObject;

                        clone.GetComponent<TextMesh>().text = x + "," + y;
                    }

                    if (spawnBoxes)
                    {
                        ActivateBox(x, y);
                    }
                }
            }
        }
    }

    static BoxManager instance;
    public static BoxManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoxManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<BoxManager>();
                }
            }
            return instance;
        }
    }

    public int GetArrayValue(int x, int y)
    {
        if (x >= 0 && x < 30 && y >= 0 && y < 30)
            return boxArray[x, y];
        else
            return 0;
    }

    public void ActivateBox(int x, int y)
    {
        if (x >= 0 && x < 30 && y >= 0 && y < 30)
        {
            if(boxArray[x, y] != 0)
            {
                if(box[x, y])
                {
                    box[x, y].SetActive(true);
                    boxArray[x, y] = 2;
                }
                else
                    Debug.Log("Box Broken at (" + x + ", " + y + ")");

            }
        }

    }

    public void OnBoxEnd(int x, int y)
    {
        boxArray[x, y] = 1;
    }

}
