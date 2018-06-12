using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievements : MonoBehaviour
{
    static Achievements instance;
    public static Achievements Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Achievements>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<Achievements>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (!SteamManager.Initialized)
        {
            Debug.Log("NO STEAM");
        }
    }

    public void Achievment(string api_name)
    {
        if (SteamManager.Initialized && !GodMode.active)
        {
            SteamUserStats.SetAchievement(api_name);

            Debug.Log("Achievement - " + api_name);
        }
    }

}
