using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class TextManager : MonoBehaviour
{
    public Font damageFont;
    public Font scoreFont;
    public Font popupFont;
    public Font weaponFont;

    public Color damageColor;
    public Color scoreColor;
    public Color[] weaponColor;


    //public float hideDistance;          //At what distance will the label not be drawn?
    public float maxViewAngle;          //What is the maximum angle the labels should be drawn at?
    public float floatingSpeed;         //How fast the label should move
    //public float scaleFactor;           //How big the label should be
    //public float outlineSize;           //The size of the outline
    public float distanceScaleFactor;   //Scale size of text according to distance to the target

    //public bool outlined;               //Should the label be outlined?
    int textPoolCount = 300;           //How many text object should there maximum be at a time?

    public GameObject textPrefab;
    public Camera cam { get; set; }                 //Reference to the camera
    public Canvas canvas { get; set; }
    public List<CombatText> texts { get; set; }

    static TextManager instance;

    int debugCount;

    float screenScale;

    public static TextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TextManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<TextManager>();
                }
            }
            return instance;
        }
    }

    private RectTransform canvasRectT;

    void Start()
    {
        texts = new List<CombatText>();
        cam = Camera.main;
        canvas = GetComponent<Canvas>();
        canvasRectT = canvas.GetComponent<RectTransform>();

        for (int i = 0; i < textPoolCount; i++)
        {
            CombatText text = Instantiate(textPrefab).GetComponent<CombatText>();
            text.gameObject.SetActive(false);
            text.transform.SetParent(canvas.transform);
            //text.hideFlags = HideFlags.HideInHierarchy;
            texts.Add(text);     
        }

        screenScale = GUIManager.Instance.gameObject.GetComponent<Canvas>().scaleFactor;//canvas.scaleFactor;
        //Debug.Log("Screen Size = " + screenScale);
    }

    void LateUpdate()
    {
        //DebugText.Instance.SetText("" + texts.Count);

        for (int i = 0; i < texts.Count; i++)
        {
            if (!texts[i].gameObject.activeSelf)
            {
                continue;
            }

            //If the alpha channel of the color reaches 0 then remove the label from the list of labels
            if (texts[i].uiText.color.a <= 0)
            {
                texts[i].gameObject.SetActive(false);
                //Skip this iteration in the loop
                continue;
            }

            texts[i].velocity *= texts[i].drag;
            texts[i].position += texts[i].velocity * Time.deltaTime;

            if (texts[i].worldSpace)
            {
                //Calculate the position of the transform in screen space
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, texts[i].position);

                //Position the label
                texts[i].rectT.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;// + texts[i].offset;
                
            }
            else
            {
                texts[i].rectT.anchoredPosition = texts[i].position;
            }


            //Make the label fade
            texts[i].uiText.color -= new Color(0, 0, 0, Time.deltaTime * texts[i].fadeSpeed / 100);
        }
    }


    public void DamageText(int value, Vector3 damagePos, Bounds bounds)
    {
        CombatText text = GetText();


        text.position = bounds.ClosestPoint(damagePos);
        text.velocity = (bounds.center - damagePos).normalized * 300;
        text.drag = 0.9f;

        text.transform.localPosition = Vector3.zero;

        text.uiText.color = damageColor;
        text.bounds = bounds;

        text.worldSpace = true;
        text.uiText.text = "" + value;
        
        text.uiText.font = damageFont;

        if(value < 300)
        {
            text.fadeSpeed = 100;
            text.uiText.fontSize = (int)(30 * screenScale);
        }
        else
        {
            text.fadeSpeed = 60;
            text.uiText.fontSize = (int)(40 * screenScale);
        }
    }

    public void ScoreText(int value, Bounds bounds, float size)
    {
        CombatText text = GetText();

        text.velocity = Vector3.up * 400;
        text.drag = 0.9f;

        text.position = bounds.center;
        text.fadeSpeed = 50;
        text.transform.localPosition = Vector3.zero;

        text.uiText.color = scoreColor;
        text.bounds = bounds;

        text.worldSpace = true;
        text.uiText.text = "" + value;
        text.uiText.fontSize = (int)(size * screenScale);
        text.uiText.font = scoreFont;
    }

    public void WeaponText(string _text, Bounds bounds, bool upgrade)
    {
        CombatText text = GetText();

        text.velocity = Vector3.up * 4000;
        text.drag = 0.95f;

        text.position = bounds.center;
        text.fadeSpeed = 50;
        text.transform.localPosition = Vector3.zero;

        if(upgrade)
        {
            text.uiText.color = weaponColor[0];
            text.velocity = Vector3.up * 1000;
        }
        else
        {
            text.uiText.color = weaponColor[1];
            text.velocity = Vector3.up * -1000;
        }

        
        text.bounds = bounds;

        text.worldSpace = true;
        text.uiText.text = _text;
        text.uiText.fontSize = (int)(40 * screenScale);
        text.uiText.font = weaponFont;
    }

    public void PopupText(string _text, Vector2 pos, float size)
    {
        CombatText text = GetText();

        text.velocity = Vector3.up * 100;
        text.drag = 0.9f;

        text.position = pos;
        text.fadeSpeed = 50;
        text.transform.localPosition = Vector3.zero;

        text.uiText.color = scoreColor;

        text.worldSpace = false;
        text.uiText.text = _text;

        text.uiText.fontSize = (int)(size * screenScale);
        text.uiText.font = popupFont;

    }

    public void DebugText(string _text)
    {
        CombatText text = GetText();

        text.velocity = Vector3.zero;       

        debugCount++;

        if (debugCount > 12)
            debugCount = 0;

        text.position = new Vector2(-800, -340 + debugCount * 50);
        text.fadeSpeed = 40;
        text.transform.localPosition = Vector3.zero;

        text.uiText.color = scoreColor;

        text.worldSpace = false;
        text.uiText.text = _text;

        text.uiText.fontSize = (int)(40 * screenScale);
        text.uiText.font = damageFont;

    }

    public CombatText GetText()
    {
        foreach (CombatText cText in texts)
        {
            if (!cText.gameObject.activeSelf)
            {

                cText.gameObject.SetActive(true);
                return cText;
            }
        }

        CombatText text = Instantiate(textPrefab).GetComponent<CombatText>();
        text.transform.SetParent(canvas.transform);
        text.hideFlags = HideFlags.HideInHierarchy;
        texts.Add(text);

        Debug.Log("Text Pool increased to " + texts.Count);
        
        
        //texts.Add(text);

        return text;
    }

}
