using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Vision;
namespace RosSharp.RosBridgeClient
{
    public class FaceDetectionSubscriber : UnitySubscriber<MessageTypes.Vision.FrameResults>
    {
        public Transform PublishedTransform;
        private Vector3 position;
        private Vector3 targetPosition;
        private Vector3 scale;
        private Vector3 targetScale;

        private MessageTypes.Vision.FaceArray faces;
        private bool isMessageReceived;

        private float lerpDuration = 3;
        private float startValue = 0;
        private float endValue = 10;
        private float valueToLerp;

        protected override void Start()
        {
            base.Start();
    }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
            LerpPosition(targetPosition, 5);
            LerpScale(targetScale, 5);
        }

        protected override void ReceiveMessage(MessageTypes.Vision.FrameResults message)
        {
            this.faces = message.faces;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (faces.faces.Length > 0)
            {
                targetPosition = GetPosition(faces.faces[0]);
                targetScale = GetScale(faces.faces[0]);                
            }
            isMessageReceived = false;
        }

        private void Lerp(float timeElapsed)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, 1);
        }

        private void LerpPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = PublishedTransform.position;

            position = Vector3.Lerp(startPosition, targetPosition, 1);
            position.z = PublishedTransform.position.z;
            PublishedTransform.position = position;
        }

        private void LerpScale (Vector3 targetScale, float duration)
        {
            Vector3 startScale = PublishedTransform.localScale;

            scale = Vector3.Lerp(startScale, targetScale, 1);
            scale.z = PublishedTransform.localScale.z;
            PublishedTransform.localScale = scale;
        }

        private Vector3 GetPosition(MessageTypes.Vision.Face message)
        {
            return new Vector3(
                 10 + (5 * (float)message.center.x),
                5 + (-5 * (float)message.center.y),
                PublishedTransform.position.z);
        }

        private Vector3 GetScale(MessageTypes.Vision.Face message)
        {
            return new Vector3(
                (float)(message.bb[3] * 0.01),
                (float)(message.bb[2] * 0.01),
                PublishedTransform.localScale.z);
        }
    }
}