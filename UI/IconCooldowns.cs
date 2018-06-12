using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconCooldowns : MonoBehaviour
{
    Image[] skill = new Image[4];
    Image[] backIcon = new Image[4];

    Text[] buttonText = new Text[4];
    Text[] chargeNum = new Text[4];

    float[] iconFill = new float[4];
    int[] charges = new int[4];

    Player player;
    bool keyBoardText = false;
    public int playerNum;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            skill[i] = transform.Find("" + i).gameObject.transform.Find("Icon").GetComponent<Image>();
            backIcon[i] = transform.Find("" + i).gameObject.transform.Find("Back Icon").GetComponent<Image>();

            buttonText[i] = transform.Find("" + i).gameObject.transform.Find("Text").GetComponent<Text>();
            chargeNum[i] = transform.Find("" + i).gameObject.transform.Find("Charges").GetComponent<Text>();
        }

        SetPlayer(playerNum - 1);
    }

    public void SetPlayer(int _playerNum)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[_playerNum].GetComponent<Player>();

        //Change Icons
        for (int i = 0; i < 4; i++)
        {
            string iconFolder = "Icons/Skill_Icons/";

            //Debug.Log(player.shipName);

            if (player.shipName != "Bot")
            {
                skill[i].sprite = Resources.Load<Sprite>(iconFolder + player.shipName + "_" + i);
                backIcon[i].sprite = skill[i].sprite;
            }
        }     

        if(player.GetComponent<Player>().inputType == Player.InputType.MOUSE)
            ChangeInputText(true);     
    }

    void ChangeInputText(bool _keyBoard)
    {
        keyBoardText = _keyBoard;

        if(_keyBoard)
        {
            buttonText[0].text = "RMB";
            buttonText[1].text = "Space";
            buttonText[2].text = "Q";
            buttonText[3].text = "E";          
        }
        else
        {
            buttonText[0].text = "RT";
            buttonText[1].text = "LT";
            buttonText[2].text = "LB";
            buttonText[3].text = "RB";     
        }

    }

    void Update()
    {
        if(player)
        {
            for (int i = 0; i < 2; i++)
            {
                iconFill[i] = player.GetCooldown(i);

                if (iconFill[i] > 0)
                    skill[i].fillAmount = iconFill[i];
                else
                    skill[i].fillAmount = 1;

                charges[i] = player.GetCharges(i);

                if (player.GetMaxCharges(i) > 1)
                    chargeNum[i].text = string.Format("{0}", charges[i]);
                else
                    chargeNum[i].text = "";
            }

            if (player.inputType == Player.InputType.MOUSE && !keyBoardText)           
                ChangeInputText(true);          
            else if(player.inputType == Player.InputType.JOYSTICK && keyBoardText)
                ChangeInputText(false);        
        }
    }
}