using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BossHealth : MonoBehaviour
{

    BaseEnemy boss;

    public GameObject bar;
    public GameObject back;

    Vector3 defaultSize;
    Vector3 defaultPosition;

    bool barActive = false;
    float healthPercent = 1;
    float maxHealth;

    void Start()
    {
        defaultSize = bar.transform.localScale;
        defaultPosition = bar.transform.localPosition;
    }

    public void StartBar(GameObject _boss)
    {
        boss = _boss.GetComponent<BaseEnemy>();

        maxHealth = boss.health;

        bar.SetActive(true);
        back.SetActive(true);

        //Debug.Log(boss.health + " / " + maxHealth);

        barActive = true;
    }

    void Update()
    {
        if (barActive)
        {
            healthPercent = boss.health / maxHealth;

            //DebugText.Instance.SetText("" + healthPercent);


            bar.transform.localScale = new Vector3(healthPercent * (defaultSize.x - 5) + 5,
                defaultSize.y, defaultSize.z);

            bar.transform.localPosition = defaultPosition + new Vector3(
                (bar.transform.localScale.x - defaultSize.x) / -2, 0, 0);

        }
    }
}
