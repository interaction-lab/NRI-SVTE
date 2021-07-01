using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Vision;
namespace RosSharp.RosBridgeClient
{
    public class FaceDetectionSubscriber : UnitySubscriber<MessageTypes.Vision.FaceArray>
    {
        //public MeshRenderer meshRenderer;
        public Transform PublishedTransform;
        private Vector3 position;

        private MessageTypes.Vision.Face[] faces;
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

        protected override void ReceiveMessage(MessageTypes.Vision.FaceArray faceArray)
        {
            this.faces = faceArray.faces;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if(faces.Length > 0)
            {
                position = GetPosition(faces[0]).Ros2Unity();
                PublishedTransform.position = position;
            }
            isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Vision.Face message)
        {
            return new Vector3(
                (float)message.center.x,
                (float)message.center.y,
                (float)message.center.z);
        }
    }
}