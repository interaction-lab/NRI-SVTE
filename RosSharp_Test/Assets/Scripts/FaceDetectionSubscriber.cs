using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Vision;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class FaceDetectionSubscriber : UnitySubscriber<MessageTypes.Vision.FaceArray>
    {
        public MeshRenderer meshRenderer;
        public Transform PublishedTransform;
        private Vector3 position;

        private Face[] faces;
        private Point center; //center of the first face
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
            if(faces[0] != null)
            {
                position = GetPosition(faces[0].center).Ros2Unity();
                PublishedTransform.position = position;
            }
            meshRenderer.material.SetTexture("_MainTex", texture2D);
            isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Vision.FaceArray message)
        {
            return new Vector3(
                (float)message.faces[0].center.position.x,
                (float)message.faces[0].center.position.y,
                (float)message.faces[0].center.position.z);
        }
    }
}