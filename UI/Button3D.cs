using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Button3D : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IEventSystemHandler,
    ISelectHandler,
    IDeselectHandler,
    IPointerDownHandler
{
    public bool active = true;
    public Transform model;
    Vector3 defaultScale;
    Vector3 defaultPos;

    void Awake()
    {
        defaultScale = model.localScale;
        defaultPos = model.localPosition;
    }

    void OnEnable()
    {
        if (model)
            model.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        if(model)
            model.gameObject.SetActive(false);
    }

    void Start()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, model.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(active)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);

            model.DOLocalMoveZ(model.localPosition.z - 100, 0.1f).From();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(active)
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);


        //model.DOScale(defaultScale * 1.3f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (active)
        {

            //model.DOLocalMove(defaultPos, 0.1f);

            if (EventSystem.current.currentSelectedGameObject == this.gameObject)
                EventSystem.current.SetSelectedGameObject(null);
        }

    }

    public void OnSelect(BaseEventData eventData)
    {
        if(active)
            model.DOScale(defaultScale * 1.5f, 0.1f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if(active)
            model.DOScale(defaultScale, 0.1f);
    }
}
