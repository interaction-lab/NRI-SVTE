using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class ControllerTwistPublisher : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        private MessageTypes.Geometry.Twist message;
        //private float previousRealTime;        
        //private Vector3 previousPosition = Vector3.zero;
        //private Quaternion previousRotation = Quaternion.identity;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            //float deltaTime = Time.realtimeSinceStartup - previousRealTime;

            //Vector3 linearVelocity = (PublishedTransform.position - previousPosition)/deltaTime;
            //Vector3 angularVelocity = (PublishedTransform.rotation.eulerAngles - previousRotation.eulerAngles)/deltaTime;

            //message.linear = GetGeometryVector3(linearVelocity.Unity2Ros()); ;
            //message.angular = GetGeometryVector3(- angularVelocity.Unity2Ros());

            //previousRealTime = Time.realtimeSinceStartup;
            //previousPosition = PublishedTransform.position;
            //previousRotation = PublishedTransform.rotation;

            //Publish(message);
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }

        public void moveUp()
        {
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
            message.angular.z = 0;
            message.angular.x = 0;
            message.angular.y = 0;
            message.linear.x = 1;
            message.linear.z = 0;
            message.linear.y = 0;
            Publish(message);

        }

        public void moveDown()
        {
            message.angular.z = 0;
            message.angular.x = 0;
            message.angular.y = 0;
            message.linear.x = -1;
            message.linear.z = 0;
            message.linear.y = 0;
            Publish(message);
        }

        public void moveLeft()
        {
            message.angular.z = 1;
            message.linear.x = 0;
            Publish(message);
        }

        public void moveRight()
        {
            message.angular.z = -1;
            message.linear.x = 0;
            Publish(message);
        }
    }
}