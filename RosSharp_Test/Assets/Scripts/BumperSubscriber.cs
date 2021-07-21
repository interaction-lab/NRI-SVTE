using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
namespace RosSharp.RosBridgeClient
{
    public class BumperSubscriber : UnitySubscriber<MessageTypes.MobileBaseDriver.Sensors>
    {
        public MeshRenderer meshBumper;
        public Material off;
        public Material on;

        private MessageTypes.MobileBaseDriver.Bumper[] bumper;
        private bool isMessageReceived;
        private AnimationPublisher animPub;

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
            this.bumper = message.bumper;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (bumper[0].state == 1 || bumper[1].state == 1 || bumper[2].state == 1)
            {
                meshBumper.material = on;
                animPub.PublishAnim(AnimationPublisher.ANIMATION_CMD.bump);
            } else
            { 
                meshBumper.material = off;
            }

            isMessageReceived = false;
        }
    }
}