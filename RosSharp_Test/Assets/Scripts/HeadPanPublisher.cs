using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class HeadPanPublisher : UnityPublisher<MessageTypes.Trajectory.JointTrajectoryPoint>
    {
        private MessageTypes.Trajectory.JointTrajectoryPoint message;
        private double panLeft = 0.78;
        private double panRight = -0.78;
        private double tiltUp = -0.92;
        private double tiltDown = 0.29;
        private double[] positions;
        private double moveCoef = 0.1;

        protected override void Start()
        {
            base.Start();
            positions = new double[2];
            positions[0] = 0;
            positions[1] = 0;
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Trajectory.JointTrajectoryPoint();
            message.positions = positions;
            Publish(message);

        }
        private void UpdateMessage()
        {
        }

        public void moveUp()
        {
            if(positions[1] - moveCoef > tiltUp)
            {
                positions[1] -= moveCoef;
                message.positions = positions;
                Publish(message);
            }
        }

        public void moveDown()
        {
            if (positions[1] + moveCoef < tiltDown)
            {
                positions[1] += moveCoef;
                message.positions = positions;
                Publish(message);
            }
        }

        public void moveRight()
        {
            if (positions[0] - moveCoef > panRight)
            {
                positions[0] -= moveCoef;
                message.positions = positions;
                Publish(message);
            }
        }

        public void moveLeft()
        {
            if (positions[0] + moveCoef < panLeft)
            {
                positions[0] += moveCoef;
                message.positions = positions;
                Publish(message);
            }
        }
    }
}