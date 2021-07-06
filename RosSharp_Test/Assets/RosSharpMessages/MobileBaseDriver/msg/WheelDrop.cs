/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



namespace RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver
{
    public class WheelDrop : Message
    {
        public const string RosMessageName = "mobile_base_driver/WheelDrop";

        //  Provides a wheeldrop sensor stae
        //  wheel
        public const byte RIGHT = 0;
        public const byte LEFT = 1;
        //  wheel state
        public const byte RAISED = 0;
        public const byte DROPPED = 1;
        public byte wheel { get; set; }
        public byte state { get; set; }

        public WheelDrop()
        {
            this.wheel = 0;
            this.state = 0;
        }

        public WheelDrop(byte wheel, byte state)
        {
            this.wheel = wheel;
            this.state = state;
        }
    }
}
