using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LaserBar : MonoBehaviour 
{
    Player player;
    RawImage bar;
    Text text;
    Text moarText;

    float barPercent;
    float level;

    int playerNum;

	void Start () 
    {
        playerNum = transform.parent.gameObject.GetComponent<HealthBar>().playerNum;
        SetPlayer(playerNum - 1);

        bar = transform.Find("Bar").GetComponent<RawImage>();
        text = transform.Find("Text").GetComponent<Text>();
	}

    public void SetPlayer(int _playerNum)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[_playerNum].GetComponent<Player>();
    }

	void Update () 
    {
        level = player.GetWeaponLevel();

        if (player.GetWeaponLevel() != 2)
        {
            text.text = string.Format("Lv {0}", player.GetWeaponLevel() + 1);
            //barPercent = (player.weaponExp % player.expCap) / player.expCap;
            bar.transform.localScale = new Vector3(barPercent, 1, 1);
        }
        else
        {
            text.text = "Max";
            bar.transform.localScale = new Vector3(1, 1, 1);
        }          
	}
}
