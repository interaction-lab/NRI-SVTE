using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
namespace RosSharp.RosBridgeClient
{
    public class MobileSensorSubscriber : UnitySubscriber<MessageTypes.MobileBaseDriver.Sensors>
    {
        //public MeshRenderer meshRenderer;
        //public Transform PublishedTransform;
        public MeshRenderer meshRenderer;
        public Material off;
        public Material on;
        private Vector3 position;

        private MessageTypes.MobileBaseDriver.Touch touch;
        private bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
    }
        private void Update()
        {
            //Debug.Log("test");
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.MobileBaseDriver.Sensors message)
        {
            //Debug.Log("got a message!");
            this.touch = message.touch;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //Debug.Log("pm");
            if (touch.electrodes[0])
            {
                Debug.Log("FL");
                meshRenderer.material = on;
            }else
            {
                meshRenderer.material = off;
            }
            if (touch.electrodes[1])
            {
                Debug.Log("F");
            }
            if (touch.electrodes[2])
            {
                Debug.Log("RL");
            }
            if (touch.electrodes[3])
            {
                Debug.Log("C");
            }
            if (touch.electrodes[0])
            {
                Debug.Log("FL");
            }
            isMessageReceived = false;
        }
    }
}