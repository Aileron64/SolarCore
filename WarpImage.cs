using UnityEngine;
using System.Collections;

public class WarpImage : MonoBehaviour
{

    float size = 0;
    float maxSize = 4;

    float timer;
    float backUpTimer = 5;

    public GameObject[] extra;

    void OnEnable()
    {
        size = 0;
        timer = 0;
    }

    public void SetModel(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetModel(GameObject obj)
    {
        if (obj.GetComponent<MeshFilter>())
        {
            GetComponent<MeshFilter>().mesh = obj.GetComponent<MeshFilter>().mesh;
        }

        int x = 0;

        foreach (Transform child in obj.transform)
        {
            if (child.GetComponent<MeshFilter>() && x < extra.Length)
            {
                extra[x].GetComponent<MeshFilter>().mesh = child.GetComponent<MeshFilter>().mesh;
                extra[x].transform.localPosition = child.transform.localPosition;
                extra[x].transform.localRotation = child.transform.localRotation;
                extra[x].SetActive(true);
                x++;
            }
        }

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= backUpTimer)
        {
            this.gameObject.SetActive(false);
        }

        if (size <= maxSize)
        {
            size += Time.deltaTime * 10;
            transform.localScale = new Vector3(size, size, size);
        }
    }


}
