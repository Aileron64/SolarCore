using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatBars : MonoBehaviour
{
    public Slider[] slider;
    public Slider orbSlider;

    static CombatBars instance;

    public static CombatBars Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatBars>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<CombatBars>();
                }
            }
            return instance;
        }
    }

    public Slider GetBar(string name)
    {
        for (int i = 0; i < slider.Length; i++)
        {
            if (slider[i])
            {


                if (!slider[i].gameObject.activeSelf)
                {
                    slider[i].gameObject.SetActive(true);

                    if (slider[i].gameObject.transform.Find("Text"))
                        slider[i].gameObject.transform.Find("Text").GetComponent<Text>().text = name;
                    else
                        Debug.Log("Slider Text Object Missing");

                    return slider[i];
                }
            }

        }


        Debug.Log("OUT OF SLIDERS");
        return null;
    }

    public Slider GetOrbBar() { return orbSlider; }

}
