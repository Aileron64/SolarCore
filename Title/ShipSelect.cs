using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipSelect : MonoBehaviour 
{
    GameObject blueButton;
    GameObject whiteButton;
    GameObject orangeButton;
    GameObject purpleButton;
    GameObject cyanButton;
    GameObject greenButton;

    GameObject selectBorder;

    public int playerNum;

    public bool debugMenu;
    

    void Start()
    {       // Character Select 
        blueButton = transform.Find("Blue").gameObject;
        blueButton.GetComponent<Button>().onClick.AddListener(() => { Select(1, blueButton); });

        whiteButton = transform.Find("White").gameObject;
        whiteButton.GetComponent<Button>().onClick.AddListener(() => { Select(2, whiteButton); });

        orangeButton = transform.Find("Green").gameObject;
        orangeButton.GetComponent<Button>().onClick.AddListener(() => { Select(5, orangeButton); });

        purpleButton = transform.Find("Purple").gameObject;
        purpleButton.GetComponent<Button>().onClick.AddListener(() => { Select(3, purpleButton); });

        cyanButton = transform.Find("Cyan").gameObject;
        cyanButton.GetComponent<Button>().onClick.AddListener(() => { Select(4, cyanButton); });

        greenButton = transform.Find("Turq").gameObject;
        greenButton.GetComponent<Button>().onClick.AddListener(() => { Select(6, greenButton); });

        selectBorder = transform.Find("Select").gameObject;
        //PlayerMan.shipNum[playerNum] = 1;

        switch(PlayerManager.shipNum[playerNum])
        {
            default:
            case 1:
                Select(1, blueButton);
                break;

            case 2:
                Select(2, whiteButton);
                break;

            case 3:
                Select(3, purpleButton);
                break;

            case 4:
                Select(4, cyanButton);
                break;

            case 5:
                Select(5, orangeButton);
                break;

            case 6:
                Select(6, greenButton);
                break;
        }

        // God I wish I knew about this sooner
        //shipInfo = Object.FindObjectOfType<ShipInfo>();

        PlayerManager.titleInput = true;
    }

    
    void Select(int num, GameObject button)
    {
        selectBorder.transform.localPosition = button.transform.localPosition;
        PlayerManager.shipNum[playerNum] = num;

        if(!debugMenu)
            ShipInfo.Instance.SwitchShip(num, button.name);  
    }
}
