using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AudioVisualizer
{
    /// <summary>
    /// Object scale waveform.
    /// Scale objects along a given axis, to create an audio waveform. Objects are typically arranged in a line or close together.
    /// </summary>
    public class MusicBars : MonoBehaviour
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

        public bool createHalfCircle;
        public bool rightSide;
        public float radius;

        // Use this for initialization
        void Start()
        {
            if(createHalfCircle)
                CreateHalfCircle();

            //initialize starting scales and positions
            startingScales = new List<Vector3>();
            startingPositions = new List<Vector3>(); 

            foreach (GameObject obj in objects)
            {
                startingScales.Add(obj.transform.localScale);
                startingPositions.Add(obj.transform.localPosition);
            }
        }

        void CreateHalfCircle()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                float angle;

                if(rightSide)
                    angle = i * (3.14f / 2) * 2 / (objects.Count - 0.5f);
                else
                    angle = i * (3.14f / 2) * 2 / (objects.Count - 0.5f) * -1;

                objects[i].transform.localPosition = new Vector3(
                    Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);

                objects[i].transform.LookAt(gameObject.transform);
            }
        }

        //Mathf.Sin(i* 3.14f * 2 / objects.Count) * radius, 0, Mathf.Cos(i* 3.14f * 2 / objects.Count) * radius);


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
                for (int i = 0; i < objects.Count; i++)
                {
                    float sampleScale = Mathf.Min(audioSamples[i] * sensitivity, 1); // % of maxHiehgt, via teh audio sample
                    float currentHeight = sampleScale * maxHeight;

                    Vector3 desiredScale = startingScales[i] + currentHeight * transform.InverseTransformDirection(scaleAxis); // get desired scale, in correct direction, using audio
                    objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, desiredScale, Time.smoothDeltaTime * lerpSpeed); // lerp from current scale to desired scale

                    //reposition the object after scaling, so that it's seemingly in the same place.
                    float distanceScaled = (objects[i].transform.localScale - startingScales[i]).z; // get change in scale


                    Vector3 direction = objects[i].transform.TransformDirection(scaleAxis); // get movement direction, relative to object


                    objects[i].transform.localPosition = startingPositions[i] + distanceScaled * direction * .5f; // move the object

                }
            }
            else
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    Vector3 desiredScale = new Vector3(20, 20, 20); // Set to default if audio is off

                    objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, desiredScale,
                        Time.smoothDeltaTime * lerpSpeed); // lerp from current scale to desired scale
                }
            }
        }
    }
}
