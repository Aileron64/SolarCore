using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Player player;
    public int playerNum;

    public GameObject bar1;
    public GameObject bar2;

    Vector3 defaultSize;
    Vector3 defaultPosition1;  
    Vector3 defaultPosition2;

    float energyPercent;

    void Start()
    {
        player = PlayerManager.Instance.GetPlayer(playerNum).GetComponent<Player>();

        if(!player)
            gameObject.SetActive(false);

        defaultSize = bar1.transform.localScale;
        defaultPosition1 = bar1.transform.localPosition;
        defaultPosition2 = bar2.transform.localPosition;
    }


    void Update()
    {
        energyPercent = player.GetComponent<Player>().GetEnergy() / 50;

        if (energyPercent >= 2)
        {
            bar2.transform.localScale = new Vector3((defaultSize.x - 5) + 5,
                defaultSize.y, defaultSize.z);
        }
        else if (energyPercent >= 1)
        {
            bar1.transform.localScale = new Vector3((defaultSize.x - 5) + 5,
                defaultSize.y, defaultSize.z);  

            bar2.transform.localScale = new Vector3((energyPercent - 1) * (defaultSize.x - 5) + 5,
                defaultSize.y, defaultSize.z);
        }
        else if (energyPercent > 0)
        {
            bar1.transform.localScale = new Vector3(energyPercent * (defaultSize.x - 5) + 5,
                defaultSize.y, defaultSize.z);

            bar2.transform.localScale = Vector3.zero;
        }
        else
        {
            bar1.transform.localScale = Vector3.zero;
            bar2.transform.localScale = Vector3.zero;
        }

        bar1.transform.localPosition = defaultPosition1 + new Vector3(
                (defaultSize.x - bar1.transform.localScale.x) / -2, 0, 0);

        bar2.transform.localPosition = defaultPosition2 + new Vector3(
                (defaultSize.x - bar2.transform.localScale.x) / -2, 0, 0);
    }
}
