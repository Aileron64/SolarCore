using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RedLaser : BaseAttack
{  
    public ParticleSystem explosion_prefab;
    ParticleSystem explosion;

    float size;
    float pulseScale = 1.5f;

    Transform core;

    protected override void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer
            && !Application.isEditor)
            transform.GetChild(0).gameObject.SetActive(false);

        base.Awake();

        explosion = Instantiate(explosion_prefab);
        explosion.gameObject.SetActive(false);

        core = GameObject.FindWithTag("Core").transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        timeDilation = 1;
        BaseLevel.OnBeat += OnBeat;
    }

    public void SetSize(float _size)
    {
        size = _size;
        transform.localScale = new Vector3(size, size, size);
    }

    void OnBeat()
    {
        transform.DOScale(
            new Vector3(size * pulseScale, size * pulseScale, size * pulseScale), 0.1f).From();

        if ((transform.position - core.position).magnitude >= 2500)
            OnEnd();
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
        }

        if (col.gameObject.tag == "Player")
        {
            OnEnd();
        }
      
        if (col.gameObject.tag == "Block")
        {
            OnEnd();
        }

    }


    void OnTriggerExit(Collider col)
    {
        if (side == Team.RED)
        {
            if (col.gameObject.tag == "Chrono")
            {
                timeDilation = 1;
            }
        }
    }

    protected override void OnEnd()
    {
        if (explosion)
        {
            explosion.startSize = size * 1.4f;
            explosion.transform.position = transform.position;
            explosion.gameObject.SetActive(true);
            explosion.Play();   
        }

        base.OnEnd();
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }

}
