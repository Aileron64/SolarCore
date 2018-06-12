using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using ItsHarshdeep.LoadingScene.Controller;

public class PauseMenu : MonoBehaviour 
{
    EventSystem eventSystem;

    public GameObject resumeButton;
    GameObject mainButton;
    GameObject quitButton;
    GameObject confirmation;
    GameObject yesButton;
    GameObject noButton;

    AudioSource Music;

    Text stats;
    int quitIndex;

	void Awake () 
    {
        
        eventSystem = EventSystem.current;

        //resumeButton = transform.Find("Resume").gameObject;
        resumeButton.GetComponent<Button>().onClick.AddListener(() => { Resume(); });

        mainButton = transform.Find("Main Menu").gameObject;
        mainButton.GetComponent<Button>().onClick.AddListener(() => { Confirmation(0); });

        quitButton = transform.Find("Quit").gameObject;
        quitButton.GetComponent<Button>().onClick.AddListener(() => { Confirmation(1); });

        confirmation = transform.Find("Confirmation").gameObject;

        yesButton = confirmation.transform.Find("Yes").gameObject;
        yesButton.GetComponent<Button>().onClick.AddListener(() => { YesButton(); });

        noButton = confirmation.transform.Find("No").gameObject;
        noButton.GetComponent<Button>().onClick.AddListener(() => { NoButton(); });

        //stats = transform.FindChild("Stats").GetComponent<Text>();
        Music = Object.FindObjectOfType<Music>().GetComponent<AudioSource>();

        Cursor.visible = true;
    }
    
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    void Resume()
    {
        Music.UnPause();
        GUIManager.isPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    void Confirmation(int x)
    {
        
        quitIndex = x;
        confirmation.SetActive(true);
        eventSystem.SetSelectedGameObject(confirmation.transform.Find("Yes").gameObject);
    }

    void YesButton()
    {
        if (quitIndex == 0)
        {
            GUIManager.isPaused = false;
            Time.timeScale = 1;

            SceneController.LoadLevel(0, 0);
        }
        else
        {
            Debug.Log("Quiter :( ");
            Application.Quit();
        }
    }

    void NoButton()
    {
        if (quitIndex == 0)
        {
            EventSystem.current.SetSelectedGameObject(mainButton.gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(quitButton.gameObject);
        }

        confirmation.SetActive(false);        
    }

	void Update () 
    {
        if (Input.GetButtonDown("Pause P1"))
        {
            Resume();
        }
	}
}
