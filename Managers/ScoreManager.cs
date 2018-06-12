using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score;
    float scoreMultiplyer = 1;

    const float COIN_VALUE = 100;
   
    public GameObject scoreText;
    public GameObject highScoreText;

    public GameObject multiText;
    public GameObject coinText;

    static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }



    float killSteakValue;
    const float DEFAULT_CAP = 40;
    float streakValueCap = DEFAULT_CAP;

    public int coinNum;
    public int coinTotal;
    public int maxMultiplyer = 0;
    public int damageTaken;

    bool active;

    void Start()
    {
        if (BaseLevel.Instance.GetLevelType() == 1)
        {
            active = true;
        }
        else
        {
            active = false;
            scoreText.GetComponent<Text>().text = "";
        }
    } 

    void UpdateText()
    {
        if(active)
        {
            scoreText.GetComponent<Text>().text = "" + (int)score;
            coinText.GetComponent<Text>().text = coinNum + "/" + coinTotal;
        }
    }

    public void AddCoin(Bounds bounds)
    {
        if(active)
        {
            score += COIN_VALUE * scoreMultiplyer;
            TextManager.Instance.ScoreText((int)(COIN_VALUE * scoreMultiplyer), bounds, 50);
            coinNum++;
            UpdateText();
        }
    }

    public void AddCointTotal()
    {       
        coinTotal++;
        UpdateText();
    }

    public void AddEnemyKill(float value, Bounds bounds)
    {
        if(active)
        {
            float addedScore = value * 10 * scoreMultiplyer;
            score += addedScore;

            TextManager.Instance.ScoreText((int)addedScore, bounds, 50);

            killSteakValue += value;
            if (killSteakValue >= streakValueCap)
            {
                scoreMultiplyer++;
                TextManager.Instance.PopupText("x" + scoreMultiplyer, new Vector2(0, -300), 50);

                if (scoreMultiplyer > maxMultiplyer)
                    maxMultiplyer = (int)scoreMultiplyer;

                streakValueCap += DEFAULT_CAP + (streakValueCap * 0.5f);

                multiText.GetComponent<Text>().text = "x" + (int)scoreMultiplyer;
            }


            UpdateText();
        }
    }

    public void EndKillStreak()
    {
        if(active)
        {
            if (scoreMultiplyer > 1)
                TextManager.Instance.PopupText("Streak Ended", new Vector2(0, -300), 30);

            scoreMultiplyer = 1;
            multiText.GetComponent<Text>().text = "";
            killSteakValue = 0;
            streakValueCap = DEFAULT_CAP;
        }
    }
}
