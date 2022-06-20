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
        private IntentVisualizer[] intentVisualizers;
        private GameObject[] intentElements;
        private GameObject[] intentElementsRemovable;
        public bool IntentElementsEnabled = false;

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
            {
                SortIntents(message.Intents);
                for (int i = 0; i < intentVisualizers.Length; i++)
                    intentVisualizers[i].Visualize(message.Intents);
                SetIntentElementsActive(true);
                HighlightKeywords(message.Intents);
            }
            else
            {
                for (int i = 0; i < intentVisualizers.Length; i++)
                    intentVisualizers[i].Disable();
                SetIntentElementsActive(false);
            }
                
        }

        private void SortIntents(NLUIntent[] intentList)
        {
            System.Array.Sort(intentList, new NLUIntentComparer());
        }

        private void HighlightKeywords(NLUIntent[] intentList)
        {
            for(int i = 0; i < intentList.Length ; i++)
            {
                HighlightIntentKeywords(intentList[i], intentVisualizers[0].GetIntentColor(i));
                
            }
        }

        private void HighlightIntentKeywords(NLUIntent intent, Color color)
        {
            string hexColor = ColorUtility.RGBToHex(color);
           
            if (intent.Keywords == null)
                return;
            for(int i = 0; i < intent.Keywords.Length; i++)
            {
               
                speechBubble.ChangeText(
                    speechBubble.GetText().Replace(
                        intent.Keywords[i], "<u color=" + hexColor + ">"+intent.Keywords[i]+"</u>"));
            }
        }

       
        // Determines if the loading icon should be active or not (NLUState == 1 || NLUState == 0).
        private bool IsLoadingIconActive(int robotState)
        {
            if (robotState == 0 || robotState == 1)
                return true;
            else
                return false;
        }   


        // Start is called before the first frame update
        void Start()
        {
            speechBubble = GameObject.FindWithTag(ResourcePathManager.speechBubbleTag).GetComponent<SpeechBubble>();
            intentVisualizers = gameObject.GetComponents<IntentVisualizer>();
            speechBubble.Setup("",false);
            intentElements = GameObject.FindGameObjectsWithTag(ResourcePathManager.intentElementTag);
            intentElementsRemovable = GameObject.FindGameObjectsWithTag(ResourcePathManager.intentElementRemovableTag);
            if (!IntentElementsEnabled)
            {
                for (int i = 0; i < intentElementsRemovable.Length; i++) 
                    intentElementsRemovable[i].SetActive(false);
                
            }
            SetIntentElementsActive(false);
        }
        
        private void SetIntentElementsActive(bool active)
        {
            for (int i = 0; i < intentElements.Length; i++)
                intentElements[i].SetActive(active);
            if (IntentElementsEnabled)
            {
                for (int f = 0; f < intentElementsRemovable.Length; f++)
                    intentElementsRemovable[f].SetActive(active);
            }
        }

       
    }
}
