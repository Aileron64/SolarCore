using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ItsHarshdeep.LoadingScene.Controller;
using DG.Tweening;

public class WinScreen : MonoBehaviour
{
    public Button nextButton;
    public Button stageButton;

    public GameObject winText;
    public GameObject coinText;
    public GameObject multiText;
    public GameObject damageText;
    public GameObject scoreText;
    public GameObject score;
    public GameObject gradeText;

    bool active = false;

    ScoreManager SM;

    float scaleFromValue = 0.6f;

    // Use this for initialization
    void Start()
    {
        nextButton.onClick.AddListener(() => { Next(); });
        stageButton.onClick.AddListener(() => { StageSelect(); });

        SM = ScoreManager.Instance;

        Invoke("WinText", 0.2f);
    }

    void WinText()
    {
        winText.SetActive(true);
        winText.transform.DOScale(scaleFromValue, 0.2f).From();

        if(BaseLevel.Instance.GetLevelType() == 1)
            Invoke("CoinText", 3.2f);
        else
            Invoke("ActivateButtons", 3.2f);

    }

    void CoinText()
    {
        coinText.SetActive(true);
        coinText.transform.DOScale(scaleFromValue, 0.2f).From();
        coinText.GetComponent<Text>().text = SM.coinNum + " / " + SM.coinTotal + " Orbs";

        if (SM.coinNum >= SM.coinTotal)
            SM.score += 10000;

        Invoke("Multitext", 0.5f);
    }

    void Multitext()
    {
        multiText.SetActive(true);
        multiText.transform.DOScale(scaleFromValue, 0.2f).From();
        multiText.GetComponent<Text>().text = "Highest Streak: " + SM.maxMultiplyer;

        SM.score += SM.maxMultiplyer * 1000;

        Invoke("DamageText", 0.5f);
    }

    void DamageText()
    {
        damageText.SetActive(true);
        damageText.transform.DOScale(scaleFromValue, 0.2f).From();
        damageText.GetComponent<Text>().text = SM.damageTaken + " Damage Taken";

        SM.score -= SM.damageTaken * 10;

        Invoke("ScoreText", 0.5f);
    }

    void ScoreText()
    {
        scoreText.SetActive(true);
        scoreText.transform.DOScale(scaleFromValue, 0.2f).From();

        Invoke("Score", 1.5f);
    }

    void Score()
    {
        score.SetActive(true);
        score.transform.DOScale(scaleFromValue, 0.2f).From();
        score.GetComponent<Text>().text = SM.score + "";

        //Invoke("GradeText", 1.5f);
        Invoke("ActivateButtons", 0.7f);
    }


    void GradeText()
    {
        gradeText.SetActive(true);
        gradeText.transform.DOScale(1.8f, 0.1f).From();

        Invoke("ActivateButtons", 0.7f);
    }


    void ActivateButtons()
    {
        
            nextButton.gameObject.SetActive(true);

        stageButton.gameObject.SetActive(true);
        winText.SetActive(true);
        active = true;
        Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
    }


    void Next()
    {
        //if(SceneListCheck.Has("sceneNameHere"))

        if(Application.CanStreamedLevelBeLoaded(BaseLevel.Instance.nextLevel))
        {
            SceneController.LoadLevel(BaseLevel.Instance.nextLevel, 0);
        }
        else
        {
            Debug.Log("Scene not found");
            SceneController.LoadLevel(0, 0);
        }



    }

    void StageSelect()
    {
        SceneController.LoadLevel(0, 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause P1") && active)
        {
            Next();
        }
    }
}
