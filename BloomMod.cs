using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioVisualizer;

public class BloomMod : MonoBehaviour
{
    SENaturalBloomAndDirtyLens bloom;
    public float levelMod = 0;

    void Start ()
    {
        bloom = GetComponent<SENaturalBloomAndDirtyLens>();

        if(levelMod == 0)
            levelMod = BaseLevel.Instance.bloomMod;
	}
	
	void Update ()
    {
        bloom.bloomIntensity = AudioSampler.instance.GetRMS(0) * (0.4f + levelMod) + 0.05f;
    }
}
