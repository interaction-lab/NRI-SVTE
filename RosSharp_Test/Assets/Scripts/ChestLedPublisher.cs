using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;
using Microsoft.MixedReality.Toolkit.Experimental.ColorPicker;

namespace RosSharp.RosBridgeClient {
    public class ChestLedPublisher : UnityPublisher<MessageTypes.MobileBaseDriver.ChestLeds> {
        private MessageTypes.MobileBaseDriver.ChestLeds message;
        public ColorPicker colorPicker;
        private AnimationPublisher animPub;


        protected override void Start() {
            base.Start();
            InitializeMessage();
            if (!colorPicker) {
                colorPicker = FindObjectOfType<ColorPicker>(); // TODO: remove all `FindObjectOfType<T>`
            }
        }

        private void FixedUpdate() {
            if (colorPicker != null && colorPicker.isActiveAndEnabled) {
                //Color c;// = colorPicker.CustomColor;
                //setColor((int)(c.r * 255), (int)(c.g * 255), (int)(c.b * 255));
            }
        }

        private void InitializeMessage() {
            message = new MessageTypes.MobileBaseDriver.ChestLeds();
            for (int i = 0; i < message.leds.Length; i++) {
                message.leds[i] = new Led();
            }
            setColor(0, 0, 0);
        }

        public void setColor(int red, int green, int blue) {
            //animPub.PublishAnim(AnimationPublisher.ANIMATION_CMD.smile);
            for (int i = 0; i < message.leds.Length; i++) {
                message.leds[i].red = (byte)red;
                message.leds[i].green = (byte)green;
                message.leds[i].blue = (byte)blue;
            }
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