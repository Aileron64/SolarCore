using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardMode : MonoBehaviour
{
    public static bool active = false;

    Toggle toggle;

    void Awake()
    {
        toggle = GetComponent<Toggle>();


        toggle.isOn = active;
    }



    public void Toggle()
    {
        active = toggle.isOn;
    }

}
