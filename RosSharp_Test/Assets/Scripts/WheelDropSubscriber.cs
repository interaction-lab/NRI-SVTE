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
        private AnimationPublisher _animPub;
        bool pickup;
        bool prevPickup;

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
            pickup = false;
            prevPickup = false;
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
            if (wheeldrop[0].state == 1)
                mesh.material = on;
            else if (wheeldrop[1].state == 1)
                mesh.material = on;
            else
                mesh.material = off;

            pickup = (wheeldrop[0].state == 1 || wheeldrop[1].state == 1);
            
            if(pickup != prevPickup)
            {
                if(pickup)
                    AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.pickup);
                else
                    AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.putdown);
            }
            prevPickup = pickup;

            isMessageReceived = false;
        }
    }
}