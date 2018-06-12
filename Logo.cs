using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItsHarshdeep.LoadingScene.Controller;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    //void Awake()
    //{
    //    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    //    {
    //        string name = SceneUtility.GetScenePathByBuildIndex(i);
    //        name = name.Substring(name.LastIndexOf("/") + 1);
    //        name = name.Remove(name.IndexOf("."));

    //        Debug.Log(i + ": " + name);
    //    }
    //}

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneController.LoadLevel(0, 0);
        }
    }
}
