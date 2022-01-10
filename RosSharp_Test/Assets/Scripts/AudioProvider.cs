using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RosSharp.RosBridgeClient
{
    public abstract class AudioProvider : MonoBehaviour
    {
        // Start is called before the first frame update
        protected AudioVisualizer[] audioVisualizers;
        protected MessageTypes.Std.Float64MultiArray audioRecording = new MessageTypes.Std.Float64MultiArray();
        protected bool IsMessageSet = false;
        protected readonly float loudnessMin = 0;
        protected readonly float loudnessMax = 100;
        abstract protected void SetMessage();

       
    }
}
