using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class HeadPanPublisher : UnityPublisher<MessageTypes.Trajectory.JointTrajectoryPoint> {
        private MessageTypes.Trajectory.JointTrajectoryPoint msg;

        string JOINT_PAN = "head_1_joint", JOINT_TILT = "head_2_joint", JOINT_EYES = "eyelids_joint";
        static float JOINT_HEIGHT = 0.405f, PAN_LEFT = 0.78f, PAN_NEUTRAL = 0f;
        static float PAN_RIGHT = -PAN_LEFT, TILT_UP = -0.92f, TILT_NEUTRAL = 0.0f;
        static float TILT_DOWN = 0.29f, EYES_OPEN = 0.0f, EYES_NEUTRAL = 0.1f;
        static float EYES_CLOSED = 0.41f, EYES_HAPPY = -0.16f, EYES_SUPER_SAD = 0.15f, EYES_CLOSED_BLINK = 0.35f;
        string HEAD_NS = "head_controller/follow_joint_trajectory", EYES_NS = "eyelids_controller/follow_joint_trajectory";


        /*       private double panLeft = 0.78;
               private double panRight = -0.78;
               private double tiltUp = -0.92;
               private double tiltDown = 0.29;
               private double[] positions;
               private double moveCoef = 0.1;*/

        protected override void Start() {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage() {
            msg = new MessageTypes.Trajectory.JointTrajectoryPoint();
        }

        public void moveUp() {

        }

        public void moveDown() {

        }

        public void moveRight() {

        }

        public void moveLeft() {
            /*if (positions[0] + moveCoef < panLeft) {
                positions[0] += moveCoef;
                msg.positions = positions;
                Publish(msg);
            }*/
        }
    }
}