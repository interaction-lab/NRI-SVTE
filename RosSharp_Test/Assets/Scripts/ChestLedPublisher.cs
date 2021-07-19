using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.MobileBaseDriver;

namespace RosSharp.RosBridgeClient
{
    public class ChestLedPublisher : UnityPublisher<MessageTypes.MobileBaseDriver.ChestLeds>
    {
        private MessageTypes.MobileBaseDriver.ChestLeds message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.MobileBaseDriver.ChestLeds();
            for (int i = 0; i < message.leds.Length; i++)
            {
                message.leds[i] = new Led();
            }
            setColor(0, 0, 0);
        }

        public void setColor(int red, int green, int blue)
        {
            for (int i = 0; i < message.leds.Length; i++)
            {
                Debug.Log(message.leds[i]);
                message.leds[i].red = (byte)red;
                message.leds[i].green = (byte)green;
                message.leds[i].blue = (byte)blue;
            }
            Publish(message);
        }

        public void setBlue(int b)
        {
            for (int i = 0; i < message.leds.Length; i++)
            {
                message.leds[i].blue = (byte)b;
            }
            Publish(message);
        }

        public void setRed(int r)
        {
            for (int i = 0; i < message.leds.Length; i++)
            {
                message.leds[i].red = (byte)r;
            }
            Publish(message);
        }

        public void setGreen(int g)
        {
            for (int i = 0; i < message.leds.Length; i++)
            {
                message.leds[i].green = (byte)g;
            }
            Publish(message);
        }
    }
}