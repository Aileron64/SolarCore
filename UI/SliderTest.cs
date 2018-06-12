using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioVisualizer;


public class SliderTest : MonoBehaviour
{
    Slider slider;


    float x;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        //DebugText.Instance.SetText("" + AudioSampler.instance.GetInstantEnergy(0));
        //DebugText.Instawnce.SetText ;


        //Debug.Log(AudioSampler.instance.GetInstantEnergy(0));
        slider.value = AudioSampler.instance.GetInstantEnergy(0);
    }

}
