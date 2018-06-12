using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour 
{
    EventSystem eventSystem;
    public GameObject startButton;
    //public GameObject optionsButton;
    //public GameObject debugButton;
    //public GameObject quitButton;

    public GameObject titleGUI;
    public GameObject titleLogo;

    public GameObject shipSelect;
    bool shipSelectActive;

	void Start () 
    {
        eventSystem = EventSystem.current;
        startButton.GetComponent<Button>().onClick.AddListener(() => { StartButton(); });

        //muteButton = transform.Find("MuteMusic").gameObject;
        //muteButton.GetComponent<Button>().onClick.AddListener(() => { MuteMusic(); });
        //muteText = muteButton.transform.Find("Text").GetComponent<Text>();

        AnalyticManager.OnStart();

        PlayerManager.coopActive = false;
        PlayerManager.titleInput = true;

        Cursor.visible = true;
    }


    void StartButton()
    {
        GetComponent<MapControls>().ActivateMap();

        //GetComponent<MapControls>().SelectButton();

        eventSystem.SetSelectedGameObject(shipSelect);

        titleGUI.SetActive(false);
        //titleLogo.SetActive(false);
    }

    public void ActivateMenu()
    {
        titleGUI.SetActive(true);
        titleLogo.SetActive(true);

        eventSystem.SetSelectedGameObject(startButton);
    }

}
