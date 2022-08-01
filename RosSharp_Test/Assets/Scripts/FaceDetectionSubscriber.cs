using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Vision;
namespace RosSharp.RosBridgeClient {
    public class FaceDetectionSubscriber : UnitySubscriber<MessageTypes.Vision.FrameResults> {
        public Transform PublishedTransform;
        private Vector3 position;
        private Vector3 targetPosition;
        private Vector3 scale;
        private Vector3 targetScale;

        private MessageTypes.Vision.FaceArray faces;
        private bool isMessageReceived;
        private AnimationPublisher _animPub;
        public AnimationPublisher AnimPublisher {
            get {
                if (!_animPub) {
                    _animPub = FindObjectOfType<AnimationPublisher>();
                }
                return _animPub;
            }
        } // TODO: replace inspector public variable with better getter

        private float speed = 0.0025f;
        float turnOffTime = 1f;
        private bool userThere;
        MeshRenderer pubrend;

        protected override void Start() {
            base.Start();
            userThere = false;
            pubrend = PublishedTransform.GetComponent<MeshRenderer>();
        }
        private void Update() {
            if (isMessageReceived)
                ProcessMessage();
            LerpPosition(targetPosition, speed);
            LerpScale(targetScale, speed);
            if(turnOffTime < 0 && pubrend.enabled)
            {
                pubrend.enabled = false;
            }
            else
            {
                turnOffTime -= Time.deltaTime;
            }
        }

        protected override void ReceiveMessage(MessageTypes.Vision.FrameResults message) {
            this.faces = message.faces;
            isMessageReceived = true;
        }

        void TurnOn()
        {
            pubrend.enabled = true;
            turnOffTime = 1f;   
        }

        

        private void ProcessMessage() {
            if (faces.faces.Length > 0) {
                if (!userThere)
                {
                    AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.face_detected);
                }
                TurnOn();
                userThere = true;
                targetPosition = GetPosition(faces.faces[0]);
                targetScale = GetScale(faces.faces[0]);
            }
            else {
                if (userThere)
                {
                    AnimPublisher.PublishAnim(AnimationPublisher.ANIMATION_CMD.face_lost);
                }
                userThere = false;
            }
            isMessageReceived = false;
        }

        private void LerpPosition(Vector3 targetPosition, float speed) {
            Vector3 myPos = PublishedTransform.localPosition;
            myPos.z = 0;
            targetPosition.z = 0;
            if (Vector3.Distance(myPos, targetPosition) > speed)
            {
                Vector3 addition = Vector3.Normalize(targetPosition - myPos) * speed;
                position = PublishedTransform.localPosition + addition;
                position.z = PublishedTransform.localPosition.z;
                PublishedTransform.localPosition = position;
            }
        }

        private void LerpScale(Vector3 targetScale, float speed) {
            Vector3 myScale = PublishedTransform.localScale;
            myScale.z = 0;
            targetScale.z = 0;
            if (Vector3.Distance(myScale, targetScale) > speed)
            {
                Vector3 addition = Vector3.Normalize(targetScale - myScale) * speed;
                scale = PublishedTransform.localScale + addition;
                scale.z = PublishedTransform.localScale.z;
                PublishedTransform.localScale = scale;
            }
        }

        private Vector3 GetPosition(MessageTypes.Vision.Face message) {
            return new Vector3(
                (float)message.center.x,
                (float)message.center.y,
                PublishedTransform.position.z);
        }

        private Vector3 GetScale(MessageTypes.Vision.Face message) {
            return new Vector3(
                (float)(message.bb[3] * 0.001),
                (float)(message.bb[2] * 0.001),
                PublishedTransform.localScale.z);
        }
    }
}