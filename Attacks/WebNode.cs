using UnityEngine;
using System.Collections;

public class WebNode : MonoBehaviour 
{
    public GameObject nova;
    public GameObject prevNode;

    GameObject web;

    float power = 5;

    float delay = 3;
    float timer;

    void Start()
    {
        Destroy(this.gameObject, 15);
        timer = delay;

        web = transform.Find("Web").gameObject;

        if(prevNode)
        {
            web.GetComponent<Web>().prevWeb = prevNode.transform;
            web.SetActive(true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (web.activeInHierarchy && !prevNode)
            web.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Laser")
        {
            if (col.GetComponent<BaseAttack>().atkTag == "FlameNova" && timer >= delay)
            {
                GameObject clone = Instantiate(nova, transform.position, transform.rotation) as GameObject;
                clone.GetComponent<BaseAttack>().SetPower(power);
                clone.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                timer = 0;
            }
        }
    }
}
