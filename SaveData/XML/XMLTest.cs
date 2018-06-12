using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLTest : MonoBehaviour 
{
    string x = "10";
    string y = "20";
    string z = "30";

    public TestDatabase data;

    void Start ()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TestDatabase));

        FileStream stream = new FileStream(Application.dataPath + "/XML/test2.xml", FileMode.Create);

        serializer.Serialize(stream, data);
        stream.Close();

    }
}

[System.Serializable]
[XmlRoot("Data")]
public class TestDatabase
{
    [XmlArray("List")]
    [XmlArrayItem("Int")]
    public List<int> list = new List<int>();


}


