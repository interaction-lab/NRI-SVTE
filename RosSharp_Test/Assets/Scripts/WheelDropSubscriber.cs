using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
namespace RosSharp.RosBridgeClient
{
    public class WheelDropSubscriber : UnitySubscriber<MessageTypes.MobileBaseDriver.Sensors>
    {
        public MeshRenderer mesh;
        public Material off;
        public Material on;

        private MessageTypes.MobileBaseDriver.WheelDrop[] wheeldrop;
        private bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.MobileBaseDriver.Sensors message)
        {
            this.wheeldrop = message.wheeldrop;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (wheeldrop[0].state == 0)
                mesh.material = on;
            else
                mesh.material = off;

            if (wheeldrop[1].state == 0)
                mesh.material = on;
            else
                mesh.material = off;

            isMessageReceived = false;
        }
    }
}