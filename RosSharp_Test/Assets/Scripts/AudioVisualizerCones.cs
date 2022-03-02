using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RosSharp.RosBridgeClient
{
    public class AudioVisualizerCones : AudioVisualizer
    {
        private GameObject conePrefab;
        private GameObject[] audioCones;
        private bool IsCreated = false;
        //Radius of the cirle around kuri on which the end of the cones are located
        private readonly float circleRadius = 0.25f;
        private float coneScale = 0.15f;
        private readonly int coneNumber = 16;
        public bool IsThreeDimensional = true;
        public float inflatingCoefficient = 2.0f;
        protected override void Create()
        {
            conePrefab = Resources.Load<GameObject>(ResourcePathManager.conePath);
            //Adjusting scale of the cones by coneNumber
            float tempConeScale = coneScale;
            tempConeScale /= (coneNumber/4);
            audioCones = new GameObject[coneNumber];
            int threeDimensionalFlag = 0;
            if (IsThreeDimensional)
                threeDimensionalFlag = 1;
            for (int i = 0; i < coneNumber; i++)
            {
                
                //Calculating cone position on the circle around Kuri and its rotation on the y axis
                float conePosition = (float)i / (float)coneNumber;
                float rotationY = (float)i * (360 / coneNumber);
                float x = Mathf.Sin(conePosition * Mathf.PI * 2.0f + Mathf.PI/4) * circleRadius;
                float z = Mathf.Cos(conePosition * Mathf.PI * 2.0f + Mathf.PI /4) * circleRadius;
                audioCones[i] = Instantiate(conePrefab, new Vector3(x, 0.1f, z), Quaternion.Euler(90,rotationY + 180,0 - 45)) as GameObject;
                audioCones[i].transform.parent = GameObject.Find("Microphones").transform;
                audioCones[i].transform.localPosition =
                    new Vector3(audioCones[i].transform.position.x,
                    audioCones[i].transform.position.y, audioCones[i].transform.position.z);
                audioCones[i].transform.localScale = new Vector3(tempConeScale, tempConeScale, tempConeScale * threeDimensionalFlag);
                if(hiddenObjects)
                    audioCones[i].SetActive(false);
                audioCones[i].GetComponent<ConeMesh>().SetInflatingCoefficient(inflatingCoefficient);
            }
            IsCreated = true;
        }
        public override void Visualize(MessageTypes.Std.Float64MultiArray audioRecording)
        {
            float coneLoudness;
            if (audioCones != null && IsCreated == true)
            {
                for (int i = 0; i < audioCones.Length; i++)
                {
                    //Getting loudness heard by cone i
                    coneLoudness = GetObjectLoudness(GetLoudness(audioRecording), i, coneNumber);
                    if (hiddenObjects && coneLoudness > 0)
                    {
                        audioCones[i].SetActive(true);
                        audioCones[i].GetComponent<ConeMesh>().SetColor(GetInterpolatedColor(highColor, lowColor, coneLoudness));
                        audioCones[i].GetComponent<ConeMesh>().ChangeRadius(coneLoudness);
                    }
                    else if (hiddenObjects && coneLoudness == 0)
                        audioCones[i].SetActive(false);
                }
            }
        }


        protected override void DestroyObjects()
        {
            if (audioCones != null)
            {
                for (int i = 0; i < audioCones.Length; i++)
                {
                    Destroy(audioCones[i]);
                }
            }
            IsCreated = false;
        }

        private void Start()
        {
            //Creating microphone cones
            if (IsCreated == false)
            {
                Create();
                IsCreated = true;
            }
        }

    }
}
