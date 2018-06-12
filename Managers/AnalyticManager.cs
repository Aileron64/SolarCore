using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Steamworks;

public class AnalyticManager : MonoBehaviour
{
    static bool flag = true;
    static string id;

    static string GetName()
    {
        if (SteamManager.Initialized)
            return SteamFriends.GetPersonaName();
        else
            return "Unknown";
    }

    public static void OnStart()
    {
        if(flag)
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
                Analytics.CustomEvent("Game_Launched - " + GetName() + " - Windows");
            else if (Application.platform == RuntimePlatform.OSXPlayer)
                Analytics.CustomEvent("Game_Launched - " + GetName() + " - Mac");
            else if (Application.platform == RuntimePlatform.LinuxPlayer)
                Analytics.CustomEvent("Game_Launched - " + GetName() + " - Linux");
            else
                Analytics.CustomEvent("Game_Launched - " + GetName());

            //Analytics.CustomEvent("Game Launched", new Dictionary<string, object>
            //{
            //    { "ID", GetName()},
            //});

            flag = false;
        }
    }

    public static void OnLevelStart()
    {
        Analytics.CustomEvent(SceneManager.GetActiveScene().name 
            + " - " + GetName()
            + " - " + PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName);

        Analytics.CustomEvent(GetName(), new Dictionary<string, object>
        {
            { SceneManager.GetActiveScene().name,
             PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName }
        });

        //Analytics.CustomEvent("Level Start", new Dictionary<string, object>
        //{
        //    { "ID", GetName() },
        //    { "Lv", SceneManager.GetActiveScene().name },
        //    { "Ship", PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName },
        //});
    }

    public static void OnGameOver()
    {
        //Analytics.CustomEvent("Game_Over - "
        //    + SceneManager.GetActiveScene().name
        //    + " - " + GetName()
        //    + " - " + PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName
        //    + " - Beat#_" + BaseLevel.Instance.GetBeatNum());

        Analytics.CustomEvent(GetName(), new Dictionary<string, object>
        {
            { "Game_Over",
             SceneManager.GetActiveScene().name + " - "
             + PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName }
        });

        //Analytics.CustomEvent("Game Over", new Dictionary<string, object>
        //{
        //    { "ID", GetName() },
        //    { "Lv", SceneManager.GetActiveScene().name },
        //    { "Ship", PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName },
        //    { "Beat#", BaseLevel.Instance.GetBeatNum() },
        //});
    }

    public static void OnLevelComplete()
    {
        Analytics.CustomEvent(GetName(), new Dictionary<string, object>
        {
            { "Level_Complete",
             SceneManager.GetActiveScene().name + " - "
             + PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName }
        });

        //Analytics.CustomEvent("Level_Complete - "
        //    + SceneManager.GetActiveScene().name
        //    + " - " + GetName()
        //    + " - " + PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName);

        //Analytics.CustomEvent("Level Complete", new Dictionary<string, object>
        //{
        //    { "ID", GetName() },
        //    { "Lv", SceneManager.GetActiveScene().name },
        //    { "Ship", PlayerManager.Instance.GetPlayer(0).GetComponent<Player>().shipName },
        //});
    }

}
