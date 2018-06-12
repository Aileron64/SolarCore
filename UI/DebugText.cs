using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebugText : MonoBehaviour
{
    Text text;

    static DebugText instance;

    public static DebugText Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DebugText>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<DebugText>();
                }
            }
            return instance;
        }
    }
    

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string x)
    {
        text.text = x;
    }

}
