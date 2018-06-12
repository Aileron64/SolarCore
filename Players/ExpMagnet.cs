using UnityEngine;
using System.Collections;

public class ExpMagnet : MonoBehaviour 
{

 
     public void FixedUpdate() 
     {
         float pullRadius = 300;
         float pullForce = 1000;

         foreach (Collider col in Physics.OverlapSphere(transform.position, pullRadius))
         {
             if (col.gameObject.tag == "Exp")
             {
                 Vector3 forceDirection = transform.position - col.transform.position;

                 col.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce);
             }
         }
    }
}
