using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamTest : MonoBehaviour
{
    private enum Achievement : int
    {
        ACH_WIN_ONE_GAME,
        ACH_WIN_100_GAMES,
        ACH_HEAVY_FIRE,
        ACH_TRAVEL_FAR_ACCUM,
        ACH_TRAVEL_FAR_SINGLE,
    };

    private Achievement_t[] m_Achievements = new Achievement_t[] {
        new Achievement_t(Achievement.ACH_WIN_ONE_GAME, "TEST", ""),
        new Achievement_t(Achievement.ACH_WIN_100_GAMES, "TEST", ""),
        new Achievement_t(Achievement.ACH_TRAVEL_FAR_ACCUM, "TEST", ""),
        new Achievement_t(Achievement.ACH_TRAVEL_FAR_SINGLE, "TEST", "")
    };

    // Our GameID
    private CGameID m_GameID;

    // Did we get the stats from Steam?
    private bool m_bRequestedStats;
    private bool m_bStatsValid;

    // Should we store stats this frame?
    private bool m_bStoreStats;

    // Current Stat details
    private float m_flGameFeetTraveled;
    private float m_ulTickCountGameStart;
    private double m_flGameDurationSeconds;

    // Persisted Stat details
    private int m_nTotalGamesPlayed;
    private int m_nTotalNumWins;
    private int m_nTotalNumLosses;
    private float m_flTotalFeetTraveled;
    private float m_flMaxFeetTraveled;
    private float m_flAverageSpeed;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

    void OnEnable()
    {
        if (!SteamManager.Initialized)
        {
            Debug.Log("NO STEAM");
            return;
        }


        // Cache the GameID for use in the Callbacks
        m_GameID = new CGameID(SteamUtils.GetAppID());

        m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        // These need to be reset to get the stats upon an Assembly reload in the Editor.
        m_bRequestedStats = false;
        m_bStatsValid = false;
    }

    private void Update()
    {
        if (!SteamManager.Initialized)
            return;

        if (!m_bRequestedStats)
        {
            // Is Steam Loaded? if no, can't get stats, done
            if (!SteamManager.Initialized)
            {
                m_bRequestedStats = true;
                return;
            }

            // If yes, request our stats
            bool bSuccess = SteamUserStats.RequestCurrentStats();

            // This function should only return false if we weren't logged in, and we already checked that.
            // But handle it being false again anyway, just ask again later.
            m_bRequestedStats = bSuccess;
        }

        if (!m_bStatsValid)
            return;

        // Get info from sources

        // Evaluate achievements
        foreach (Achievement_t achievement in m_Achievements)
        {
            if (achievement.m_bAchieved)
                continue;

            switch (achievement.m_eAchievementID)
            {
                case Achievement.ACH_WIN_ONE_GAME:
                    if (m_nTotalNumWins != 0)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_WIN_100_GAMES:
                    if (m_nTotalNumWins >= 100)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_TRAVEL_FAR_ACCUM:
                    if (m_flTotalFeetTraveled >= 5280)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
                case Achievement.ACH_TRAVEL_FAR_SINGLE:
                    if (m_flGameFeetTraveled >= 500)
                    {
                        UnlockAchievement(achievement);
                    }
                    break;
            }
        }

        //Store stats in the Steam database if necessary
        if (m_bStoreStats)
        {
            // already set any achievements in UnlockAchievement

            // set stats
            SteamUserStats.SetStat("NumGames", m_nTotalGamesPlayed);
            SteamUserStats.SetStat("NumWins", m_nTotalNumWins);
            SteamUserStats.SetStat("NumLosses", m_nTotalNumLosses);
            SteamUserStats.SetStat("FeetTraveled", m_flTotalFeetTraveled);
            SteamUserStats.SetStat("MaxFeetTraveled", m_flMaxFeetTraveled);
            // Update average feet / second stat
            SteamUserStats.UpdateAvgRateStat("AverageSpeed", m_flGameFeetTraveled, m_flGameDurationSeconds);
            // The averaged result is calculated for us
            SteamUserStats.GetStat("AverageSpeed", out m_flAverageSpeed);

            bool bSuccess = SteamUserStats.StoreStats();
            // If this failed, we never sent anything to the server, try
            // again later.
            m_bStoreStats = !bSuccess;
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: Accumulate distance traveled
    //-----------------------------------------------------------------------------
    public void AddDistanceTraveled(float flDistance)
    {
        m_flGameFeetTraveled += flDistance;
    }


    //-----------------------------------------------------------------------------
    // Purpose: Unlock this achievement
    //-----------------------------------------------------------------------------
    private void UnlockAchievement(Achievement_t achievement)
    {
        achievement.m_bAchieved = true;

        // the icon may change once it's unlocked
        //achievement.m_iIconImage = 0;

        // mark it down
        SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

        // Store stats end of frame
        m_bStoreStats = true;
    }



    //-----------------------------------------------------------------------------
    // Purpose: An achievement was stored
    //-----------------------------------------------------------------------------
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (0 == pCallback.m_nMaxProgress)
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            }
            else
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }

    private class Achievement_t
    {
        public Achievement m_eAchievementID;
        public string m_strName;
        public string m_strDescription;
        public bool m_bAchieved;

        /// <summary>
        /// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
        /// </summary>
        /// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
        /// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
        /// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
        public Achievement_t(Achievement achievementID, string name, string desc)
        {
            m_eAchievementID = achievementID;
            m_strName = name;
            m_strDescription = desc;
            m_bAchieved = false;
        }
    }
}



//private void UnlockAchievement(Achievement_t achievement)
//{
//    achievement.m_bAchieved = true;

//    // the icon may change once it's unlocked
//    //achievement.m_iIconImage = 0;

//    // mark it down
//    SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

//    // Store stats end of frame
//    m_bStoreStats = true;
//}
