using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ItsHarshdeep.LoadingScene.Controller;

public class GameOver : MonoBehaviour 
{
    GameObject retryButton;
    GameObject mainButton;
    GameObject quitButton;

	// Use this for initialization
	void Awake () 
    {
        retryButton = transform.Find("Retry").gameObject;
        retryButton.GetComponent<Button>().onClick.AddListener(() => { Retry(); });

        mainButton = transform.Find("Main Menu").gameObject;
        mainButton.GetComponent<Button>().onClick.AddListener(() => { MainMenu(); });

        quitButton = transform.Find("Quit").gameObject;
        quitButton.GetComponent<Button>().onClick.AddListener(() => { Quit(); });

        Cursor.visible = true;
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(retryButton.gameObject);
    }

    void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void MainMenu()
    {
        SceneController.LoadLevel(0, 0);
    }

    void Quit()
    {
        Debug.Log("Quiter :( ");
        Application.Quit();
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Pause P1"))
        {
            Retry();
        }
	}
}
