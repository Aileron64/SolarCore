using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphereCol : MonoBehaviour
{
    SphereCollider col;

    void Awake()
    {
        col = GetComponent<SphereCollider>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (col)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(col.center, col.radius);
        }

    }

}
