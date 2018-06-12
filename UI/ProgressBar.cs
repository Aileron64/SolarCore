using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour 
{

    Slider slider;
    int beatCount;

    public GameObject background;
    //public GameObject fill;

    void Start ()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = Object.FindObjectOfType<BaseLevel>().GetTotalBeats();

        if(BaseLevel.Instance.GetLevelType() == 1)
        {
            if (BaseLevel.Instance.waveImage)
            {
                slider.fillRect.GetComponent<Image>().sprite = BaseLevel.Instance.waveImage;
                background.GetComponent<Image>().sprite = BaseLevel.Instance.waveImage;
            }
            else
                Debug.Log("Wave Image Missing");
        }
        else
        {
            gameObject.SetActive(false);
        }

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
        beatCount = BaseLevel.Instance.GetBeatNum();
        slider.value = beatCount;
    }




}
