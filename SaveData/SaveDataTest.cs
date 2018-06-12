using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CI.QuickSave;

public class SaveDataTest : MonoBehaviour
{
    public Text text;

    string path;

    int clickCount;

    string fileName = "TestData";

    void Awake() 
    {
        GetComponent<Button>().onClick.AddListener(() => { ButtonPress(); });

        //if (QuickSaveRaw.Exists(fileName))
        //    Debug.Log("Exists");


        //Debug.Log(QuickSaveRaw.Exists(fileName));

        //QuickSaveReader.TryLoad<>

        //Save();
        Load();

        text.text = "" + clickCount;
    }


    public void Save()
    {
        QuickSaveWriter.Create(fileName)
                .Write("ClickCount", clickCount)
                .Commit();

        //Content.text = QuickSaveRaw.LoadString("Inputs.json");
    }

    public void Load()
    {
        QuickSaveReader.Create(fileName)
               .Read<int>("ClickCount", (r) => { clickCount = r; });
    }

    void ButtonPress()
    {

        clickCount++;
        text.text = "" + clickCount;


        Save();

    }

}

