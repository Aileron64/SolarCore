using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : BaseAttack
{


    void OnBeat()
    {
        if (transform.position.y < -500)
            transform.DOMoveY(transform.position.y + 450, 0.1f);
        else
        {
            GetComponent<BoxCollider>().enabled = false;


            GetComponent<Renderer>().material.SetFloat("_Mode", 3);


            GetComponent<Renderer>().material.SetFloat("_Mode", 2);
            GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            GetComponent<Renderer>().material.SetInt("_ZWrite", 0);
            GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
            GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
            GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            GetComponent<Renderer>().material.renderQueue = 3000;





            GetComponent<Renderer>().material.DOFade(0, 0.4f).OnComplete(OnEnd);
            transform.DOScale(40, 0.4f);
        }
            
    }

    protected override void OnEnable()
    {
        BaseLevel.OnBeat += OnBeat;
        //GetComponent<Renderer>().material.DOFade(0.6f, 0.4f).OnComplete(OnEnd);
    }

    protected void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }

    protected override void OnEnd()
    {
        Destroy(this.gameObject);
    }
}
