using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLevelButton : MonoBehaviour
{
    LevelData levelData;

    void OnEnable()
    {
        //levelData = LevelManager.Instance.GetLevelData(gameObject.name);


            
    }

    void Start()
    {
        levelData = LevelManager.Instance.GetLevelData(gameObject.name);

        if (levelData.completed)
        {
            ColorBlock cBlock = GetComponent<Button>().colors;
            cBlock.normalColor = Color.green;
            //Debug.Log(gameObject.name + " - Completed");
            GetComponent<Button>().colors = cBlock;
        }
    }

}
