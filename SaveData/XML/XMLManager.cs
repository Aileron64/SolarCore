using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    void Awake()
    {
        instance = this;
    }



    public ItemDatabase itemDB;

    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));

        FileStream stream = new FileStream(Application.dataPath + "/XML/test2.xml", FileMode.Create);

        serializer.Serialize(stream, itemDB);
        stream.Close();

    }

}

[System.Serializable]
public class ItemEntry
{
    public string itemName;
    public Mat material;
    public int value;

}

[System.Serializable]
public class ItemDatabase
{
    public List<ItemEntry> list = new List<ItemEntry>();



}

public enum Mat
{
    Wood, Copper, Iron, Steel, Obsidian
}

