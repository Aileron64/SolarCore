using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwitterButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => { OnClick(); });
    }

    void OnClick()
    {
        Application.OpenURL("https://twitter.com/Aileron64");
    }
}
