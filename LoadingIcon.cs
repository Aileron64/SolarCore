using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingIcon : MonoBehaviour
{
    float time;
    float timer = 0.6f;

    float angle = 0;

    void Update()
    {
        time += Time.deltaTime;

        if(time >= timer)
        {
            angle -= 45;
            transform.DORotate(new Vector3(0, 0, angle), 0.2f);
            time = 0;
        }
    } 

}
