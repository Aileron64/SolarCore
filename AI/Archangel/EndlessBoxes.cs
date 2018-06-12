using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBoxes : MonoBehaviour
{
    const int NUM_OF_BOXES = 15;
    const int BOX_SIZE = 240;

    GameObject[] blocks = new GameObject[NUM_OF_BOXES];

    int frontBlock;
    int endBlock;

    Transform cam;

    //DebugText dText;

    float upperCamLine;
    float lowerCamLine;

    void Awake()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = transform.Find("Block (" + i + ")").gameObject;
        }

        frontBlock = 0;
        endBlock = blocks.Length - 1;

        //Cam start pos = -120, 1440, 1000
        cam = Camera.main.transform;

        //dText = Object.FindObjectOfType<DebugText>();

        upperCamLine = cam.position.x + BOX_SIZE;
        lowerCamLine = cam.position.x - BOX_SIZE;
    }

    void LateUpdate()
    {
        if(cam.position.x > upperCamLine)
        {
            MoveUp();
            lowerCamLine = upperCamLine;
            upperCamLine += BOX_SIZE;
        }

        if (cam.position.x < lowerCamLine)
        {
            MoveDown();
            upperCamLine = lowerCamLine;
            lowerCamLine -= BOX_SIZE;
        }

        //dText.SetText("Cam.x = " + cam.position.x
        //            + "\nUpper = " + upperCamLine
        //            + "\nLower = " + lowerCamLine);
    }


    void MoveUp()
    {
        blocks[frontBlock].transform.position = blocks[endBlock].transform.position + new Vector3(240, 0, 0);

        endBlock = frontBlock;

        if (frontBlock < blocks.Length - 1)
            frontBlock += 1;
        else
            frontBlock = 0;
    }

    void MoveDown()
    {
        blocks[endBlock].transform.position = blocks[frontBlock].transform.position + new Vector3(-240, 0, 0);

        frontBlock = endBlock;

        if (endBlock > 0)
            endBlock -= 1;
        else
            endBlock = blocks.Length - 1;
    }

}
