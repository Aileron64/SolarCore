using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapGUI : MonoBehaviour
{
    public Text levelName;
    public GameObject levelMenu;


    void Start()
    {
        
    }


    public void SetLevelName(string _name)
    {
        levelName.text = _name;
    }

    public void ToggleMenu(bool x)
    {
        levelMenu.gameObject.SetActive(x);       
    }

    void LoadLevel()
    {
        //Object.FindObjectOfType<MapControls>().LoadLevel();
    }

}
