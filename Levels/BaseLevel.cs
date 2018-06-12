using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using AudioVisualizer;
using UnityEngine.SceneManagement;

public class BaseLevel : MonoBehaviour
{
    protected enum LevelType
    {
        NONE, NORMAL, BOSS
    }

    protected LevelType levelType;
    public int GetLevelType()
    {
        if (levelType == LevelType.NORMAL)
            return 1;
        else if (levelType == LevelType.BOSS)
            return 2;
        else
            return 0;
    }


    public Vector3 corePos;
    protected const float PI = 3.14f;

    protected bool spawnCoins = true;

    public delegate void BeatAction();
    public static event BeatAction OnBeat;

    public delegate void EndLevel();
    public static event EndLevel OnLevelEnd;
    public bool gameOver;

    protected bool active = true;
    public bool GetActive() { return active; }

    protected float beatTime = 0.47f;
    public float GetBeatTime() { return beatTime; }
    protected float timer;

    protected int beatNum = 0;
    public int GetBeatNum() { return beatNum; }
    public void SetBeatNum(int x) { beatNum = x; }
    protected int totalBeats;
    public int GetTotalBeats() { return totalBeats; }

    protected const float WARP_DISTANCE = 30000;

    protected GameObject coin_prefab;
    List<GameObject> coin = new List<GameObject>();

    protected AudioSource music;
    protected GameObject solarCore;
    protected SolarRings sRings;

    public int coinLifeTime = 60;
    public Sprite waveImage;
    public Color[] color;
    public float bloomMod = 0;
    public string nextLevel;
    protected BackgroundControl background;

    public bool syncOverride;

    LevelData levelData;

    static BaseLevel instance;
    public static BaseLevel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BaseLevel>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<BaseLevel>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        //solarCore = Object.FindObjectOfType<SolarCore>().gameObject;
        sRings = Object.FindObjectOfType<SolarRings>();

        background = Camera.main.GetComponent<BackgroundControl>();

        coin_prefab = Resources.Load("Prefabs/Coin") as GameObject;
        Pooler.Instantiate(coin, coin_prefab, 50);

        music = Music.Instance.GetComponent<AudioSource>();

        

        if (HardMode.active)
        {
            music.pitch *= 1.1f;
            syncOverride = true;
        }
    }

    protected virtual void Start()
    {
        AnalyticManager.OnLevelStart();

        if(GetLevelType() > 0)
        {
            levelData = LevelManager.Instance.GetLevelData(SceneManager.GetActiveScene().name);
        }
    }
    
    void OnDisable()
    {
        Debug.Log("Beat # " + beatNum);
    }

    protected virtual void SetBeatTime(float bpm) { beatTime = 60 / (bpm * music.pitch); }

    protected virtual void SetTotalBeats(float song_min, float song_seconds)
    {
        totalBeats = (int)(((song_min * 60) + (song_seconds / music.pitch)) / beatTime);
    }

    public void StartAt(int x)
    {
        if (Application.isEditor)
        {
            beatNum = x;
            Music.Instance.GetComponent<AudioSource>().time = x * beatTime;
            GameTime.Instance.SetBeatCount(x - 1);
        }
    }

    protected virtual void InstantiatePool(List<GameObject> enemyPool, GameObject enemy, int size)
    {
        //if (size < 2)
        //    size = 2;

        for (int i = 0; i < size; i++)
        {
            //if(i == 0)
            //{
            //    enemyPool[0] = enemy;
            //}
            //else
            //{
                enemyPool.Add(Instantiate(enemy) as GameObject);
                enemyPool[i].gameObject.SetActive(false);
            //}
        }
    }

    #region Game Management

    void Beat()
    {
        if (active)
        {
            if (OnBeat != null)
                OnBeat();

            Spawn(beatNum);
            beatNum++;

            if ((music.time / beatTime) - beatNum > 1.5f && !syncOverride)
            {
                music.time = (beatNum + 1) * beatTime;
                TextManager.Instance.DebugText("MUSIC DESYNC");
            }

            if (beatNum >= totalBeats && levelType == LevelType.NORMAL)
            {
                LevelComplete();
            }
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= beatTime && active)
        {
            Beat();

            //if (timer >= beatTime * 2)
            //{
            //    music.time = (beatNum + 1) * beatTime;
            //    TextManager.Instance.DebugText("MUSIC DESYNC");       
            //}

            timer -= beatTime;
        }
    }

    public virtual void GameOver()
    {
        active = false;
        gameOver = true;

        Camera.main.GetComponent<ProCamera2D>().enabled = false;

        OnLevelEnd();

        //Camera.main.GetComponent<ProCamera2DZoomToFitTargets>().MaxZoomInAmount = 3;
        //Camera.main.GetComponent<ProCamera2DZoomToFitTargets>().DisableWhenOneTarget = false;

        GUIManager.Instance.GameOver();
        AnalyticManager.OnGameOver();

    }

    public virtual void LevelComplete()
    {
        active = false;
        gameOver = false;

        //Disable Music Bars
        //MusicBars[] musicBars = (MusicBars[])GameObject.FindObjectsOfType(typeof(MusicBars));

        levelData.completed = true;
        LevelManager.Instance.Save();

        Camera.main.GetComponent<ProCamera2D>().enabled = false;

        OnLevelEnd();

        GUIManager.Instance.WinScreen();
        AnalyticManager.OnLevelComplete();
    }

    #endregion

    #region Coin Spawning

    public void SpawnCoin(float x, float z)
    {
        SpawnCoin(x, z, corePos); 
    }

    public void SpawnCoin(float x, float z, Vector3 center)
    {

        Pooler.GetObject(coin, new Vector3(x, 0, z) + center, Quaternion.identity);

        ScoreManager.Instance.AddCointTotal();
    }

    public void SpawnCoinCircle(int amount, float radius)
    {
        SpawnCoinCircle(amount, radius, corePos);
    }

    public void SpawnCoinCircle(int amount, float radius, float offset)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnCoin(Mathf.Sin(i * PI * 2 / amount + offset) * radius,
                Mathf.Cos(i * PI * 2 / amount + offset) * radius, corePos);
        }
    }

    public void SpawnCoinCircle(int amount, float radius, Vector3 center)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnCoin(Mathf.Sin(i * PI * 2 / amount) * radius,
                Mathf.Cos(i * PI * 2 / amount) * radius, center);
        }
    }

    public void SpawnCoinCircle(int circleAmount, int circleRadius, int amount, int radius, float offset)
    {
        for (int i = 0; i < circleAmount; i++)
        {
            SpawnCoinCircle(amount, radius,         
                new Vector3(Mathf.Sin(i * PI * 2 / circleAmount + offset) * circleRadius, 
                0, Mathf.Cos(i * PI * 2 / circleAmount + offset) * circleRadius) + corePos);
        }
    }

    public void SpawnCoinRandom(int amount, float range)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnCoin(Random.Range(-range, range), Random.Range(-range, range));
        }
    }



    #endregion

    #region Enemy Spawning

    protected virtual void Spawn(int num) { }

    //Using object pooling for enemy spawning
    public GameObject SpawnEnemy(List<GameObject> enemyPool, Vector3 location)
    {
        return SpawnEnemy(enemyPool, location.x, location.z, corePos);
    }

    public GameObject SpawnEnemy(List<GameObject> enemyPool, float x, float z)
    {
        return SpawnEnemy(enemyPool, new Vector3(x, -WARP_DISTANCE, z));
    }

    public GameObject SpawnEnemy(List<GameObject> enemyPool, float x, float z, Vector3 center)
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeSelf)
            {
                enemyPool[i].transform.position = new Vector3(x, -WARP_DISTANCE, z) + center;
                enemyPool[i].GetComponent<BaseEnemy>().OnSpawn();
                enemyPool[i].SetActive(true);

                return enemyPool[i];
            }
        }

        GameObject obj = Instantiate(enemyPool[0]) as GameObject;
        obj.SetActive(false);
        obj.transform.position = new Vector3(x, -WARP_DISTANCE, z) + center;
        obj.GetComponent<BaseEnemy>().OnSpawn();
        obj.SetActive(true);

        Debug.Log(enemyPool[0].name + " Overflow - Size: " + enemyPool.Count);

        return obj;
    }

    public void SpawnCircle(List<GameObject> enemyPool, int amount, float radius)
    {
        Vector3 loc = Vector3.zero;

        for (int i = 0; i < amount; i++)
        {
            loc.y = -WARP_DISTANCE;
            loc.x = Mathf.Sin(i * PI * 2 / amount) * radius;
            loc.z = Mathf.Cos(i * PI * 2 / amount) * radius;

            SpawnEnemy(enemyPool, loc);
        }
    }

    public void SpawnCircle(List<GameObject> enemyPool, int amount, float radius, Vector3 offset)
    {
        Vector3 loc = Vector3.zero;

        for (int i = 0; i < amount; i++)
        {
            loc.y = -WARP_DISTANCE;
            loc.x = Mathf.Sin(i * PI * 2 / amount) * radius;
            loc.z = Mathf.Cos(i * PI * 2 / amount) * radius;

            SpawnEnemy(enemyPool, loc + offset);
        }
    }

    public void SpawnCircle(List<GameObject> enemyPool, int amount, float radius, float offset)
    {
        Vector3 loc = Vector3.zero;

        for (int i = 0; i < amount; i++)
        {
            loc.y = -WARP_DISTANCE;
            loc.x = Mathf.Sin(i * PI * 2 / amount + offset) * radius;
            loc.z = Mathf.Cos(i * PI * 2 / amount + offset) * radius;

            SpawnEnemy(enemyPool, loc);
        }
    }

    protected void SpawnHalfCircle(List<GameObject> enemyPool, int amount, int circleSize, float radius, Vector3 offset, float rotOffset)
    {
        Vector3 loc = Vector3.zero;

        for (int i = 0; i < amount; i++)
        {
            loc.y = -WARP_DISTANCE;
            loc.x = Mathf.Sin(i * PI * 2 / circleSize + rotOffset) * radius;
            loc.z = Mathf.Cos(i * PI * 2 / circleSize + rotOffset) * radius;

            SpawnEnemy(enemyPool, loc + offset);
        }

    }

    protected void SpawnRandom(List<GameObject> enemyPool, int amount, float range)
    {
        Vector3 spawnLocation;
        Vector3 spawnAnchor = new Vector3(1000, 0, 1000);

        for (int i = 0; i < amount; i++)
        {
            //float distance = -1000 - (i * spawnDelay);

            spawnLocation = new Vector3(Random.Range(-range, range),
                -WARP_DISTANCE, Random.Range(-range, range));
            //+ spawnAnchor;

            SpawnEnemy(enemyPool, spawnLocation);
        }
    }

    public virtual void SpawnMini(float x, float z, Vector3 pos)
    {
        DebugText.Instance.SetText("SPAWN MINI MISSING OVERRIDE");
    }

    #endregion
}



