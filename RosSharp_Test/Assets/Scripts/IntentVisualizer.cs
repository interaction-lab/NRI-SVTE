using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public abstract class IntentVisualizer : MonoBehaviour
    {
        protected Color[] intentColors;

        
        abstract public void Visualize(NLUDataTypes.NLUIntent[] intentList);

        abstract public void Disable();

        protected void GenerateIntentColors(int length)
        {
            intentColors = ColorUtility.GetDifferentColors(length);
        }

        public Color GetIntentColor(int index)
        {
            return intentColors[index];
        }

       
    }
}
