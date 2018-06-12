using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class ShipInfo : MonoBehaviour 
{

    [XmlAttribute("name")]
    XmlDocument shipXML;

    public Text shipName;
    public Text[] skillName;
    public Text[] info;
    public Image[] icon;


    static ShipInfo instance;
    public static ShipInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShipInfo>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ShipInfo>();
                }
            }
            return instance;
        }
    }


    void Start()
    {
        SwitchShip(1, "Blue");

    }


    public void SwitchShip(int num, string name)
    {
        for (int i = 0; i < 2; i++)
        {
            if(icon[i])
                icon[i].sprite = Resources.Load<Sprite>("Icons/Skill_Icons/" + name + "_" + i);

            //Debug.Log("Icons/Skill_Icons/" + name + "_" + i);
        }

        if (icon[0])
            LazyTextSwap(num);
    }


    public void LazyTextSwap(int x)
    {
        switch(x)
        {
            default:
            case 1: // Blue
                shipName.text = "Blue";

                info[0].text = "Standard ship, shoots lasers";

                skillName[0].text = "Power Shot";
                info[1].text = "Charged laser that pierces through enemies";
                    //+ "\n - Size/damage changes based on how long the button was held down";

                skillName[1].text = "Stun Nova";
                info[2].text = "Charged nova that damages and stuns enemeies" 
                    + "\n More effective based on proximity of targets";
                break;

            case 2: // White
                shipName.text = "White";

                info[0].text = "Controls the radius of circleing orbs which deal damage"
                    + "\n Moves faster based on proximity of orbs";

                skillName[0].text = "Orb Cannons";
                info[1].text = "Fire lasers from each orb";

                skillName[1].text = "Orb Lasers";
                info[2].text = "Channel laser beams to each orb";
                break;

            case 3: // Purple
                shipName.text = "Purple";

                info[0].text = "Creates black holes that grow in size on contact with enemies";

                skillName[0].text = "Black Hole";
                info[1].text = "Fire a missle that creates a black hole on contact";

                skillName[1].text = "Chronosphere";
                info[2].text = "Create large sphere that slows down enemies and their projectiles";
                break;

            case 4: // Cyan
                shipName.text = "Cyan";

                info[0].text = "Switches between 3 differnt weapon modes";

                skillName[0].text = "Big F-ing Laser";
                info[1].text = "Giant laser beam, long recharge";

                skillName[1].text = "Rearm";
                info[2].text = "Switch weapon mode and instantly recharge your laser";
                break;

            case 5: // Green
                shipName.text = "Green";

                info[0].text = "Places bombs and push them around with its shot gun";

                skillName[0].text = "Bomb";
                info[1].text = "Explodes, 5 charges";

                skillName[1].text = "Detonate";
                info[2].text = "Detonate all bombs";
                break;

            case 6: // Turq
                shipName.text = "Turq";

                info[0].text = "Firing lasers creates recoil that can be used to move around";

                skillName[0].text = "Space Breaks";
                info[1].text = "Reduce the recoil from shooting";

                skillName[1].text = "Bubble Sheild";
                info[2].text = "Encases ship in bubble that sheilds it from all damage"
                    + "\n Deals mass damage to enemies on contact";
                break;
        }




    }

}
