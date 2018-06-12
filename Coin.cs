using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public int delay;

    float beatTime;

    public GameObject[] orbs;

    float timer;
    float lifetime;
    bool failsafe;
    bool active;

    void Awake()
    {
        beatTime = BaseLevel.Instance.GetBeatTime();      
    }

    void OnEnable()
    {
        lifetime = beatTime * BaseLevel.Instance.coinLifeTime;

        BaseLevel.OnBeat += OnBeat;
        transform.localScale = new Vector3(0, 0, 0);
        GetComponent<SphereCollider>().enabled = true;

        timer = 0;
        failsafe = false;
        active = false;

        for (int i = 0; i < 3; i++)
        { 
            //iTween.ColorTo(orbs[i], iTween.Hash("a", 1, "namedcolorvalue", "_TintColor"));

            orbs[i].GetComponent<Renderer>().material.DOFade(1, "_TintColor", 0.1f);

            //orbs[i].
        }

        Activate();
    }

    void Activate()
    {
        transform.DOScale(110, beatTime).OnComplete(Active);
    }
    
    void Active()
    {
        active = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            //iTween.Stop(gameObject);

            //iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0, 0, 0),
            //    "time", beatTime, "easeType", easeType, "onComplete", "Deactivate"));

            timer = 0;
            failsafe = true;
        }

        if (timer >= lifetime && failsafe)
        {
            gameObject.SetActive(false);
        }
    }

    void OnBeat()
    {
        if(active)
        {
            transform.DOScale(120, beatTime).From();
        }
    }

    public void Explode()
    {
        GetComponent<SphereCollider>().enabled = false;
        active = false;

        
        transform.DOScale(400, beatTime);

        for (int i = 0; i < 3; i++)
        {
            //iTween.ColorTo(orbs[i], iTween.Hash("a", 0, "namedcolorvalue", "_TintColor",
            //    "time", beatTime / 2));

            orbs[i].GetComponent<Renderer>().material.DOFade(0, "_TintColor", beatTime / 2);
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }

}
