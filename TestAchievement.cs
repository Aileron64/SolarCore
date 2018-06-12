using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;

public class TestAchievement : MonoBehaviour
{
    bool flag = true;
    Text text;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { OnClick(); });

        if (!SteamManager.Initialized)
        {
            Debug.Log("NO STEAM");
            text.text = "No Steam";
            GetComponent<Button>().enabled = false;
            //Destroy(this.gameObject);
        }
        else
        {
            

            text = transform.GetChild(0).GetComponent<Text>();

            //SteamUserStats.GetAchievement("TEST", out flag);
            //Debug.Log("TEST: " + flag);

            //SteamUserStats.GetAchievement("CUBE_GOD", out flag);
            //Debug.Log("CUBE_GOD: " + flag);

            //SteamUserStats.GetAchievement("ARCHANGEL", out flag);
            //Debug.Log("ARCHANGEL: " + flag);

            SteamUserStats.GetAchievement("TEST", out flag);

            if(flag)
            {
                text.text = "Achievement--";
            }
            else
            {
                text.text = "Achievement++";
            }
        }
    }


    void OnClick()
    {
        if(!flag)
        {
            SteamUserStats.SetAchievement("TEST");
            Debug.Log("Achievement Added");
            text.text = "Achievement--";
        }
        else
        {
            SteamUserStats.ClearAchievement("TEST");
            Debug.Log("Achievement Removed");
            text.text = "Achievement++";
        }

        flag = !flag;
    }
}
