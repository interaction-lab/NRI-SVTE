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
            //Debug.Log("test");
            if (isMessageReceived)
                ProcessMessage();
            LerpPosition(targetPosition, 5);

        }

        protected override void ReceiveMessage(MessageTypes.Vision.FrameResults message)
        {
            //Debug.Log("got a message!");
            this.faces = message.faces;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //Debug.Log("pm");
            if (faces.faces.Length > 0)
            {
                Debug.Log("I see a face!");
                targetPosition = GetPosition(faces.faces[0]);
                scale = GetScale(faces.faces[0]);
                
                PublishedTransform.localScale = scale;
            }
            isMessageReceived = false;
        }

        private void Lerp(float timeElapsed)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, 1);
                //timeElapsed / lerpDuration);
        }

        private void LerpPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = PublishedTransform.position;

            position = Vector3.Lerp(startPosition, targetPosition, 1);
            position.z = PublishedTransform.position.z;
            PublishedTransform.position = position;
        }

        private Vector3 GetPosition(MessageTypes.Vision.Face message)
        {
            return new Vector3(
                 10 + (5 * (float)message.center.x),
                5 + (-5 * (float)message.center.y),
                //(float)message.center.z);
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