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
            setColor(0, 0, 0);
        }

        private void setColor(byte red, byte green, byte blue)
        {
            for (int i = 0; i < message.leds.Length; i++)
            {
                message.leds[i].red = red;
                message.leds[i].green = green;
                message.leds[i].blue = blue;
            }

            Publish(message);
        }
    }
}