using RosSharp.RosBridgeClient.MessageTypes.Std;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NLUDataTypes;
using System.Text.Json;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class NLUVisualizerSpeechBubble : NLUVisualizer
    {
        private SpeechBubble speechBubble;
        private GameObject intentBubble;

        public override void DestroyObjects()
        {
            throw new System.NotImplementedException();
        }

        public override void Visualize(String nluMessage)
        {
            NLUData message = JsonSerializer.Deserialize<NLUData>(nluMessage.data.ToString());
            if (message.NLUState == 0)
                speechBubble.ClearText();
            speechBubble.Setup(message.Text, IsLoadingIconActive(message.NLUState));
            if (message.NLUState == 2)
                ShowIntentList(message.Intents);
            else
                intentBubble.gameObject.SetActive(false);
        }

        // Determines if the loading icon should be active or not (NLUState == 1 || NLUState == 0).
        private bool IsLoadingIconActive(int robotState)
        {
            if (robotState == 0 || robotState == 1)
                return true;
            else
                return false;
        }   

        void ShowIntentList(NLUIntent[] intents)
        {
            intentBubble.gameObject.SetActive(true);
            TextMeshProUGUI intentText = GameObject
                .FindWithTag(ResourcePathManager.intentTextTag).GetComponent<TextMeshProUGUI>();
            intentText.text = "";
            foreach(NLUIntent intent in intents)
            {
                intentText.text += intent.ToString() + ";";
                intentText.text += "\n\n";
            }
            
        }

        // Start is called before the first frame update
        void Start()
        {
            speechBubble = GameObject.FindWithTag(ResourcePathManager.speechBubbleTag).GetComponent<SpeechBubble>();
            intentBubble = GameObject.FindWithTag(ResourcePathManager.intentBubbleTag);
            intentBubble.gameObject.SetActive(false);
            speechBubble.Setup("",false);
        }

        
    }
}
