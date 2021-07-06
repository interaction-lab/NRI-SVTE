/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Std;

namespace RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver
{
    public class WheelTraj : Message
    {
        public const string RosMessageName = "mobile_base_driver/WheelTraj";

        public Header header { get; set; }
        public WheelTrajPoint[] points { get; set; }

        public WheelTraj()
        {
            this.header = new Header();
            this.points = new WheelTrajPoint[0];
        }

        public WheelTraj(Header header, WheelTrajPoint[] points)
        {
            this.header = header;
            this.points = points;
        }
    }
}
