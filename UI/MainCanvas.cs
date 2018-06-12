using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCanvas : MonoBehaviour 
{
    GameObject player;
    GameObject spawner;

    Text waveInfo;
    Text waveTimer;
    Text warning;
    Text warning2;
    Text debug;
    Text score;


    int numOfObjects;
    GameObject[] gameObjects;

    float lagTime;

    //public static string bigText;

    bool debugText = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawner = GameObject.Find("Spawner");
        
        //waveInfo = transform.FindChild("Wave Info").GetComponent<Text>();
        //waveTimer = transform.FindChild("Wave Time").GetComponent<Text>();
        

        debug = transform.Find("FPS Text").GetComponent<Text>();
        score = transform.Find("Score").GetComponent<Text>();


        score.text = "";
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.BackQuote)))
        {
            debugText = !debugText;
        }

        if (1.0f / Time.deltaTime <= 30)
        {
            lagTime += Time.deltaTime;
        }


        if (debugText)
            debug.text = string.Format("Solar Distance: {0:f2}\nFPS: {1}\nLagtime: {2}\nNumber of Objects: {3}", 
                player.GetComponent<Player>().GetSolarDistance(), 
                (int)(1.0f / Time.deltaTime), 
                lagTime,
                numOfObjects);
        else
            debug.text = "";
            
    }

}
