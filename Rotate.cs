using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    public float x;
    public float y;
    public float z;

	void Update () 
    {
        transform.Rotate(new Vector3(x, y, z) * Time.timeScale);
	}
}
