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

            // intentColors = new Color[2];
            // intentColors[0] = new Color(175, 175, 0, 255);
            // intentColors[1] = new Color(50, 200, 200, 255);

        }

        public Color GetIntentColor(int index)
        {
            return intentColors[index];
        }


    }
}
