using UnityEngine;
using System.Collections;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
using NRISVTE;

namespace RosSharp.RosBridgeClient
{
    public class BumperSubscriber : UnitySubscriber<MessageTypes.MobileBaseDriver.Sensors>
    {
        public MeshRenderer meshBumper;
        public Material off;
        public Material on;

        private MessageTypes.MobileBaseDriver.Bumper[] bumper;
        private bool isMessageReceived;
        private AnimationPublisher _animPub;
        bool animating = false;
        

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

        IEnumerator BumpSound()
        {
            if (animating)
            {
                yield break;
            }

            animating = true;
            AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.bump);
            yield return new WaitForSeconds(1.0f);
            animating = false;
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
                StartCoroutine(BumpSound());
                VisualizationManager.instance.toggleLidar(false);


            } else
            { 
                meshBumper.material = off;
            }

            isMessageReceived = false;
        }
    }
}