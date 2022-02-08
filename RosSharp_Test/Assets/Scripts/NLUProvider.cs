using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RosSharp.RosBridgeClient
{
    public abstract class NLUProvider : MonoBehaviour
    {
        // Start is called before the first frame update
        protected NLUVisualizer[] nluVisualizers;
        protected MessageTypes.Std.String nluMessage = new MessageTypes.Std.String();
        protected bool IsMessageSet = false;
        abstract protected void SetMessage();


    }
}
