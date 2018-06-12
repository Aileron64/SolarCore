using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapQuickButton : MonoBehaviour
{
    public GameObject mapNode;
    public Color completedColor;

    bool active = true;

    public float defaultHeight;

    private void Awake()
    {
        defaultHeight = GetComponent<Button3D>().model.localScale.x;
        mapNode.GetComponent<MapPoint>().quickButton = this;
        GetComponent<Button>().onClick.AddListener(() => { OnClick(); });
    }

    public void Completed()
    {
        GetComponent<Button3D>().model.GetComponent<Renderer>().material.color = completedColor;
    }

    public void Deactive()
    {
        active = false;
        GetComponent<Button3D>().model.localScale = new Vector3(GetComponent<Button3D>().model.localScale.x, 3, 25);
        GetComponent<Button3D>().active = false;
    }




    void OnClick()
    {
        if(active)
            MapControls.Instance.NewTarget(mapNode);
    }

}
