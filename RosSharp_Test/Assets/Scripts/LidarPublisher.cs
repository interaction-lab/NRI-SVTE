using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Sensor;
using Microsoft.MixedReality.Toolkit.Experimental.ColorPicker;
//
// Topic = /scan
// // header:
// //   seq: 20381
// //   stamp:
// //     secs: 1630021777
// //     nsecs: 150040000
// //   frame_id: hokuyo_laser_link
// // angle_min: -1.57079637051
// // angle_max: 1.57079637051
// // angle_increment: 0.0174532923847
// // time_increment: 0.0
// // scan_time: 0.0
// // range_min: 0.0500000007451
// // range_max: 6.75
// // ranges: [nan, inf, nan, inf, inf, inf, inf, inf, nan, inf, inf, 0.5759999752044678, 0.5669999718666077, 0.5210000276565552, 0.5049999952316284, 0.4830000102519989, 0.46399998664855957, 0.45500001311302185, 0.46000000834465027, 0.4390000104904175, 0.4480000138282776, 0.44200000166893005, 0.41499999165534973, 0.45500001311302185, 0.4399999976158142, 0.4309999942779541, 0.45500001311302185, 0.4650000035762787, 0.4690000116825104, nan, 0.49900001287460327, 0.5120000243186951, 0.5590000152587891, 0.5529999732971191, 0.5509999990463257, 0.5659999847412109, 0.5690000057220459, inf, 0.6129999756813049, inf, inf, inf, 0.6259999871253967, inf, inf, inf, inf, inf, inf, nan, nan, nan, nan, inf, nan, nan, nan, inf, nan, inf, inf, nan, nan, nan, inf, nan, nan, nan, nan, nan, inf, inf, nan, nan, nan, 0.5699999928474426, 0.5770000219345093, 0.5690000057220459, 0.5669999718666077, nan, nan, nan, nan, 0.6290000081062317, 0.621999979019165, 0.6209999918937683, nan, nan, nan, nan, nan, nan, nan, nan, nan, nan, nan, nan, inf, nan, inf, nan, 1.1430000066757202, 1.1490000486373901, inf, 1.1670000553131104, 1.1670000553131104, nan, inf, nan, inf, inf, inf, nan, inf, nan, 1.062999963760376, 1.0379999876022339, 1.0210000276565552, 1.0169999599456787, 2.7790000438690186, 2.7939999103546143, inf, inf, inf, nan, inf, 2.5360000133514404, 2.4790000915527344, 2.377000093460083, 2.3459999561309814, 2.3340001106262207, 2.321000099182129, nan, inf, inf, nan, nan, inf, inf, inf, nan, inf, inf, inf, inf, inf, inf, inf, 1.4769999980926514, 1.4769999980926514, 1.472000002861023, 1.465999960899353, 1.4600000381469727, 1.4630000591278076, 1.4570000171661377, nan, nan, nan, inf, inf, 1.5219999551773071, 1.4500000476837158, 1.534000039100647, 1.315000057220459, 1.2899999618530273, 1.2419999837875366, 1.2400000095367432, 1.2690000534057617, 1.2740000486373901, 1.2740000486373901, 1.277999997138977, 1.281000018119812, 1.3339999914169312, 1.3459999561309814, 1.3769999742507935, inf, inf, nan, nan]
// // intensities: []

namespace RosSharp.RosBridgeClient {
    public class LidarPublisher : UnityPublisher<MessageTypes.Sensor.LaserScan> {
        private MessageTypes.Sensor.LaserScan message;
        public ColorPicker colorPicker;
        private AnimationPublisher animPub;
        private int layermask;
        private RaycastHit hit;


        protected override void Start() {
            layermask = (1 << 6);
            layermask = ~layermask;
            base.Start();
            InitializeMessage();
            if (!colorPicker) {
                colorPicker = FindObjectOfType<ColorPicker>(); // TODO: remove all `FindObjectOfType<T>`
            }
        }

        private void FixedUpdate() {
            if (colorPicker != null && colorPicker.isActiveAndEnabled) {
                Color c = colorPicker.CustomColor;
                setColor((int)(c.r * 255), (int)(c.g * 255), (int)(c.b * 255));
            }
        }

        private void InitializeMessage() {
            // message = new MessageTypes.MobileBaseDriver.ChestLeds();
            int layermask = 1 << 6;
            layermask = ~layermask;
            message = new MessageTypes.Sensor.LaserScan();
            for (int i = 0; i < message.ranges.Length; i++) {
                float angle = i * Mathf.PI / 180;
                float angleDegrees = 180 - angle*Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                if (Physics.Raycast(transform.position, rot * transform.forward, out hit, 2.5f, layermask)){
                  message.ranges[i] = hit.distance;
                }
                else{
                  message.ranges[i] = float.NaN;
                }

            }
            setColor(0, 0, 0);
        }


        public void setColor(int red, int green, int blue) {
            //animPub.PublishAnim(AnimationPublisher.ANIMATION_CMD.smile);
            // for (int i = 0; i < message.leds.Length; i++) {
            //     message.leds[i].red = (byte)red;
            //     message.leds[i].green = (byte)green;
            //     message.leds[i].blue = (byte)blue;
            // }
            // Publish(message);
        }

        public void scan() {
          // TODO: what stamp should I use? does it matter? the one in the slack example was 0
          // message.header.stamp = scan_time;
          // TODO: should I be using hokuyo_laser_link as my frame_id?
          // to conclude, I have no idea what to put in the header
          // scan.header.frame_id = "laser_frame";

          scan.angle_min = -1.57;
          scan.angle_max = 1.57;
          scan.angle_increment = 0.0174532923847;
          scan.time_increment = 0.0;
          scan.range_min = 0.0500000007451;
          scan.range_max = 6.75;

          // Ranges should be the readings from all 180 rays
          scan.ranges.resize(num_readings);
          // scan.intensities = [];
          for(int i = 0; i < num_readings; ++i){
            scan.ranges[i] = ranges[i];
          }

          scan_pub.publish(scan);
            Publish(message);
        }

        public void setBlue(int b) {
            for (int i = 0; i < message.leds.Length; i++) {
                message.leds[i].blue = (byte)b;
            }
            Publish(message);
        }

        public void setRed(int r) {
            for (int i = 0; i < message.leds.Length; i++) {
                message.leds[i].red = (byte)r;
            }
            Publish(message);
        }

        public void setGreen(int g) {
            for (int i = 0; i < message.leds.Length; i++) {
                message.leds[i].green = (byte)g;
            }
            Publish(message);
        }
    }
}
