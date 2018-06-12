using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxCol : MonoBehaviour
{
    BoxCollider col;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (col)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero + col.center, col.size);
        }
        
    }

}
