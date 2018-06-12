using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AudioVisualizer;

public class AudioSlider : MonoBehaviour
{
    static float volume = 0.3f;
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();

        AudioListener.volume = volume;
        slider.value = AudioListener.volume;
    }


	
	// Update is called once per frame
	void Update ()
    {
        AudioListener.volume = slider.value;
        volume = slider.value;
    }

    void OnDisable()
    {
        //MusicBars[] musicBars = (MusicBars[])GameObject.FindObjectsOfType(typeof(MusicBars));

        //foreach (MusicBars mBar in musicBars)
        //{
        //    if (AudioListener.volume == 0)
        //        mBar.active = false;
        //    else
        //        mBar.active = true;
        //}
    }
}
