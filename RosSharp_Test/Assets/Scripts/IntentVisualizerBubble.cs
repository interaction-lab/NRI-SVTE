using NLUDataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class IntentVisualizerBubble : IntentVisualizer
    {
        private GameObject intentBubble;
       
        public override void Disable()
        {
            intentBubble.SetActive(false);
        }

        public override void Visualize(NLUIntent[] intents)
        {
            intentBubble.SetActive(true);
            TextMeshProUGUI intentText = GameObject
                .FindWithTag(ResourcePathManager.intentTextTag).GetComponent<TextMeshProUGUI>();
            intentText.text = "";
            foreach (NLUIntent intent in intents)
            {
                intentText.text += intent.ToString() + ";";
                intentText.text += "\n\n";
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            intentBubble = GameObject.FindWithTag(ResourcePathManager.intentBubbleTag);
            intentBubble.SetActive(false);
        }

       
    }
}
