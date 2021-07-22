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
        private bool react;

        private MessageTypes.MobileBaseDriver.Touch touch;
        private bool isMessageReceived;
        private AnimationPublisher _animPub;
        public AnimationPublisher AnimPublisher
        {
            get
            {
                if (!_animPub)
                {
                    _animPub = FindObjectOfType<AnimationPublisher>();
                }
                return _animPub;
            }
        } // TODO: replace inspector public variable with better getter

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
            touch = message.touch;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (touch.electrodes[0] || touch.electrodes[1] || touch.electrodes[2] || touch.electrodes[3] || touch.electrodes[4] || touch.electrodes[5] || touch.electrodes[6])
                AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.smile);

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