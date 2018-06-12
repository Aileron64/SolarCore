using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;


public class HealthBar : MonoBehaviour 
{
    GameObject player;
    Transform parent;

    public int playerNum;

    Vector3 defaultSize;
    Vector3 defaultPosition;

    float targetSize;
    float playerMaxHealth;
    float currentHealth;

    void Start () 
    {
        defaultSize = transform.localScale;
        defaultPosition = transform.localPosition;
        parent = transform.parent;

        player = PlayerManager.Instance.GetPlayer(playerNum);

        if (player)
            playerMaxHealth = player.GetComponent<Player>().GetMaxHealth();

        currentHealth = playerMaxHealth;
    }

    void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
    }


    void Update () 
    {
        if(player)
        {
            if(currentHealth > player.GetComponent<Player>().GetHealth() &&
                player.GetComponent<Player>().GetHealth() < playerMaxHealth)
            {
                parent.DOLocalMoveZ(-40, 0.1f).From();
                
            }

            // Don't go over max health
            
            currentHealth = player.GetComponent<Player>().GetHealth();


            float healthPercent = currentHealth / playerMaxHealth;

            if(healthPercent <= 1)
                transform.localScale = new Vector3(healthPercent * (defaultSize.x - 5) + 5, 
                    transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3((defaultSize.x - 5) + 5,
                    transform.localScale.y, transform.localScale.z);

            transform.localPosition = defaultPosition + new Vector3(
                (defaultSize.x - transform.localScale.x) / -2, 0, 0);
        }      
    }

    void OnBeat()
    {
        if(currentHealth / playerMaxHealth <= 0.15)
        {
            parent.DOScale(1.3f, 0.1f).From();
        }
        else if (currentHealth / playerMaxHealth <= 0.3)
        {
            parent.DOScale(1.15f, 0.1f).From();
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }
}

//targetSize = player.GetComponent<Player>().GetHealth() / playerMaxHealth * (defaultSize.x - 5) + 5;

//transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, targetSize, Time.smoothDeltaTime * 2), 
//    transform.localScale.y, transform.localScale.z);