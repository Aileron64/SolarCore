using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class LevelManager : MonoBehaviour
{
    static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    public SaveData data;
    string fileName = "Save_Data";

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        //Load

        if (QuickSaveRoot.Exists(fileName))
            QuickSaveReader.Create(fileName).Read<SaveData>("Data", (r) => { data = r; });
        else
            Debug.Log("No Save File");

    }

    void Start()
    {
        Invoke("Save", 1);
    }

    public LevelData GetLevelData(string name)
    {
        for (int i = 0; i < data.level.Count; i++)
        {
            if (data.level[i].name == name)
            {
                //if(data.level[i].completed)
                //    Debug.Log(data.level[i].name + ": " + data.level[i].completed);

                return LevelManager.Instance.data.level[i];
            }
        }

        LevelData tmpData = new LevelData();
        tmpData.name = name;
        data.level.Add(tmpData);
        return tmpData;
    }

    public void Save()
    {
        QuickSaveWriter.Create(fileName)
            .Write("Data", data)
            .Commit();
    }

    //private void OnDisable()
    //{
    //    for (int i = 0; i < data.level.Count; i++)
    //    {
    //        Debug.Log(data.level[i].name);
    //    }
    //}
}

[System.Serializable]
public class SaveData
{
    public List<LevelData> level = new List<LevelData>();
}

[System.Serializable]
public class LevelData
{

    public string name;
    public bool completed = false;
    public int highscore = 0;

    public int blueScore = 0;
    public int cyanScore = 0;
    public int purpleScore = 0;
    public int greenScore = 0;
    public int whiteScore = 0;
    public int turqScore = 0;
}

