using System;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public abstract class AudioVisualizer : MonoBehaviour
    {
        protected double loudnessMin=0;
        protected double loudnessMax=100;
        protected float alpha = 0.25f;
        protected bool hiddenObjects = true;
        protected Color lowColor = new Color((float)247/255,(float)252/255,(float)245/255);
        protected Color highColor = new Color((float)0 / 255, (float)68 / 255, (float)27 / 255);
        private bool divergingColorPalette = false;

        abstract public void Visualize(MessageTypes.Std.Float64MultiArray audioRecording);

        abstract protected void Create();
        abstract protected void DestroyObjects();

        //AudioRecording data be an array of floats of dimension 4.
        //Being the norm of the vector recorded for each microphone
        protected double[] GetLoudness(MessageTypes.Std.Float64MultiArray audioRecording)
        {
            int dimension = (int)audioRecording.layout.dim[0].size;
            double[] loudness = new double[4];

            if (dimension == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    loudness[i] = (double)audioRecording.data[i];
                }
                return loudness;
            }
            throw new Exception("AudioRecording array has dimension different than 4: " + dimension);
        }
        
        private float GetPercentualLoudness(double loudness)
        {
            float percentualLoudness = (float)((loudness - loudnessMin) / (loudnessMax - loudnessMin));
            return percentualLoudness;
        }

        //Gets a color interpolated between first and second color based on the percentage. If percentage is equal to 1, 
        //The color displayed will be the first otherwise if percentage is equal to 0 the color displayed will be the second
        protected Color GetInterpolatedColor(Color first, Color second, float percentage)
        {
            Color newColor; 
            if (divergingColorPalette) {
                if(percentage >= 0.5)
                    newColor = GetAverageColor(first,Color.white,percentage);
                else
                    newColor = GetAverageColor(Color.white,second,percentage);
            }
            else {
                newColor = GetAverageColor(first,second,percentage);
            }
            return newColor;
        }

        private Color GetAverageColor(Color first, Color second, float percentage) {
            float r = first.r * (percentage) + second.r * (1 - percentage);
            float g = first.g * (percentage) + second.g * (1 - percentage);
            float b = first.b * (percentage) + second.b * (1 - percentage);
            float a = alpha;
            return new Color(r, g, b, a);
    }

        //Gets the loudness heard by each object, given that objects are placed on a circle around Kuri.
        protected float GetObjectLoudness(double[] loudness, int objectIndex, int objectNumber)
        {
            int microphoneIdx;
            microphoneIdx = GetMicrophoneIndex(objectIndex, objectNumber);
            float sphereLoudness;
            if (microphoneIdx != -1)
            {
                //The object is placed directly on a microphone, the loudness heard by that object only depends on that
                //microphone
                sphereLoudness = GetPercentualLoudness(loudness[microphoneIdx]);
            }
            else
            {
                //The loudness heard by the object is a weighted average of the loudness heard by the two nearest microphonses
                sphereLoudness = GetPercentualLoudness(GetAverageLoudness(objectIndex, loudness, objectNumber));
            }
            return sphereLoudness;
        }

        //Given the index of the object, the number of objects and the loudness: calculates the weighted average
        //between the loudnesses heard by the nearest microphones
        private double GetAverageLoudness(int objectIdx, double[] loudness, int objectNumber)
        {

            int microphoneBeforeIdx = (int)(Math.Floor((double)((float)objectIdx / (objectNumber / 4)))) % 4;
            int microphoneAfterIdx = (int)(Math.Ceiling((double)((float)objectIdx / (objectNumber / 4)))) % 4;
            int microphoneBeforeConeIdx = microphoneBeforeIdx * (objectNumber / 4);
            int microphoneAfterConeIdx = microphoneAfterIdx != 0 ? microphoneAfterIdx * (objectNumber / 4) : objectNumber;
            return (((objectNumber / 4) - Math.Abs(objectIdx - microphoneBeforeConeIdx))
                    * loudness[microphoneBeforeIdx] +
                ((objectNumber / 4) - Math.Abs(microphoneAfterConeIdx - objectIdx))
                * loudness[microphoneAfterIdx]) / (objectNumber / 4);
        }

        //Given the index of the object and the number of objects. If the object is directly placed on a microphone it returns
        //its index, otherwise it returns -1
        private int GetMicrophoneIndex(int objectIndex, int objectNumber)
        {
            if (objectNumber >= 4 && objectNumber % 4 == 0 && objectIndex % (objectNumber / 4) == 0)
                return objectIndex / (objectNumber / 4);
            return -1;
        }

        protected void OnDisable()
        {
            DestroyObjects();
        }

        protected void OnEnable()
        {
            Create();
        }

    }
}
