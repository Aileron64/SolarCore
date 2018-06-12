using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using ItsHarshdeep.LoadingScene.Controller;
using UnityEngine.UI;

public class MapControls : MonoBehaviour
{
    MapGUI gui;

    bool lockedOn = false;

    public GameObject target;
    public GameObject levelMap;
    public GameObject mapGUI;
    public GameObject shipSelectGUI;

    public GameObject map3DButtons;

    bool active;
    bool shipSelectActive;

    Vector3 defaultCamPos;
    Vector3 defualtCamRot;

    public Button selectButton;
    public Button playButton;
    public Button backButton;

    bool inputFlag1;
    bool inputFlag2;

    float inputTimer1;
    float inputTimer2;

    public MapPoint[] mapPoint;


    static MapControls instance;
    public static MapControls Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapControls>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<MapControls>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        defaultCamPos = Camera.main.transform.position;
        defualtCamRot = Camera.main.transform.localEulerAngles;

        gui = mapGUI.GetComponent<MapGUI>();

        selectButton.GetComponent<Button>().onClick.AddListener(() => { SelectButton(); });
        playButton.GetComponent<Button>().onClick.AddListener(() => { PlayButton(); });
        backButton.GetComponent<Button>().onClick.AddListener(() => { BackButton(); });

        mapPoint[0  ].Active();

        for (int i = 0; i < mapPoint.Length; i++)
        {
            mapPoint[i].levelData = LevelManager.Instance.GetLevelData(mapPoint[i].sceneName);

            if(mapPoint[i].levelData.completed)
            {

                mapPoint[i].Completed();

                mapPoint[i].quickButton.Completed();

                if (i < mapPoint.Length - 1)
                    mapPoint[i + 1].Active();
            }
            else
            {
                if (i < mapPoint.Length - 1)
                    mapPoint[i + 1].quickButton.Deactive();
            }
        }
    }


    public void ActivateMap()
    {
        mapGUI.SetActive(true);
        active = true;
        levelMap.GetComponent<Rotate>().enabled = false;
        NewTarget(target);
        map3DButtons.SetActive(true);

        
    }

    public void DeactivateMap()
    {
        active = false;
        levelMap.GetComponent<Rotate>().enabled = true;
        GetComponent<Title>().ActivateMenu();

        Camera.main.transform.DOMove(defaultCamPos, 0.5f);
        Camera.main.transform.DOLocalRotate(defualtCamRot, 0.5f);

        mapGUI.SetActive(false);
        map3DButtons.SetActive(false);
    }

    public void SelectButton()
    {
        shipSelectActive = true;

        active = false;
        mapGUI.SetActive(false);
        shipSelectGUI.SetActive(true);
    }

    public void PlayButton()
    {
        SceneController.LoadLevel(target.GetComponent<MapPoint>().sceneName, 0);
    }

    public void BackButton()
    {
        //active = true;
        //mapGUI.SetActive(true);    
        shipSelectGUI.SetActive(false);

        DeactivateMap();
    }

    void Update()
    {

        if(shipSelectActive)
        {
            if (Input.GetButtonDown("Pause P1"))
            {
                PlayButton();
            }
            
            if (Input.GetButtonDown("Cancel P1"))
            {
                BackButton();
            }



        }





        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeactivateMap();
                //SceneManager.LoadScene(0);
            }



            if (Input.GetAxis("Horizontal P1") < -0.5f)
            {
                if(inputFlag1 || inputTimer1 >= 0.6f)
                {
                    NextTarget();
                    inputFlag1 = false;

                    if (inputTimer1 >= 0.6f)
                        inputTimer1 = 0.4f;
                }

                
                inputTimer1 += Time.deltaTime;
            }               
            else
            {
                inputFlag1 = true;
                inputTimer1 = 0;
            }
                


            if (Input.GetAxis("Horizontal P1") > 0.5f)
            {
                if(inputFlag2 || inputTimer2 >= 0.6f)
                {
                    PrevTarget();
                    inputFlag2 = false;

                    if(inputTimer2 >= 0.6f)
                        inputTimer2 = 0.4f;
                }

                inputTimer2 += Time.deltaTime;  
            }
            else
            {
                inputFlag2 = true;
                inputTimer2 = 0;
            }
                
            


            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextTarget();
            

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PrevTarget();
            
        }
    }

    void NextTarget()
    {
        if (target.GetComponent<MapPoint>().nextPoint)
            NewTarget(target.GetComponent<MapPoint>().nextPoint);
    }

    void PrevTarget()
    {
        if (target.GetComponent<MapPoint>().prevPoint)
            NewTarget(target.GetComponent<MapPoint>().prevPoint);
    }

    public void NewTarget(GameObject _target)
    {
        if(_target.GetComponent<MapPoint>().active)
        {
            if (target)
            {
                target.GetComponent<MapPoint>().quickButton.GetComponent<Button3D>().model.DOScale(
                    target.GetComponent<MapPoint>().quickButton.defaultHeight, 0.2f);

                target.GetComponent<MapPoint>().quickButton.GetComponent<Button3D>().active = true;
            }


            target = _target;
            target.GetComponent<MapPoint>().quickButton.GetComponent<Button3D>().model.DOScaleY(
                400, 0.2f);

            target.GetComponent<MapPoint>().quickButton.GetComponent<Button3D>().active = false;

            float moveTime = 0.5f;

            //Move
            float distance = target.transform.position.magnitude;
            Vector3 newPos = target.transform.position.normalized * (distance + 70);
            newPos.y = 50;

            Camera.main.transform.DOMove(newPos, moveTime);

            //Rotate
            //transform.DOLocalRotate(new Vector3(30, 
            //    Mathf.Rad2Deg * -1 * Mathf.Asin(target.transform.position.x / target.transform.position.magnitude), 0), moveTime);

            Vector3 newRot = target.transform.rotation.eulerAngles;
            newRot.x = 30;

            Camera.main.transform.DOLocalRotate(newRot, moveTime);


            GetComponent<MapGUI>().SetLevelName(target.GetComponent<MapPoint>().levelName);

            //gui.SetLevelName(target.GetComponent<MapPoint>().levelName);
        }
    }



    //public void LoadLevel()
    //{

    //    SceneController.LoadLevel(target.GetComponent<MapPoint>().sceneName, 0);

    //    //SceneManager.LoadScene(target.GetComponent<MapPoint>().sceneName);
    //}



}

