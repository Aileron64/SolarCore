using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AudioVisualizer
{
    /// <summary>
    /// Object scale waveform.
    /// Scale objects along a given axis, to create an audio waveform. Objects are typically arranged in a line or close together.
    /// </summary>
    public class TitleBars : MonoBehaviour
    {
        public int audioSource; // index into audioSampler audioSource array. Determines which audio source we want to sample
        public FrequencyRange frequencyRange; // what frequency will we listen to? 
        public float sensitivity; // how sensitive is this script to the audio. This value is multiplied by the audio sample data.
        public List<GameObject> objects; // objects to scale with the music
        public Vector3 scaleAxis = Vector3.up; // the direction we scale each object.
        public float maxHeight; // the max scale along the scaleAxis. i.e. if scaleAxis is(0,1,0), this is our maxHeight
        public float lerpSpeed; // speed at which we scale, from currentScale to nextScale.
        public bool absoluteVal; // /use absolute value of audio decibal levels

        private List<Vector3> startingScales; // the scale of each object on game start
        private List<Vector3> startingPositions; // the position of each object on game start

        public GameObject box;
        public int boxNum;
        public float width;

        
        // Use this for initialization
        void Start()
        {
            SetUpBars();

            //initialize starting scales and positions
            startingScales = new List<Vector3>();
            startingPositions = new List<Vector3>();

            foreach (GameObject obj in objects)
            {
                startingScales.Add(obj.transform.localScale);
                startingPositions.Add(obj.transform.localPosition);
            }
        }

        void SetUpBars()
        {
            objects.Add(box);
            box.transform.localScale = new Vector3(width / (float)boxNum, 1, 1);

            for (int i = 1; i < boxNum; i++)
            {
                objects.Add(Instantiate(box) as GameObject);
                objects[i].name = "Cube " + i; 
                objects[i].transform.SetParent(gameObject.transform, false);
            }

            float barGap = objects[0].transform.localScale.x * 1.15f;

            for (int i = 0; i < objects.Count; i++)
            {       
                objects[i].transform.localPosition = new Vector3(
                    (i - ((float)objects.Count - 1) / 2) * barGap, 0, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ScaleObjects();
        }

        void ScaleObjects()
        {
            float[] audioSamples;
            if (frequencyRange == FrequencyRange.Decibal)
            {
                audioSamples = AudioSampler.instance.GetAudioSamples(audioSource, objects.Count, absoluteVal);
            }
            else
            {
                audioSamples = AudioSampler.instance.GetFrequencyData(audioSource, frequencyRange, objects.Count, absoluteVal);     
            }

            if (AudioSampler.instance.GetRMS(0) > 0)
            { 
                //for each object
                for (int i = 0; i < objects.Count / 2 + 1; i++)
                {
                    float sampleScale = Mathf.Min(audioSamples[i] * sensitivity, 1); // % of maxHiehgt, via teh audio sample
                    float currentHeight = sampleScale * maxHeight;

                    Vector3 desiredScale = startingScales[i] + currentHeight
                         * scaleAxis; // get desired scale, in correct direction, using audio

                    objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, desiredScale,
                        Time.smoothDeltaTime * lerpSpeed); // lerp from current scale to desired scale

                    objects[objects.Count - i - 1].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, desiredScale,
                        Time.smoothDeltaTime * lerpSpeed); // Mirror
                }
            }
            else
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    Vector3 desiredScale = new Vector3(0, 0, 0); // Set to default if audio is off

                    objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, desiredScale,
                        Time.smoothDeltaTime * lerpSpeed); // lerp from current scale to desired scale
                }
            }

        }
    }
}
