using UnityEngine;
using System.Collections;


public class PlayerManager : MonoBehaviour 
{
    //GameMan GM;
    GameObject[] players = new GameObject[2];

    

    public static int[] shipNum = new int[2];
    public static bool coopActive = false;
    public static bool titleInput = false;

    public bool godMod;

    int playerCount;

    public enum ShipType
    {
        BLUE, WHITE, GREEN, PURPLE, CYAN, TURQ, TEAL, EMPTY
    }

    public ShipType player1Ship;
    public ShipType player2Ship;
    public bool active;
    public bool coop;

    //static GUIMan guiManager;


    GameObject blue;
    GameObject white;
    GameObject green;
    GameObject purple;
    GameObject cyan;
    GameObject turq;
    GameObject teal;
    GameObject empty;


    static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<PlayerManager>();
                }
            }
            return instance;
        }
    }

    public GameObject GetPlayer(int x)
    {
        if (active)
            return players[x];
        else
            return FindObjectOfType<Player>().gameObject;

    }

    void Awake()
    {
        string playerFolder = "Players/";
        blue = Resources.Load(playerFolder + "Blue") as GameObject;
        white = Resources.Load(playerFolder + "White") as GameObject;
        purple = Resources.Load(playerFolder + "Purple") as GameObject;
        cyan = Resources.Load(playerFolder + "Cyan") as GameObject;
        green = Resources.Load(playerFolder + "Green") as GameObject;
        turq = Resources.Load(playerFolder + "Turq") as GameObject;
        teal = Resources.Load(playerFolder + "Teal") as GameObject;
        empty = Resources.Load(playerFolder + "Empty") as GameObject;

        if (titleInput)
        {
            coop = coopActive;
            player1Ship = TitleShipInput(0);

            if(coop)
                player2Ship = TitleShipInput(1);
        }
         
        if(active)
        {
            SpawnPlayer(player1Ship, 1);

            if (coop)
            {
                SpawnPlayer(player2Ship, 2);
                coopActive = true;
            }
        }

        if(godMod && Application.isEditor)
            GodMode.active = true;
    }

    private void Start()
    {

        if (GodMode.active)
            DebugText.Instance.SetText("God Mode");
    }

    ShipType TitleShipInput(int _num)
    {
        // Uses a static int to get choise from title screen
        switch (shipNum[_num])
        {   
            default:
            case 1:
                return ShipType.BLUE;

            case 2:
                return ShipType.WHITE;

            case 3:
                return ShipType.PURPLE;

            case 4:
                return ShipType.CYAN;

            case 5:
                return ShipType.GREEN;

            case 6:
                return ShipType.TURQ;

            case 7:
                return ShipType.TEAL;

            case 8:
                return ShipType.EMPTY;
        }
    }

    void SpawnPlayer(ShipType ship, int playerNum)
    {
        Vector3 offset = new Vector3(0, 0, (playerNum - 1) * -100);

        switch (ship)
        {
            default:
                Debug.Log("No Ship");
                break;

            case ShipType.BLUE:
                players[playerNum - 1] = Instantiate(blue, transform.position + offset,
                    transform.rotation) as GameObject;            
                break;

            case ShipType.WHITE:
                players[playerNum - 1] = Instantiate(white, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.GREEN:
                players[playerNum - 1] = Instantiate(green, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.PURPLE:
                players[playerNum - 1] = Instantiate(purple, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.CYAN:
                players[playerNum - 1] = Instantiate(cyan, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.TURQ:
                players[playerNum - 1] = Instantiate(turq, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.TEAL:
                players[playerNum - 1] = Instantiate(teal, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

            case ShipType.EMPTY:
                players[playerNum - 1] = Instantiate(empty, transform.position + offset,
                    transform.rotation) as GameObject;
                break;

        }

        playerCount++;

        players[playerNum - 1].GetComponent<Player>().playerNum = playerNum;
    }

    public void PlayerAmount(int x)
    {
        playerCount += x;

        if(playerCount <= 0)
        {
            BaseLevel.Instance.GameOver();
        }
    }

}
