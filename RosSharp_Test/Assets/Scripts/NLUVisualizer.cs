using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public abstract class NLUVisualizer : MonoBehaviour
    {
        abstract public void Visualize(MessageTypes.Std.String nluMessage);

        abstract public void DestroyObjects();

    }
}
