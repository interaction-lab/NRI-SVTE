using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Vision;
namespace RosSharp.RosBridgeClient
{
    public class FaceDetectionSubscriber : UnitySubscriber<MessageTypes.Vision.FrameResults>
    {
        //public MeshRenderer meshRenderer;
        public Transform PublishedTransform;
        private Vector3 position;

        private MessageTypes.Vision.FaceArray faces;
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
                position = GetPosition(faces.faces[0]);
                PublishedTransform.position = position;
            }
            isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Vision.Face message)
        {
            return new Vector3(
                 10 + (5 * (float)message.center.x),
                5 + (-5 * (float)message.center.y),
                //(float)message.center.z);
                PublishedTransform.position.z);
        }
    }
}