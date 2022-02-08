using System.Text.Json;
using System.IO;
using NLUDataTypes;
using System.Collections;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class FakeNLUProvider : NLUProvider
    {
        private NLUDialogue dialogue;
        private int lastMessage = -1;
        protected override void SetMessage()
        {
            //In setMessage I have to serialize NLUData into a string, and set isMessageSet to true
            string message = JsonSerializer.Serialize<NLUData>(dialogue.Messages[lastMessage].Message);
            nluMessage = new MessageTypes.Std.String
            {
                data = message
            };

            IsMessageSet = true;
        }

        public IEnumerator SetMessageCallback()
        {
            if (lastMessage == -1)
                yield return new WaitForSeconds(dialogue.Messages[0].Timestamp / 1000);
           
            for(int i = 0; i < dialogue.Messages.Length; i++)
            {
                lastMessage = i;
                SetMessage();
                if (i + 1 < dialogue.Messages.Length)
                    yield return new WaitForSeconds((dialogue.Messages[lastMessage + 1].Timestamp
                        - dialogue.Messages[lastMessage].Timestamp) / 1000);
            }
        }

        void Update()
        {
            if (IsMessageSet)
            {
                for(int i = 0; i < nluVisualizers.Length; i++)
                {
                    nluVisualizers[i].Visualize(nluMessage);
                }
                IsMessageSet = false;
            }
        }

        void Start()
        {
            nluVisualizers = GetComponents<NLUVisualizer>();
            string jsonString = File.ReadAllText(ResourcePathManager.nluFakeDataPath);
            dialogue = JsonSerializer.Deserialize<NLUDialogue>(jsonString);
            lastMessage = -1;
            StartCoroutine(SetMessageCallback());
            
        }


    }
}
