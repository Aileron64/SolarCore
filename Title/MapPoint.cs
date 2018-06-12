using UnityEngine;
using System.Collections;

public class MapPoint : MonoBehaviour
{

    public string levelName;
    public string sceneName;

    public int levelID;

    public GameObject nextPoint;
    public GameObject prevPoint;

    public bool active;
    public bool completed;

    public MapQuickButton quickButton;

    public LevelData levelData;

    void Awake()
    {
        transform.LookAt(Vector3.zero);
        
    }

    public void Completed()
    {
        completed = true;
    }

    public void Active()
    {
        active = true;
    }

}
