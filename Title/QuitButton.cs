using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { QuitGame(); });
    }

    void QuitGame()
    {
        Debug.Log("Quiter :( ");
        Application.Quit();
    }

}
