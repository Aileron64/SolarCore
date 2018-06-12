using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour 
{
    //GameObject[] playerGUI = new GameObject[2];
    //GameObject radar;

    GameObject pauseMenu;
    GameObject gameOver;
    GameObject winScreen;
    public static bool isPaused = false;
    public static bool isGameOver;

    //public Text scoreText;
    //public Text highScoreText;
    //public Text newHighText;

    //GameMan GM;
    AudioSource Music;

    static GUIManager instance;
    public static GUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GUIManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<GUIManager>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        isGameOver = false;

        pauseMenu = transform.Find("Pause Menu").gameObject;
        gameOver = transform.Find("Game Over").gameObject;
        winScreen = transform.Find("Win Screen").gameObject;

        //playerGUI[0] = transform.FindChild("Player 1 GUI").gameObject;
        //playerGUI[1] = transform.FindChild("Player 2 GUI").gameObject;

        //radar = transform.FindChild("Radar").gameObject;


        Music = Object.FindObjectOfType<Music>().GetComponent<AudioSource>();


        //if (LM.levelID == 101)
        //    highScoreActive = true;

        CreateGUI();
    }

    public void CreateGUI()
    {
        if (PlayerManager.coopActive)
        {
            //radar.SetActive(false);
            //playerGUI[1].SetActive(true);

            //GameObject p1SkillICons = playerGUI[0].transform.FindChild("Skill Icons").gameObject;

            //p1SkillICons.GetComponent<RectTransform>().anchorMin = new Vector2(0.35f, 0);
            //p1SkillICons.GetComponent<RectTransform>().anchorMax = new Vector2(0.35f, 0);
            //panelRectTransform.anchorMin = new Vector2(1, 0);
            //panelRectTransform.anchorMax = new Vector2(0, 1);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause P1") && !isPaused && BaseLevel.Instance.GetActive())
        {
            Pause();
        }

    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        Music.Pause();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Music.Pause();

        //isGameOver = true;
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
        //Time.timeScale = 0.0f;
    }
}
