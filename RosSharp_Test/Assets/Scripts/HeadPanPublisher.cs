using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class HeadPanPublisher : UnityPublisher<MessageTypes.Std.String> {
        private MessageTypes.Std.String msg;

        protected override void Start() {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage() {
            msg = new MessageTypes.Std.String();
        }

        public void MoveUp() {
            msg.data = "U";
            Publish(msg);
        }

        public void MoveDown() {
            msg.data = "D";
            Publish(msg);
        }

        public void MoveRight() {
            msg.data = "R";
            Publish(msg);
        }

        public void MoveLeft() {
            msg.data = "L";
            Publish(msg);
        }

        public void MoveNeutral() {
            msg.data = "N";
            Publish(msg);
        }
    }
}