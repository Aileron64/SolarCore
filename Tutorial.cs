using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text text;

    int beatNum = 0;
    bool[] flag = new bool[4];

    Player player;


    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
        player = PlayerManager.Instance.GetPlayer(0).GetComponent<Player>();
    }


    void Update()
    {
        if (!flag[0])
        {
            if (Input.GetAxis("Vertical P1") != 0
                || Input.GetAxis("Horizontal P1") != 0)
            {
                ClearText();
                flag[0] = true;
            }
        }


        if (!flag[1])
        {
            if (Input.GetAxis("Right Trigger P1") != 0
                || Input.GetAxis("Left Trigger P1") != 0)
            {
                ClearText();
                flag[1] = true;

                //TextManager.Instance.DebugText("TEST");
            }
        }

        if (!flag[2])
        {
            if(ScoreManager.Instance.coinNum >= 4)
            {
                ClearText();
                flag[2] = true;
            }
        }

    }



    void OnBeat()
    {
        beatNum++;

        switch(beatNum)
        {
            default:
                break;

            case 7:
                if(!flag[0])
                    SetText("Move with WASD");
                break;


            case 35:
                if (!flag[1])
                    SetText("Use abilities with right click and space");
                break;

            case 95:
                if (!flag[2])
                    SetText("Yellow orbs restore health and grant points");
                break;

        }
    }

    void SetText(string _text)
    {
        text.text = _text;
    }

    void ClearText()
    {
        text.text = "";
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }

}
