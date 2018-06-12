using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReturnArrow : MonoBehaviour
{
    public GameObject arrow;
    GameObject player;
    SolarRings solarRings;
    bool active = false;
    Transform core;

	void Start ()
    {
        player = PlayerManager.Instance.GetPlayer(0);
        solarRings = Object.FindObjectOfType<SolarRings>();
        core = GameObject.FindWithTag("Core").transform;
    }

    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
    }

    void Update ()
    {
        if (!active)
        {
            if(solarRings.GetSolarValue(player.transform.position) == 0)
            {
                RotateArrow();
                active = true;               
                arrow.SetActive(true);
                //arrow.GetComponent<Image>().DOFade(100, 0.2f);
            }
        }
        else
        {
            RotateArrow();

            if (solarRings.GetSolarValue(player.transform.position) != 0)
            {
                active = false;
                arrow.SetActive(false);
                //arrow.GetComponent<Image>().DOFade(0, 0.2f);
            }
        }
	}

    void RotateArrow()
    {
        arrow.transform.rotation = Quaternion.Euler(0, 0,
            (Mathf.Atan2((player.transform.position.x - core.transform.position.x) * -1,
            player.transform.position.z - core.transform.position.z) * Mathf.Rad2Deg));
    }

    void OnBeat()
    {
        if(active)
        {
            arrow.transform.DOScale(3.4f, 0.1f).From();
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }
}
