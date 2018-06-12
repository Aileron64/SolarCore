using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameTime : MonoBehaviour
{

    Text text;

    float timer;

    int beatCount = 0;
    public void SetBeatCount(int x) { beatCount = x; }

    int totalBeats;


    static GameTime instance;
    public static GameTime Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameTime>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<GameTime>();
                }
            }
            return instance;
        }
    }


    void Start ()
    {
        text = GetComponent<Text>();
        totalBeats = Object.FindObjectOfType<BaseLevel>().GetTotalBeats();

    }
  
    void OnEnable()
    {
        BaseLevel.OnBeat += BeatEvent;
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
    }

    void BeatEvent()
    {
        //beatCount++;
        //text.text = beatCount + " / " + totalBeats;

        text.text = BaseLevel.Instance.GetBeatNum() + " / " + totalBeats;
    }


}
