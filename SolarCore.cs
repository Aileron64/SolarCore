using UnityEngine;
using System.Collections;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;

public class SolarCore : MonoBehaviour
{
    public GameObject core;
    public GameObject outerCore;

    public GameObject inner;
    public GameObject outer;

    float beatTimer;
    float beatTime;

    public float corePulseSize;
    public float outerPulseSize;

    public GameObject coreExplosionPrefab;
    GameObject explosion;

    public Material blueSun;


    void Start()
    {
        beatTime = Object.FindObjectOfType<BaseLevel>().GetBeatTime();

        explosion = Instantiate(coreExplosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActive(false);   
    }

    void Update()
    {
        core.transform.Rotate(new Vector3(0, 0.2f, 0) * Time.timeScale);
    }

    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
        BaseLevel.OnLevelEnd += LevelEnd;
    }

    void OnDisable()
    {
        BaseLevel.OnLevelEnd -= LevelEnd;
        BaseLevel.OnBeat -= OnBeat;
    }

    void OnBeat()
    {

        core.transform.DOScale(corePulseSize, beatTime).From();

        outerCore.transform.DOScale(outerPulseSize, beatTime).From();

        //iTween.ScaleTo(core, iTween.Hash("scale", new Vector3(corePulseSize, corePulseSize, corePulseSize),
        //    "time", BaseLevel.Instance.GetBeatTime() / 2,
        //    "looptype", "pingPong", "easeType", easeType));

        //iTween.ScaleTo(outerCore, iTween.Hash("scale", new Vector3(outerPulseSize, outerPulseSize, outerPulseSize),
        //    "time", BaseLevel.Instance.GetBeatTime() / 2,
        //    "looptype", "pingPong", "easeType", easeType));
    }

    public void LevelEnd()
    {
        if(!BaseLevel.Instance.gameOver)
        {
            Invoke("LevelEnd2", 1.4f);   
            Camera.main.transform.DOMove(new Vector3(500, 1000, 1000), 0.9f);
        }

    }

    void LevelEnd2()
    {
        explosion.SetActive(true);

        //inner.SetActive(false);
        //outer.SetActive(false);

        //core.GetComponent<Renderer>().material = blueSun;

        Camera.main.transform.DOMove(new Vector3(400, 1500, 1000), 0.3f);
        //ProCamera2DShake.Instance.Shake("PlayerHit");
    }

}