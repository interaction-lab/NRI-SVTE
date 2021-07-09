using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
namespace RosSharp.RosBridgeClient
{
    public class CapTouchSubscriber : UnitySubscriber<MessageTypes.MobileBaseDriver.Sensors>
    {
        public MeshRenderer meshFrontLeft;
        public MeshRenderer meshLeft;
        public MeshRenderer meshRearLeft;
        public MeshRenderer meshCenter;
        public MeshRenderer meshFront;
        public MeshRenderer meshRearRight;
        public MeshRenderer meshRight;
        public MeshRenderer meshFrontRight;
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
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.MobileBaseDriver.Sensors message)
        {
            this.touch = message.touch;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (touch.electrodes[0])
                meshFrontLeft.material = on;
            else
                meshFrontLeft.material = off;

            if (touch.electrodes[1])
                meshLeft.material = on;
            else
                meshLeft.material = off;

            if (touch.electrodes[2])
                meshRearLeft.material = on;
            else
                meshRearLeft.material = off;

            if (touch.electrodes[3])
                meshCenter.material = on;
            else
                meshCenter.material = off;

            if (touch.electrodes[4])
                meshFront.material = on;
            else
                meshFront.material = off;

            if (touch.electrodes[5])
                meshRearRight.material = on;
            else
                meshRearRight.material = off;

            if (touch.electrodes[6])
                meshRight.material = on;
            else
                meshRight.material = off;

            if (touch.electrodes[6])
                meshFrontRight.material = on;
            else
                meshFrontRight.material = off;

            isMessageReceived = false;
        }
    }
}