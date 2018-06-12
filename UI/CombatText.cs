using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    public string text { get; set; }                      
    public Bounds bounds { get; set; }                  

    public Font font { get; set; }
    public Text uiText { get; set; }

    public RectTransform rectT { get; set; }


    public Vector3 velocity { get; set; }
    public Vector3 position { get; set; }
    public float drag { get; set; }

    public float fadeSpeed { get; set; }

    public bool firstFrame { get; set; }
    public bool worldSpace { get; set; }

    public void Awake()
    {
        rectT = (RectTransform)transform;
        uiText = GetComponent<Text>();
    }

    void OnEnable()
    {
        rectT.localScale.Set(1, 1, 1);
    }

}
